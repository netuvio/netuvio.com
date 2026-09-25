# syntax=docker/dockerfile:1

ARG APP_UID=1000

# ==============================================================================
# Stage 1: Build the backend (.NET SDK)
# ==============================================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-backend
ARG BUILD_CONFIGURATION=Release
ARG BACKEND_ENV_B64
WORKDIR /src

# Restore backend dependencies (client.esproj is skipped via Condition in .csproj)
COPY ["server/server.csproj", "server/"]
RUN dotnet restore "./server/server.csproj"

COPY . .
WORKDIR "/src/server"

# zpracovani .env: pokud je predan BACKEND_ENV_B64 (pres secret nebo build-arg), dekoduje se do .env.
# pripadne se pouzije existujici /src/.env z kontextu, nebo se vytvorie prazdny .env.
RUN --mount=type=secret,id=BACKEND_ENV_B64,required=false \
    if [ -f /run/secrets/BACKEND_ENV_B64 ] && [ -s /run/secrets/BACKEND_ENV_B64 ]; then \
        tr -d '\r\n' < /run/secrets/BACKEND_ENV_B64 | base64 -d > .env; \
    elif [ -n "$BACKEND_ENV_B64" ]; then \
        printf '%s' "$BACKEND_ENV_B64" | tr -d '\r\n' | base64 -d > .env; \
    elif [ -f "/src/.env" ]; then \
        cp /src/.env .env; \
    elif [ ! -f ".env" ]; then \
        touch .env; \
    fi && \
    cp .env /src/.env

RUN dotnet publish "./server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ==============================================================================
# Stage 2: Build the frontend (Node.js) - runs in parallel with Stage 1
# ==============================================================================
FROM node:24-bookworm-slim AS build-client
WORKDIR /src/client

ARG DEPLOY_ENVIRONMENT
ENV DEPLOY_ENVIRONMENT=$DEPLOY_ENVIRONMENT
# Install dependencies (cached by Docker unless package*.json changes)
COPY client/package*.json ./
RUN npm ci

# Build Nuxt production bundle (.output)
COPY client/ ./
ENV NODE_ENV=production
RUN npm run build

# ==============================================================================
# Stage 3: Final runtime image (ASP.NET + Nginx + Node.js runtime)
# ==============================================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ARG DEPLOY_ENVIRONMENT
ENV DEPLOY_ENVIRONMENT=$DEPLOY_ENVIRONMENT

# Copy backend binaries from build-backend
COPY --from=build-backend /app/publish .

# Copy ONLY the compiled frontend bundle (.output) - no node_modules or source files!
COPY --from=build-client /src/client/.output /app/client/.output

# Switch to root to configure permissions and install runtime packages
USER root
RUN if [ -f /app/.env ]; then cp /app/.env /.env && chmod 644 /app/.env /.env; fi

# Install only runtime dependencies (nginx and nodejs runtime), clean apt cache
RUN apt-get update && \
    apt-get install -y --no-install-recommends curl nginx && \
    curl -fsSL https://deb.nodesource.com/setup_24.x | bash - && \
    apt-get install -y --no-install-recommends nodejs && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

# Copy Nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Prepare the start script
EXPOSE 80
WORKDIR /app
COPY --chmod=0755 start.sh .
CMD ["./start.sh"]
