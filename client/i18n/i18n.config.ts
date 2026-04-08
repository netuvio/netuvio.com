export default {
    fallbackLocale: 'en',
    warnHtmlMessage: false,
    pluralizationRules: {
        cs: (choice: any, choicesLength: any) => {
            // 0
            if(choice === 0) {
                return 0;
            }

            // 1
            if(choice === 1) {
                return 1;
            }

            // 2-4
            if(choice >= 2 && choice <= 4) {
                return 2;
            }

            // 5+
            return choicesLength < 4 ? 2 : 3;
        },

        en: (choice: any, choicesLength: any) => {
            if(choice === 0) {
                return 0;
            }

            if(choice === 1) {
                return 1;
            }

            if(choice >= 2 && choice <= 4) {
                return 2;
            }

            return choicesLength < 4 ? 2 : 3;
        }
    }
}