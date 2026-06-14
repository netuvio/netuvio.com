<script setup lang="ts">
import { motion } from "motion-v";

const { t } = useI18n();

defineProps<{
    title: string;
    bulletPoints?: string[];
    image: string;
}>();
</script>

<template>
    <motion.div
        :class="$style.card"
        :initial="{ opacity: 0, y: 20 }"
        :animate="{ opacity: 1, y: 0 }"
        :exit="{ opacity: 0, y: 20 }"
        :transition="{ duration: 0.4, ease: 'easeInOut' }"
    >
        <section>
            <div>
                <motion.h2
                    :initial="{ opacity: 0, x: -20 }"
                    :animate="{ opacity: 1, x: 0 }"
                    :exit="{ opacity: 0, x: 20 }"
                    :transition="{ duration: 0.2, ease: 'easeInOut' }"
                >
                    {{title}}
                </motion.h2>
                <motion.p
                    :initial="{ opacity: 0, y: 20 }"
                    :animate="{ opacity: 1, y: 0 }"
                    :exit="{ opacity: 0, y: -20 }"
                    :transition="{ duration: 0.2, delay: 0.1, ease: 'easeInOut' }"
                >
                    <slot />
                </motion.p>
                <motion.ul
                    v-if="bulletPoints"
                    :initial="{ opacity: 0, y: 20 }"
                    :animate="{ opacity: 1, y: 0 }"
                    :exit="{ opacity: 0, y: -20 }"
                    :transition="{ duration: 0.2, delay: 0.2, ease: 'easeInOut' }"
                >
                    <li v-for="point in bulletPoints" :key="point">{{point}}</li>
                </motion.ul>
            </div>
        </section>
        <section>
            <motion.img 
                :src="image"  
                alt=""
                :initial="{ opacity: 0, scale: 0.9 }"
                :animate="{ opacity: 1, scale: 1 }"
                :exit="{ opacity: 0, scale: 0.9 }"
                :transition="{ duration: 0.3, delay: 0.1, ease: 'easeInOut' }"
            />
        </section>
    </motion.div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.card {
    outline: 1px solid hsla(0, 0%, 0%, 0.05);
    outline-offset: -2px;
    border-radius: 30px;
    padding: clamp(24px, 4.5vw, 64px);
    display: flex;
    min-height: 640px;
    height: auto;
    gap: clamp(24px, 4vw, 64px);
    background: radial-gradient(600px at top left, var(--color-primary) 0%, transparent 100%);

    section:first-child {
        width: 60%;
        margin: auto 0;

        div {
            width: min(70%, 620px);
            font-size: clamp(16px, 1.4vw, 20px);
            
            h2 {
                font-size: clamp(32px, 3.35vw, 48px);
                line-height: 1.16;
                margin-bottom: 16px;
            }
            
            p {
                opacity: .8;
            }
            
            ul {
                list-style: none;
                padding: 0;
                font-weight: 500;
                margin-top: 16px;
                
                li {
                    position: relative;
                    padding-left: 20px;
                    margin-top: 4px;
                    
                    &::before {
                        position: absolute;
                        content: '';
                        height: 6px;
                        width: 6px;
                        top: 50%;
                        left: 4px;
                        transform: translateY(-50%);
                        border-radius: 10000px;
                        background: var(--color-primary);
                        z-index: 0;
                    }
                }
            }
        }
    }

    section:last-child {
        margin-left: auto;
        padding: clamp(28px, 4.5vw, 64px);
        background: linear-gradient(to top, var(--color-lime-300) 0%, var(--color-primary) 100%);
        border-radius: 32px;
        width: min(40vw, 512px);
        aspect-ratio: 1;
        height: auto;
        display: flex;
        align-items: center;
        justify-content: center;
        flex-shrink: 0;
        
        img {
            box-shadow: 0 0 16px 0 rgba(0, 0, 0, 0.2);
            border-radius: 35px;
            max-width: 100%;
            max-height: 100%;
            object-fit: contain;
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .card {
        min-height: auto;
        flex-direction: column;

        section:first-child {
            width: 100%;

            div {
                width: 100%;
            }
        }

        section:last-child {
            width: min(100%, 420px);
            margin: 0 auto;
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .card {
        border-radius: 24px;

        section:last-child {
            border-radius: 24px;
            width: min(100%, 320px);
        }
    }
}
</style>
