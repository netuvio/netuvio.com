<script setup lang="ts">

import HeroSection from "~/components/home/HeroSection.vue";

const { t } = useI18n();
</script>

<template>
    <div>
        <HeroSection/>
        <div :class="$style.gradientBackground"></div>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.gradientBackground {
    position: absolute;
    top: 600px;
    width: 100vw;
    height: 150%;
    pointer-events: none;
    background: radial-gradient(
        241.39% 100% at 50% 100%,
        #FFF 60%, 
        var(--color-primary) 65%, 
        black 80%, 
        rgba(16, 17, 12, 0.00) 100%);
}
</style>