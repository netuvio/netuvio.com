<template>
    <div :class="$style.gradient"></div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.gradient {
    --gradient-height: 700px;
    
    top: calc(var(--gradient-height) / -2);
    position: absolute;
    width: max(1000px, 101%);
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
    
    @media screen and (max-width: $mobileBreakpoint) {
        --gradient-height: 600px;
    }
}
</style>