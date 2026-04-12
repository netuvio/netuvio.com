<script setup lang="ts">
import { onMounted, onUnmounted, ref } from "vue";

const triangleRef = ref<HTMLImageElement | null>(null);
const hexagonRef = ref<HTMLImageElement | null>(null);

const reduceMotion =
    typeof window !== "undefined" &&
    window.matchMedia("(prefers-reduced-motion: reduce)").matches;

type ShapeState = {
    x: number;
    y: number;
    rotate: number;
};

const current = {
    triangle: { x: 0, y: 0, rotate: 0 } as ShapeState,
    hexagon: { x: 0, y: 0, rotate: 0 } as ShapeState,
};

const target = {
    triangle: { x: 0, y: 0, rotate: 0 } as ShapeState,
    hexagon: { x: 0, y: 0, rotate: 0 } as ShapeState,
};

let rafId = 0;

const setTargetsFromPointer = (clientX: number, clientY: number) => {
    const nx = (clientX / window.innerWidth) * 2 - 1;
    const ny = (clientY / window.innerHeight) * 2 - 1;

    target.triangle.x = nx * 30;
    target.triangle.y = ny * 15;
    target.triangle.rotate = nx * 4 + ny * 2;

    target.hexagon.x = nx * 25;
    target.hexagon.y = ny * 12;
    target.hexagon.rotate = nx * -1.8 + ny * -3;
};

const mouseParallax = (e: MouseEvent) => {
    setTargetsFromPointer(e.clientX, e.clientY);
};

const resetTargets = () => {
    target.triangle.x = 0;
    target.triangle.y = 0;
    target.triangle.rotate = 0;

    target.hexagon.x = 0;
    target.hexagon.y = 0;
    target.hexagon.rotate = 0;
};

const animateParallax = () => {
    const easing = 0.06;

    current.triangle.x += (target.triangle.x - current.triangle.x) * easing;
    current.triangle.y += (target.triangle.y - current.triangle.y) * easing;
    current.triangle.rotate += (target.triangle.rotate - current.triangle.rotate) * easing;

    current.hexagon.x += (target.hexagon.x - current.hexagon.x) * easing;
    current.hexagon.y += (target.hexagon.y - current.hexagon.y) * easing;
    current.hexagon.rotate += (target.hexagon.rotate - current.hexagon.rotate) * easing;

    if (triangleRef.value) {
        triangleRef.value.style.transform = `translate3d(${current.triangle.x}px, ${current.triangle.y}px, 0) rotate(${current.triangle.rotate}deg)`;
    }

    if (hexagonRef.value) {
        hexagonRef.value.style.transform = `translate3d(${current.hexagon.x}px, ${current.hexagon.y}px, 0) rotate(${current.hexagon.rotate}deg)`;
    }

    rafId = window.requestAnimationFrame(animateParallax);
};

onMounted(() => {
    if (reduceMotion) {
        return;
    }

    window.addEventListener("mousemove", mouseParallax);
    window.addEventListener("mouseleave", resetTargets);
    window.addEventListener("blur", resetTargets);
    rafId = window.requestAnimationFrame(animateParallax);
});

onUnmounted(() => {
    window.removeEventListener("mousemove", mouseParallax);
    window.removeEventListener("mouseleave", resetTargets);
    window.removeEventListener("blur", resetTargets);
    if (rafId) {
        window.cancelAnimationFrame(rafId);
    }
});
</script>

<template>
    <div :class="$style.glass">
        <img
            ref="triangleRef"
            src="~/../public/images/glass-triangle-1.png"
            alt=""
            draggable="false"
            :class="$style.triangle"
        />
        <img
            ref="hexagonRef"
            src="~/../public/images/glass-hexagon-1.png"
            alt=""
            draggable="false"
            :class="$style.hexagon"
        />
    </div>
</template>

<style module lang="scss">
.glass {
    position: absolute;
    inset: 0;
    pointer-events: none;

    .triangle {
        position: absolute;
        top: 10%;
        left: 5%;
        will-change: transform;
    }

    .hexagon {
        position: absolute;
        left: 78%;
        top: 57%;
        will-change: transform;
    }
}
</style>