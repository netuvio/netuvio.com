import type {MessageSchema} from "~~/i18n/schema";

export default defineI18nLocale(async (locale) => {
    return {
        common: {
            comingSoon: "Coming soon",
        },

        home: {
            title: "Home"
        }
    } satisfies MessageSchema
})