import type {MessageSchema} from "~~/i18n/schema";

export default defineI18nLocale(async (locale) => {
    return {
        common: {
            comingSoon: "Již brzy"
        },
    } satisfies MessageSchema
})