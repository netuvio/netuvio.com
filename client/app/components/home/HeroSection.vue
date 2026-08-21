<script setup lang="ts">
import Button from "~/components/Button.vue";
import GlassShapes from "~/components/home/GlassShapes.vue";
import { motion, useScroll, useTransform } from 'motion-v'
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import StarrySky from "~/components/home/StarrySky.vue";
import TypingTextAnimation from "~/components/TypingTextAnimation.vue";

const { t, locale } = useI18n();

const { scrollYProgress } = useScroll();
const textScale = useTransform(scrollYProgress, [0, 0.45], [1, 0.82]);
const bgScale = useTransform(scrollYProgress, [0, 1.5], [1, 0.82]);

</script>

<template>
    <main :class="$style.heroWrapper">
        <div :class="$style.bg"></div>
        <main :class="[$style.hero, 'container']">
            <img :class="$style.bgName" src="/images/hero-bg-name.svg" alt="netuvio">
            <div :class="$style.dots">
                <img src="/images/dots.svg" alt="" />
                <img src="/images/dots.svg" alt="" />
                <img src="/images/dots.svg" alt="" />
            </div>
            <motion.div :class="$style.textContainer" :style="{ scale: textScale, transformOrigin: 'center center' }">
                <!-- nadpis -->
                <h1>
                    <RandomTextAnimation :text="t('home.hero')" :interval="10" />
                </h1>

                <!-- podnadpis -->
                <h2>
                    <TypingTextAnimation 
                        :text="t('home.subtitle')"
                        :delay=".7"
                    />
                </h2>
                <motion.div
                    :initial="{ opacity: 0, y: 10 }"
                    :animate="{ opacity: 1, y: 0 }"
                    :transition="{
                        duration: .4,
                        delay: 1.8,
                    }"
                >
                    <NuxtLinkLocale to="/#brief">
                        <Button size="xl" variant="tertiary">
                            {{ t("home.learnMore") }}
                        </Button>
                    </NuxtLinkLocale>
                </motion.div>
            </motion.div>
<!--            <div :class="$style.technologies">-->
<!--                <span>{{ t("home.techWeUse") }}</span>-->
<!--                <div>-->
<!--                    <img src="/icons/tech/dotnet.svg" alt="DotNet" />-->
<!--                    <img src="/icons/tech/vuejs.svg" alt="Vue.js" />-->
<!--                    <img src="/icons/tech/react.svg" alt="React" />-->
<!--                    <img src="/icons/tech/docker.svg" alt="Docker" />-->
<!--                    <img src="/icons/tech/postgresql.svg" alt="PostgreSQL" />-->
<!--                </div>-->
<!--            </div>-->
        </main>
        <div :class="$style.pageTransition"></div>
    </main>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.heroWrapper {
    width: 100%;
    height: 90vh;
    background-color: var(--color-primary);
    position: relative;
}

.bg {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-image: radial-gradient(circle at 50% 50%, hsl(from var(--color-lime-500) h s l / 0.1) 20%, hsl(from var(--color-lime-600) h s l / 0.3) 100%);
    mask-image: url("/patterns/topography-1.svg");
    mask-repeat: no-repeat;
    mask-size: cover;
    mask-position: center;
}

.hero {
    position: relative;
    padding-top: 240px;
    height: 100%;

    >.textContainer {
        width: min(100%, 100vw);
        display: flex;
        flex-direction: column;
        justify-content: center;
        color: var(--color-text-secondary);

        h1 {
            font-size: clamp(52px, 5.55vw, 80px);
            -webkit-text-stroke: 12px transparent;
            paint-order: stroke fill;
            font-weight: 800;
            background: linear-gradient(180deg, var(--color-text-primary), var(--color-lime-200));
            -webkit-background-clip: text;
            background-clip: text;
            width: fit-content;
        }

        h2 {
            font-weight: 600;
            margin-top: 20px;
            max-width: 700px;
        }

        button {
            margin-top: 20px;
            box-shadow: 0 0 64px 0 hsl(from var(--color-primary) h s l / 0.2);
            position: relative;
            z-index: 1;
        }
    }

    .bgName {
        position: absolute;
        inset: 0;
        z-index: 0;
        top: 60px;
        left: -50px;
        pointer-events: none;
        user-select: none;
        width: 100%;
        max-width: 1500px;
    }
    
    .technologies {
        position: absolute;
        left: 50%;
        bottom: 0;
        transform: translate(-50%, 50%);
        background-color: var(--color-background-primary);
        padding: 60px 60px;
        border-radius: 48px;
        width: 100%;
        z-index: 10;
        display: flex;
        justify-content: space-between;
        align-items: center;
        gap: 96px;

        span {
            text-transform: uppercase;
            font-size: 20px;
            font-weight: 600;
            white-space: pre-line;
        }
        
        >div {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 24px;
            width: 100%;

            img {
                height: 40px;
                object-fit: contain;
            }
        }
    }
    
    .dots {
        position: absolute;
        inset: 0;
        width: 100%;
        height: 100%;
        pointer-events: none;
        user-select: none;
        
        img {
            position: absolute;
            pointer-events: none;
            user-select: none;
        }

        img:nth-child(1) {
            top: 10%;
        }

        img:nth-child(2) {
            top: 20%;
            left: 35%;
        }

        img:nth-child(3) {
            top: 12%;
            right: 15%;
        }
    }
}

.pageTransition {
    position: absolute;
    bottom: 0;
    left: 0;
    height: 150px;
    width: 100%;
    pointer-events: none;
    user-select: none;
    background-image: url('/images/page-transition.svg');
    background-size: 900px;
}

// Laptops Responsive
@media screen and (max-width: $laptopBreakpoint) {    
    .hero {
        >.textContainer {
            h1 {
                font-size: clamp(50px, 6.8vw, 70px);
            }
            
            h2 {
                font-size: 20px;
                margin-top: 12px;
            }

            button {
                padding: 16px 24px;
                font-size: 22px;
                min-height: 0;
                margin-top: 32px;
            }
        }
    }
}

// tablet
@media screen and (max-width: $tabletBreakpoint) {
    .hero {
        padding-top: 48px;
        

        >.textContainer {
            h1 {
                font-size: clamp(38px, 6.5vw, 50px);
            }

            h2 {
                font-size: 16px;
            }

            button {
                padding: 16px 24px;
                font-size: 20px;
                min-height: 0;
                margin-top: 32px;
            }
        }
    }
}

// mobile
@media screen and (max-width: $mobileBreakpoint) {
    .hero {
        padding-top: 64px;

        >.textContainer {
            margin-top: -0px;
            
            h1 {
                font-size: clamp(30px, 10vw, 42px);
                width: min(100%, 720px);
            }

            h2 {
                font-size: 12px;
            }

            button {
                padding: 12px 20px;
                font-size: 16px;
                min-height: 0;
                margin-top: 20px;
            }
        }
    }
}
</style>
