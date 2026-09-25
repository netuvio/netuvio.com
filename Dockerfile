# syntax=docker/dockerfile:1

# Define the user ID (default value 1000)
ARG APP_UID=1000

# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

ARG DEPLOY_ENVIRONMENT
ENV DEPLOY_ENVIRONMENT=$DEPLOY_ENVIRONMENT

# Stage to install Node.js
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS with-node
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_26.x | bash && apt-get install -y nodejs

ARG DEPLOY_ENVIRONMENT
ENV DEPLOY_ENVIRONMENT=$DEPLOY_ENVIRONMENT

# Stage to build the backend
FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
ARG BACKEND_ENV_B64
WORKDIR /src
COPY ["server/server.csproj", "server/"]
COPY ["client/client.esproj", "client/"]
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

# buildnuti backendu
RUN dotnet build "./server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage to publish the backend
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Switch to root to install packages
USER root
RUN if [ -f /app/.env ]; then cp /app/.env /.env && chmod 644 /app/.env /.env; fi
RUN apt-get update && apt-get install -y curl nginx
RUN curl -sL https://deb.nodesource.com/setup_26.x | bash && apt-get install -y nodejs

# Copy frontend files and fix permissions
COPY ["client/", "/app/client/"]
RUN chown -R $APP_UID:$APP_UID /app/client
WORKDIR /app/client

# Set npm cache and install dependencies
RUN npm config set cache /app/.npm
RUN npm install --unsafe-perm

# Build the frontend
RUN npm run build

# Copy Nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Switch back to non-privileged user
#USER $APP_UID

# Prepare the start script
EXPOSE 80
WORKDIR /app
COPY --chmod=0755 start.sh .
CMD ["./start.sh"]
