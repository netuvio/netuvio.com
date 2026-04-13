<script setup lang="ts">

const { t } = useI18n();
</script>

<template>
    <section>
        <div :class="$style.gradient"></div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

section {
    --gradient-height: 700px;

    width: 100%;
    min-height: 1500px;
    background-color: white;
    position: relative;
    overflow-x: clip;

    .gradient {
        top: calc(var(--gradient-height) / -2);
        position: absolute;
        width: max(1000px, 100%);
        left: 50%;
        transform: translateX(-50%);
        background: radial-gradient(
            150% 100% at 50% 100%,
            #FFF 50%,
            var(--color-primary) 55%,
            black 75%,
            rgba(0, 0, 0, 0.7) 85%,
            transparent 100%
        );
        height: var(--gradient-height);
        pointer-events: none;
    }

    @media screen and (max-width: $mobileBreakpoint) {
        --gradient-height: 600px;
    }
}
</style>