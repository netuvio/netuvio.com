<script setup lang="ts">
import { ref, provide, onMounted, onUnmounted, watch, computed } from 'vue';
import { LightboxKey, type LightboxItemData } from './useLightbox';
import IconChevronLeft from '~icons/fa6-solid/chevron-left';
import IconChevronRight from '~icons/fa6-solid/chevron-right';
import IconXmark from '~icons/fa6-solid/xmark';

const items = ref<LightboxItemData[]>([]);
const open = ref(false);
const index = ref(0);
const direction = ref<1 | -1>(1);

const transform = ref({ scale: 1, x: 0, y: 0 });

const register = (item: LightboxItemData) => {
    if (!items.value.some(i => i.src === item.src)) {
        items.value.push(item);
    }
};

const openAt = (idx: number) => {
    index.value = idx;
    open.value = true;
    transform.value = { scale: 1, x: transform.value.x, y: transform.value.y };
};

const openBySrc = (src: string) => {
    const foundIndex = items.value.findIndex(item => item.src === src);
    if (foundIndex >= 0) openAt(foundIndex);
};

// Provide context to children
provide(LightboxKey, { register, openAt, openBySrc });

const close = () => { open.value = false; };

const prev = () => {
    if (items.value.length === 0) return;
    direction.value = -1;
    index.value = (index.value - 1 + items.value.length) % items.value.length;
    transform.value = { scale: 1, x: transform.value.x, y: transform.value.y };
};

const next = () => {
    if (items.value.length === 0) return;
    direction.value = 1;
    index.value = (index.value + 1) % items.value.length;
    transform.value = { scale: 1, x: transform.value.x, y: transform.value.y };
};

const handleKeyDown = (e: KeyboardEvent) => {
    if (!open.value) return;
    switch (e.key) {
        case 'Escape': close(); break;
        case 'ArrowLeft': prev(); break;
        case 'ArrowRight': next(); break;
    }
};

const toggleZoom = (e: MouseEvent) => {
    if (transform.value.scale === 1) {
        transform.value = { scale: 3, x: e.offsetX, y: e.offsetY };
    } else {
        transform.value = { scale: 1, x: transform.value.x, y: transform.value.y };
    }
};

const prevent = (e: Event) => e.preventDefault();

watch(open, (isOpen) => {
    if (isOpen) {
        document.body.addEventListener('wheel', prevent, { passive: false });
        document.body.addEventListener('touchmove', prevent, { passive: false });
    } else {
        document.body.removeEventListener('wheel', prevent);
        document.body.removeEventListener('touchmove', prevent);
    }
});

onMounted(() => window.addEventListener('keydown', handleKeyDown));
onUnmounted(() => {
    window.removeEventListener('keydown', handleKeyDown);
    document.body.removeEventListener('wheel', prevent);
    document.body.removeEventListener('touchmove', prevent);
});

const slideTransitionName = computed(() => direction.value === 1 ? 'slide-right' : 'slide-left');
</script>

<template>
    <slot />

    <Teleport to="body">
        <Transition name="fade">
            <div v-if="open" :class="$style.lightbox">

                <button :class="$style.closeBtn" @click="close">
                    <IconXmark />
                </button>

                <template v-if="items.length > 1">
                    <button :class="[$style.navBtn, $style.navBtnLeft]" @click="prev">
                        <IconChevronLeft />
                    </button>
                    <button :class="[$style.navBtn, $style.navBtnRight]" @click="next">
                        <IconChevronRight />
                    </button>
                    <span :class="$style.counter">{{ index + 1 }}/{{ items.length }}</span>
                </template>

                <div :class="$style.contentWrapper">
                    <div :class="$style.spacer"></div>

                    <div :class="$style.imageArea">
                        <div :class="$style.backdrop" @click="close"></div>

                        <Transition :name="slideTransitionName" mode="out-in">
                            <img
                                :key="items[index]?.src"
                                :src="items[index]?.src"
                                :alt="items[index]?.alt || ''"
                                @click.stop="toggleZoom"
                                :class="[$style.image, transform.scale > 1 ? $style.zoomOut : $style.zoomIn]"
                                :style="{
                                    transform: `scale(${transform.scale})`,
                                    transformOrigin: `${transform.x}px ${transform.y}px`,
                                }"
                                draggable="false"
                            />
                        </Transition>
                    </div>

                    <span :class="$style.caption">
                        {{ items[index]?.alt }}
                    </span>
                </div>
            </div>
        </Transition>
    </Teleport>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.lightbox {
    position: fixed;
    inset: 0;
    background-color: rgba(0, 0, 0, 0.75);
    backdrop-filter: blur(4px);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 2000;
}

.closeBtn {
    position: absolute;
    top: 1rem;
    right: 1rem;
    color: white;
    font-size: 1.875rem;
    padding: 0.5rem;
    z-index: 10;
    cursor: pointer;
    background: transparent;
    border: none;
}

.navBtn {
    position: absolute;
    color: white;
    font-size: 1.875rem;
    padding: 0.5rem;
    z-index: 10;
    cursor: pointer;
    background: transparent;
    border: none;
}

.navBtnLeft {
    left: 1rem;
}

.navBtnRight {
    right: 1rem;
}

.counter {
    position: absolute;
    left: 1.5rem;
    top: 1.5rem;
    z-index: 10;
    color: white;
}

.contentWrapper {
    max-width: 90vw;
    height: 100%;
    position: relative;
    display: flex;
    flex-direction: column;
    align-items: center;
    inset: 0;
    justify-content: space-between;
}

.spacer {
    height: 10%;
}

.imageArea {
    width: 100vw;
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    z-index: 5;
}

.backdrop {
    position: absolute;
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
}

.image {
    width: auto !important;
    max-height: 80%;
    max-width: 90%;
    position: relative !important;
    object-fit: contain;
    transition: transform 0.3s ease-out;
}

.zoomOut {
    cursor: zoom-out;
}

.zoomIn {
    cursor: zoom-in;
}

.caption {
    text-align: center;
    width: 100%;
    height: 10%;
    display: grid;
    align-content: center;
    z-index: 10;
    color: white;
}
</style>

<style scoped>
.fade-enter-active, .fade-leave-active { transition: opacity 0.3s ease; }
.fade-enter-from, .fade-leave-to { opacity: 0; }

.slide-right-enter-active, .slide-right-leave-active,
.slide-left-enter-active, .slide-left-leave-active { transition: all 0.3s ease-out; }

.slide-right-enter-from { transform: translateX(100px); opacity: 0; }
.slide-right-leave-to { transform: translateX(-100px); opacity: 0; }

.slide-left-enter-from { transform: translateX(-100px); opacity: 0; }
.slide-left-leave-to { transform: translateX(100px); opacity: 0; }
</style>