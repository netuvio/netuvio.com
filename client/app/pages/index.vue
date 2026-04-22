<script setup lang="ts">
import { Head, Title } from "#components";
import HeroSection from "~/components/home/HeroSection.vue";
import Section2 from "~/components/home/Section2.vue";
import Section3 from "~/components/home/Section3.vue";

const { t } = useI18n();
</script>

<template>
    <Head>
        <Title>{{ t("home.title") }} • Netuvio</Title>
    </Head>

    <div :class="$style.test">
        <HeroSection />
        <Section2 />
        <Section3 />
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.test {
    padding-bottom: 1000px;
}
</style>