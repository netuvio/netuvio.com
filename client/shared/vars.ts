export const isProd = process.env.NODE_ENV === "production";
export const deployEnvironment = process.env.DEPLOY_ENVIRONMENT === "beta" ? "beta" : "prod";
export const csDomain = deployEnvironment === "beta" ? "https://beta.netuvio.cz" : "https://netuvio.cz";
export const enDomain = deployEnvironment === "beta" ? "https://beta.netuvio.com" : "https://netuvio.com";