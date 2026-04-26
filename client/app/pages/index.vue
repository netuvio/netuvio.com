<script setup lang="ts">
import { Head, Title } from "#components";
import HeroSection from "~/components/home/HeroSection.vue";
import Section2 from "~/components/home/Section2.vue";
import WhatWeDoSection from "~/components/home/WhatWeDoSection.vue";
import Section4 from "~/components/home/Section4.vue";

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
        <Section4 />
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;
</style>