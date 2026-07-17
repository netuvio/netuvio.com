<script setup lang="ts">
import { onMounted } from 'vue';
import { useLightbox } from './useLightbox';
import IconMagnifyingGlassPlus from '~icons/fa6-solid/magnifying-glass-plus';

const props = withDefaults(defineProps<{
    src: string;
    width: number;
    height?: number;
    alt?: string;
    className?: string;
    imageClassName?: string;
}>(), {
    alt: '',
    className: '',
    imageClassName: ''
});

const { register, openBySrc } = useLightbox();

onMounted(() => {
    register({
        src: props.src,
        width: props.width,
        height: props.height || props.width,
        alt: props.alt
    });
});
</script>

<template>
    <div
        @click="openBySrc(src)"
        :class="[$style.wrapper, className]"
    >
        <NuxtImg
            :src="src"
            :alt="alt"
            :width="width"
            :height="height || width"
            :class="imageClassName"
            format="webp"
            draggable="false"
        />
        <div :class="[$style.overlay, className]">
            <IconMagnifyingGlassPlus style="font-size: 32px;" />
        </div>
        <div :class="[$style.skeleton, className]"></div>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.wrapper {
    cursor: pointer;
    position: relative;
    border-radius: 30px;
    overflow: hidden;
    
    img {
        object-fit: cover;
        width: 100%;
    }
}

.overlay {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: rgba(0, 0, 0, 0.2);
    opacity: 0;
    transition: opacity 0.3s;

    &:hover {
        opacity: 1;
    }
}

.skeleton {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    animation: pulse 2s cubic-bezier(0.4, 0, 0.6, 1) infinite;
    background-color: #d1d5db;
    z-index: -1;
}

@keyframes pulse {
    0%, 100% {
        opacity: 1;
    }
    50% {
        opacity: 0.5;
    }
}
</style>