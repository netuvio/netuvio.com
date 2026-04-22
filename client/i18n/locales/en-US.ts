import type {MessageSchema} from "~~/i18n/schema";

export default defineI18nLocale(async (locale) => {
    return {
        common: {
            comingSoon: "Coming soon",
        },

        home: {
            title: "Home",
            subtitle: "Behind every high-quality project lies not only a good idea, but also the right technical solution. We’ll help you with that from start to finish.",
            learnMore: "Learn More",
        },
    } satisfies MessageSchema
})