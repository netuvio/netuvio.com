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
    padding: 64px;
    display: flex;
    height: 640px;
    background: radial-gradient(600px at top left, var(--color-primary) 0%, transparent 100%);

    section:first-child {
        width: 60%;
        margin: auto 0;

        div {
            width: 70%;
            font-size: 20px;
            
            h2 {
                font-size: 48px;
                line-height: 56px;
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
        padding: 64px;
        background: linear-gradient(to top, var(--color-lime-300) 0%, var(--color-primary) 100%);
        border-radius: 32px;
        width: 512px;
        height: 512px;
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
</style>