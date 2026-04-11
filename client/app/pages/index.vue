<script setup lang="ts">
import { Head, Title } from "#components";
import HeroSection from "~/components/home/HeroSection.vue";

const { t } = useI18n();
</script>

<template>
    <Head>
        <Title>{{ t("home.title") }} • Netuvio</Title>
    </Head>

    <div>
        <HeroSection/>
<!--        <div :class="$style.gradientBackground"></div>-->

        <section :class="$style.section1">
            <div :class="$style.gradient"></div>
        </section>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section1 {
    width: 100%;
    min-height: 400px;
    background-color: white;
    position: relative;
    $gradientHeight: 800px;

    .gradient {
        top: calc($gradientHeight / -2);
        position: absolute;
        width: 100%;
        background: radial-gradient(
            150% 100% at 50% 100%,
            #FFF 60%,
            var(--color-primary) 65%,
            black 80%,
            rgba(16, 17, 12, 0.00) 100%
        );
        height: $gradientHeight;
        pointer-events: none;
    }
}
</style>