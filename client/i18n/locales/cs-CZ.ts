import type {MessageSchema} from "~~/i18n/schema";

export default defineI18nLocale(async (locale) => {
    return {
        common: {
            comingSoon: "Již brzy"
        },

        home: {
            title: "Domů",
            subtitle: "Za každým kvalitním projektem stojí nejen dobrý nápad, ale i správné technické řešení. S tím vám pomůžeme od začátku do konce.",
            learnMore: "Zjistit více",
        }
    } satisfies MessageSchema
})