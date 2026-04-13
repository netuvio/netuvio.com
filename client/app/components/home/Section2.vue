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
    $gradientHeight: 800px;

    width: 100%;
    min-height: 1500px;
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