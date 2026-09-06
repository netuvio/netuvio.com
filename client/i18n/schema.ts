export type MessageSchema = {
    common: {
        comingSoon: string,
        lorem: {
            title: string,
            description: string,
            longDescription: string,
        },
    },

    home: {
        title: string,
        subtitle: string,
        learnMore: string,
        hero: string,
        techWeUse: string,
    },
    
    whoAreWe: {
        title: string,
        subtitle: string,
    },

    services: {
        design: string,
        development: string,
        hosting: string,
    },

    footer: {
        description: string,
        design: { title: string },
        development: { title: string },
        hosting: { title: string },
        about: {
            title: string,
            brief: string,
            services: string,
            process: string,
            projects: string,
            whyChooseUs: string,
            contactUs: string,
        },
        loremIpsum: string,
        allRightsReserved: string,
        privacyPolicy: string,
        termsOfService: string,
    },

    contact: {
        title: string,
        firstName: string,
        lastName: string,
        email: string,
        phoneNumber: string,
        phonePlaceholder: string,
        whatDoYouNeed: string,
        services: {
            design: string,
            development: string,
            hosting: string,
            other: string,
        },
        message: string,
        sending: string,
        send: string,
        countryFlag: string,
        validation: {
            firstNameRequired: string,
            lastNameRequired: string,
            emailRequired: string,
            emailInvalid: string,
            serviceRequired: string,
            messageRequired: string,
            phoneInvalid: string,
            maxLength: string,
        },
    },

    projects: {
        title: string,
        allProjects: string,
        allProjectsTitle: string,
        badge: string,
        subtitle: string,
        metaDescription: string,
        featuredProjects: string,
        viewAll: string,
        allFilter: string,
        noProjectsFound: string,
        resetFilter: string,
        stats: {
            customBuilt: string,
            performance: string,
            modernStack: string,
        },
        cta: {
            title: string,
            subtitle: string,
            button: string,
        },
        types: {
            website: string,
            graphicDesign: string,
        },
        learnMore: string,
        visitWebsite: string,
        sourceCode: string,
    },

    whyChooseUs: {
        title: string,
        flexibleServices: {
            title: string,
            description: string,
        },
        cta: {
            title: string,
            description: string,
            button: string,
        },
        modernTechnology: {
            title: string,
            description: string,
        },
        personalApproach: {
            title: string,
            description: string,
        },
    },

    process: {
        title: string,
        sections: {
            consultation: { title: string, description: string },
            choose: { title: string, description: string },
            build: { title: string, description: string },
            support: { title: string, description: string },
        },
    },

    header: {
        brief: string,
        services: string,
        process: string,
        projects: string,
        contact: string,
    },

    debug: {
        mobile: string,
        tablet: string,
        laptop: string,
        desktop: string,
    },

    error: {
        title: string,
        back: string,
        messages: {
            notFound: string,
            forbidden: string,
            serverError: string,
            unexpected: string,
        },
    },
}
