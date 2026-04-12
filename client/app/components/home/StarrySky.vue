<script setup lang="ts">
import {onMounted, ref} from 'vue'

interface Comet {
    id: number;
    x: number;
    y: number;
    duration: number;
}

const comets = ref<Comet[]>([]);
let nextId = 0;
let timerId: ReturnType<typeof setTimeout>;

const removeComet = (id: number) => {
    comets.value = comets.value.filter(c => c.id !== id)
}

const scheduleNextComet = () => {
    const randomDelay = 2000 + Math.random() * 8000;

    timerId = setTimeout(() => {
        if (!document.hidden) {
            const x = Math.random() * window.innerWidth - 200;

            comets.value.push({
                id: nextId++,
                x,
                y: -100,
                duration: 1 + Math.random() * 0.5
            });
        }

        scheduleNextComet();

    }, randomDelay);
}

onMounted(() => {
    scheduleNextComet();
});

onUnmounted(() => {
    clearTimeout(timerId);
});
</script>

<template>
    <div :class="$style.sky">
        <div :class="$style.stars"></div>

        <div
            v-for="comet in comets"
            :key="comet.id"
            :class="$style.comet"
            :style="{ 
                left: `${comet.x}px`,
                top: `${comet.y}px`,
                animationDuration: `${comet.duration}s`
            }"
            @animationend="removeComet(comet.id)"
        ></div>
    </div>
</template>

<style module lang="scss">
.sky {
    position: absolute;
    inset: 0;
    overflow: hidden;
}

.comet {
    position: absolute;
    width: 200px;
    height: 2px;
    background: linear-gradient(90deg, rgba(255, 255, 255, 0) 0%, rgba(255, 255, 255, .5) 100%);
    animation: cometMove linear forwards;
    pointer-events: none;
}

@keyframes cometMove {
    $cometAngle: 50deg;
    $cometHorizontalDistance: 3000px;
    
    0% {
        transform: translate(0, 0) rotate($cometAngle);
        opacity: 1;
    }
    100% {
        transform: translate($cometHorizontalDistance, calc($cometHorizontalDistance * tan($cometAngle))) rotate($cometAngle);
        opacity: 0;
    }
}

.stars {
    --flicker-subtlety: 1;
    position: absolute;
    inset: 0;
    pointer-events: none;
    overflow: hidden;
    background-image: url("/images/starry-sky.png");
    background-size: 25%;
    opacity: calc(0.46 + (var(--flicker-subtlety) * 0.03));
    animation: starFlickerBase 5.1s ease-in-out infinite;
    will-change: opacity, filter;

    &::before,
    &::after {
        content: "";
        position: absolute;
        inset: 0;
        background-image: inherit;
        background-repeat: repeat;
        pointer-events: none;
    }

    &::before {
        background-size: 23%;
        opacity: calc(0.33 + (var(--flicker-subtlety) * 0.03));
        animation: starFlickerNear 4.1s ease-in-out infinite;
        animation-delay: -1.2s;
    }

    &::after {
        background-size: 28%;
        opacity: calc(0.2 + (var(--flicker-subtlety) * 0.04));
        animation: starFlickerFar 5.8s ease-in-out infinite;
        animation-delay: -2s;
    }
}

@keyframes starFlickerBase {
    0%,
    100% {
        opacity: 0.46;
        filter: brightness(0.95);
    }
    24% {
        opacity: calc(0.46 + (var(--flicker-subtlety) * 0.08));
        filter: brightness(calc(0.95 + (var(--flicker-subtlety) * 0.13)));
    }
    27% {
        opacity: calc(0.46 + (var(--flicker-subtlety) * 0.12));
        filter: brightness(calc(0.95 + (var(--flicker-subtlety) * 0.18)));
    }
    52% {
        opacity: 0.46;
        filter: brightness(calc(0.95 + (var(--flicker-subtlety) * 0.03)));
    }
    76% {
        opacity: calc(0.46 + (var(--flicker-subtlety) * 0.1));
        filter: brightness(calc(0.95 + (var(--flicker-subtlety) * 0.15)));
    }
}

@keyframes starFlickerNear {
    0%,
    100% {
        opacity: 0.32;
        filter: brightness(0.95);
    }
    16% {
        opacity: calc(0.32 + (var(--flicker-subtlety) * 0.08));
        filter: brightness(calc(0.95 + (var(--flicker-subtlety) * 0.14)));
    }
    34% {
        opacity: calc(0.32 + (var(--flicker-subtlety) * 0.13));
        filter: brightness(calc(0.95 + (var(--flicker-subtlety) * 0.21)));
    }
    49% {
        opacity: 0.32;
        filter: brightness(calc(0.95 + (var(--flicker-subtlety) * 0.03)));
    }
    77% {
        opacity: calc(0.32 + (var(--flicker-subtlety) * 0.11));
        filter: brightness(calc(0.95 + (var(--flicker-subtlety) * 0.17)));
    }
}

@keyframes starFlickerFar {
    0%,
    100% {
        opacity: 0.2;
        filter: brightness(0.93);
    }
    20% {
        opacity: calc(0.2 + (var(--flicker-subtlety) * 0.08));
        filter: brightness(calc(0.93 + (var(--flicker-subtlety) * 0.12)));
    }
    22% {
        opacity: calc(0.2 + (var(--flicker-subtlety) * 0.14));
        filter: brightness(calc(0.93 + (var(--flicker-subtlety) * 0.2)));
    }
    63% {
        opacity: 0.2;
        filter: brightness(calc(0.93 + (var(--flicker-subtlety) * 0.03)));
    }
    82% {
        opacity: calc(0.2 + (var(--flicker-subtlety) * 0.12));
        filter: brightness(calc(0.93 + (var(--flicker-subtlety) * 0.15)));
    }
}

@media (prefers-reduced-motion: reduce) {
    .stars,
    .stars::before,
    .stars::after {
        animation: none;
        filter: none;
    }
}
</style>