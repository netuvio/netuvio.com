<script setup lang="ts">
import { Head, Title } from "#components";
import HeroSection from "~/components/home/HeroSection.vue";
import Section2 from "~/components/home/Section2.vue";
import WhatWeDoSection from "~/components/home/WhatWeDoSection.vue";
import ProcessSection from "../components/home/ProcessSection.vue";
import FeaturedProjects from "~/components/home/FeaturedProjects.vue";
import WhyChooseUsSection from "~/components/home/WhyChooseUsSection.vue";
import ContactSection from "~/components/home/ContactSection.vue";

const { t } = useI18n();
</script>

<template>
    <Head>
        <Title>{{ t("home.title") }} • Netuvio</Title>
    </Head>

    <div>
        <HeroSection />
        <Section2 />
        <WhatWeDoSection />
        <ProcessSection /> 
        <FeaturedProjects />
        <WhyChooseUsSection />
        <ContactSection />
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;
</style>