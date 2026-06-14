<script setup lang="ts">
</script>
<template>
    <div :class="$style.gradientWrapper">
        <div :class="[$style.gradient]"></div>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.gradientWrapper {
    position: relative;
    height: 0;
    width: 100%;
}

.gradient {
    --gradient-height: 700px;
    
    top: calc(var(--gradient-height) / -2);
    position: absolute;
    width: 100vw;
    left: 50%;
    transform: translateX(-50%);
    background: radial-gradient(
        150% 100% at 50% 100%,
        transparent 50%,
        var(--color-primary) 55%,
        black 75%,
        rgba(0, 0, 0, 0.7) 85%,
        transparent 100%
    );
    height: var(--gradient-height);
    pointer-events: none;
    
    @media screen and (max-width: $mobileBreakpoint) {
        --gradient-height: 600px;
    }
}
</style>
