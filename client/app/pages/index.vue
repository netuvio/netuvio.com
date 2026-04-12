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
        <section :class="$style.section1">
            <div :class="$style.gradient"></div>
        </section>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section1 {
    $gradientHeight: 800px;
    
    width: 100%;
    min-height: 400px;
    background-color: white;
    position: relative;
    overflow-x: clip;

    .gradient {
        top: calc($gradientHeight / -2);
        position: absolute;
        width: max(1000px, 100%);
        left: 50%;
        transform: translateX(-50%);
        background: radial-gradient(
            150% 100% at 50% 100%,
            #FFF 50%,
            var(--color-primary) 55%,
            black 70%,
            rgba(16, 17, 12, 0.00) 100%
        );
        height: $gradientHeight;
        pointer-events: none;
    }
}
</style>