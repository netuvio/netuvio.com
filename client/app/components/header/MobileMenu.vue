<script setup lang="ts">
import {ref} from "vue";
import type {HeaderLink} from "~/lib/types";
import { AnimatePresence, motion } from "motion-v";

const props = defineProps<{
    links: HeaderLink[]
}>();

const { t } = useI18n();

const isOpen = ref(false);
</script>

<template>
    <div>
        <button :class="[$style.menuButton, isOpen && $style.open]" @click="isOpen = !isOpen">
            <span></span>
            <span></span>
            <span></span>
        </button>
        <AnimatePresence>
            <motion.div
                :class="$style.mobileMenu"
                v-if="isOpen"
                key="mobileMenu"
                :initial="{
                opacity: 0,
            }"
                :animate="{
                opacity: 1,
            }"
                :exit="{
                opacity: 0,
            }"
                :transition="{ 
                duration: 0.2,
                ease: 'easeInOut'
            }"
            >
                <div class="container">
                    <ul>
                        <li v-for="link in props.links" :key="link.to">
                            <NuxtLinkLocale :to="link.to" @click="isOpen = false">{{ link.name }}</NuxtLinkLocale>
                        </li>
                    </ul>
                </div>
            </motion.div>
        </AnimatePresence>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.mobileMenu {
    position: fixed;
    inset: 0;
    display: none;
    background-color: var(--color-background-primary);
    z-index: 1000;
    
    > div {
        margin-top: 80px;
        
        ul {
            list-style: none;
            padding: 0;
            display: flex;
            flex-direction: column;
            align-items: center;
            font-weight: 600;
            
            li {
                width: 100%;
                
                a {
                    display: block;
                    padding: 16px 0;
                    width: 100%;
                    text-align: center;
                    color: var(--color-text-primary);
                }
            }
        }
    }
}

.menuButton {
    width: 24px;
    height: 20px;
    position: relative;
    display: inline-block;
    cursor: pointer;
    border: none;
    background: transparent;
    padding: 0;
    z-index: 1001;

    span {
        position: absolute;
        left: 0;
        width: 100%;
        height: 3px;
        background: white;
        border-radius: 2px;
        transition: all 0.3s ease;
    }

    span:nth-child(1) {
        top: 0;
    }

    span:nth-child(2) {
        top: 50%;
        transform: translateY(-50%);
    }

    span:nth-child(3) {
        bottom: 0;
    }
}

/* OPEN STATE */
.open {
    span:nth-child(1) {
        top: 50%;
        transform: translateY(-50%) rotate(45deg);
    }

    span:nth-child(2) {
        opacity: 0;
    }

    span:nth-child(3) {
        bottom: 50%;
        transform: translateY(50%) rotate(-45deg);
    }
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
    .mobileMenu {
        display: block;
    }
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>