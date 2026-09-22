<script setup lang="ts">
import { Button } from "@netuvio/ui/vue";
import { motion, useScroll, useTransform } from 'motion-v'
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import TypingTextAnimation from "~/components/TypingTextAnimation.vue";

const { t } = useI18n();

const { scrollYProgress } = useScroll();
const textScale = useTransform(scrollYProgress, [0, 0.45], [1, 0.82]);
</script>

<template>
    <main :class="$style.heroWrapper">
        <div :class="$style.bg"></div>
        <div :class="$style.heroImageContainer">
            <img src="/images/hero-image.png" :class="$style.heroImage" alt="Hero Image" />
            <div :class="$style.blackHole"></div>
        </div>
        <main :class="[$style.hero, 'container']">
            <img :class="$style.bgName" src="/images/hero-bg-name.svg" alt="netuvio">
            <div :class="$style.dots">
                <img src="/images/dots.svg" alt="" />
                <img src="/images/dots.svg" alt="" />
                <img src="/images/dots.svg" alt="" />
            </div>
            <motion.div :class="$style.textContainer" :style="{ scale: textScale, transformOrigin: 'center center' }">
                <!-- Squiggle -->
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 155.04 73.442"
                    :class="$style.heroSquiggle"
                >
                    <motion.path
                        stroke="#000"
                        stroke-linecap="round"
                        stroke-width="5.6"
                        d="M2.8 70.642s50.851-46.004 47.239-38.376c-3.612 7.628-41.307 25.497-23.296 23.347 18.011-2.15 21.079-18.828 45.495-27.669 24.417-8.84-44.735 28.412-34.631 33.66s34.042-34.729 78.579-50.098c44.537-15.369-97.689 61.553-53.113 55.605 44.576-5.948 67.848-68.955 85.268-64.048 17.42 4.906-29.431 39.878-29.431 39.878"
                        :initial="{ pathLength: 0, opacity: 0 }"
                        :animate="{ pathLength: 1, opacity: 1 }"
                        :transition="{
                            opacity: { 
                                duration: 0.2, 
                                ease: 'easeInOut',
                                delay: 1.8
                            },
                            pathLength: { 
                                duration: 1.2, 
                                ease: 'easeInOut',
                                delay: 1.9
                            }
                        }"
                    />
                </svg>

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
                            <DrawnArrow />
                        </Button>
                    </NuxtLinkLocale>
                </motion.div>
            </motion.div>
        </main>

        <div :class="['container', $style.technologiesAnchor]">
            <div :class="$style.technologies">
                <span>
                    {{ t("home.techWeUse") }}
                    <img :class="$style.squiggle" src="/images/squiggle-2.svg" alt="" />
                </span>
                <div :class="$style.techRow">
                    <img src="/icons/tech/dotnet.svg" alt="DotNet" />
                    <img src="/icons/tech/vuejs.svg" alt="Vue.js" />
                    <img src="/icons/tech/react.svg" alt="React" />
                    <img src="/icons/tech/docker.svg" alt="Docker" />
                    <img src="/icons/tech/postgresql.svg" alt="PostgreSQL" />
                </div>
            </div>
        </div>

        <div :class="$style.pageTransition"></div>
    </main>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.heroWrapper {
    width: 100%;
    min-height: 90vh;
    background-color: var(--color-primary);
    position: relative;
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.bg {
    position: absolute;
    inset: 0;
    width: 100%;
    height: 100%;
    background-image: radial-gradient(circle at 50% 50%, hsl(from var(--color-lime-500) h s l / 0.1) 20%, hsl(from var(--color-lime-600) h s l / 0.3) 100%);
    mask-image: url("/patterns/topography-1.svg");
    mask-repeat: repeat;
    mask-size: auto 100%;
    pointer-events: none;
}

.heroImageContainer {
    position: absolute;
    right: -120px;
    bottom: -120px;
    display: flex;
    justify-content: flex-end;
    align-items: center;
    pointer-events: none;
    user-select: none;
    z-index: 1;

    >img {
        width: 950px;
        height: auto;
        object-fit: contain;
        pointer-events: none;
        user-select: none;
        z-index: 1;
    }
    
    .blackHole {
        position: absolute;
        bottom: 0;
        right: 0;
        width: 83.33%;
        aspect-ratio: 1;
        border-radius: 50%;
        background: var(--color-background-primary);
        box-shadow: 0 0 64px 0 hsl(from var(--color-primary) h s l / 0.2);
        
        &::before {
            content: '';
            position: absolute;
            inset: 0;
            border-radius: 50%;
            background-image: radial-gradient(circle closest-side, white 90%, transparent 100%);
            z-index: -1;
            transform: scale(1.1);
        }
    }
}

.hero {
    position: relative;
    padding-top: 180px;
    padding-bottom: 120px;
    width: 100%;

    >.textContainer {
        width: min(100%, 100vw);
        display: flex;
        flex-direction: column;
        justify-content: center;
        color: var(--color-text-secondary);
        position: relative;
        z-index: 2;

        .heroSquiggle {
            position: absolute;
            top: -75px;
            left: 10px;
            width: 130px;
            height: auto;
            pointer-events: none;
        }

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
            max-width: 600px;
            position: relative;
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

.technologiesAnchor {
    position: absolute;
    bottom: 0;
    left: 50%;
    transform: translate(-50%, 50%);
    z-index: 10;
    width: 100%;
    pointer-events: auto;
}

.technologies {
    background-color: var(--color-background-primary);
    padding: 42px 48px;
    border-radius: 40px;
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 36px;
    box-shadow: 0 16px 32px rgba(0, 0, 0, 0.4);

    span {
        text-transform: uppercase;
        font-size: 19px;
        font-weight: 600;
        line-height: 1.18;
        white-space: pre-line;
        position: relative;
        flex-shrink: 0;
        
        .squiggle {
            position: absolute;
            top: calc(100% + 4px);
            left: 0;
            width: 86px;
            height: auto;
            pointer-events: none;
        }
    }
    
    .techRow {
        display: flex;
        align-items: center;
        align-self: center;
        justify-content: flex-end;
        gap: 32px;
        flex-wrap: nowrap;

        img {
            height: 36px;
            width: auto;
            object-fit: contain;
            transition: transform 0.2s ease;

            @media (hover: hover) {
                &:hover {
                    transform: scale(1.08);
                }
            }
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
    z-index: 9;
}

@media screen and (max-width: $laptopBreakpoint) {    
    .heroWrapper {
        min-height: 85vh;
    }

    .hero {
        padding-top: 160px;
        padding-bottom: 100px;

        >.textContainer {
            max-width: 560px;

            .heroSquiggle {
                top: -68px;
                left: 10px;
                width: 115px;
            }

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

    .technologies {
        padding: 34px 36px;
        border-radius: 32px;
        gap: 28px;

        span {
            font-size: 16px;
            line-height: 1.2;

            .squiggle {
                width: 72px;
                top: calc(100% + 3px);
            }
        }

        .techRow {
            align-self: center;
            flex-wrap: nowrap;
            gap: 22px;

            img {
                height: 30px;
            }
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .heroWrapper {
        min-height: auto;
    }

    .hero {
        padding-top: 120px;
        padding-bottom: 90px;
        height: auto;

        >.textContainer {
            max-width: min(440px, 52vw);

            .heroSquiggle {
                top: -58px;
                left: 5px;
                width: 100px;
            }

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

    .technologiesAnchor {
        padding: 0 clamp(16px, 3vw, 24px);
    }

    .technologies {
        border-radius: 24px;
        padding: 30px 24px;
        gap: 20px;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        text-align: left;

        span {
            font-size: 13px;
            line-height: 1.18;
            letter-spacing: 0.02em;
            text-align: left;

            .squiggle {
                display: block;
                position: absolute;
                top: calc(100% + 3px);
                left: 0;
                width: 62px;
                height: auto;
                pointer-events: none;
            }
        }

        .techRow {
            align-self: center;
            justify-content: flex-end;
            gap: clamp(14px, 2vw, 20px);
            flex-wrap: nowrap;

            img {
                height: 24px;
                max-width: 44px;
            }
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .heroWrapper {
        min-height: auto;
    }

    .hero {
        padding-top: 90px;
        padding-bottom: 80px;
        height: auto;

        >.textContainer {
            margin-top: 0;

            .heroSquiggle {
                top: -42px;
                left: 0;
                width: 80px;
            }
            
            h1 {
                font-size: clamp(30px, 10vw, 42px);
                width: min(100%, 720px);
            }

            h2 {
                font-size: 14px;
            }

            button {
                padding: 12px 20px;
                font-size: 16px;
                min-height: 0;
                margin-top: 20px;
            }
        }
    }

    .technologiesAnchor {
        padding: 0 16px;
    }

    .technologies {
        border-radius: 16px;
        padding: 12px 16px;
        gap: 10px;
        flex-direction: column;
        align-items: center;
        text-align: center;
        width: 100%;
        box-shadow: 0 8px 24px rgba(0, 0, 0, 0.35);

        span {
            font-size: 11px;
            font-weight: 600;
            letter-spacing: 0.05em;
            white-space: normal;
            line-height: 1.2;
            text-align: center;
            color: var(--color-carbon-100);

            .squiggle {
                display: none;
            }
        }

        .techRow {
            display: flex;
            align-items: center;
            justify-content: center;
            gap: clamp(12px, 3.8vw, 18px);
            flex-wrap: nowrap;
            width: 100%;

            img {
                height: clamp(18px, 4.8vw, 22px);
                width: auto;
                max-width: 40px;
                object-fit: contain;
                flex-shrink: 0;
            }
        }
    }
}

@media screen and (max-width: 380px) {
    .technologiesAnchor {
        padding: 0 10px;
    }

    .technologies {
        padding: 10px 12px;
        border-radius: 14px;
        gap: 8px;

        span {
            font-size: 10px;
            letter-spacing: 0.04em;
        }

        .techRow {
            gap: 10px;

            img {
                height: 17px;
                max-width: 32px;
            }
        }
    }
}


/* --- Hero Image Responsive Breakpoints --- */

@media screen and (max-width: 1600px) {
    .heroImageContainer {
        right: -170px;
        bottom: -170px;
        > img { width: 1050px; }
    }
}

@media screen and (max-width: 1380px) {
    .heroImageContainer {
        right: -140px;
        bottom: -140px;
        > img { width: 920px; }
    }
}

@media screen and (max-width: 1200px) {
    .heroImageContainer {
        right: -130px;
        bottom: -130px;
        > img { width: 800px; }
    }
}

@media screen and (max-width: 1050px) {
    .heroImageContainer {
        right: -140px;
        bottom: -120px;
        > img { width: 680px; }
    }
}

@media screen and (max-width: 960px) {
    .heroImageContainer {
        right: -150px;
        bottom: -110px;
        > img { width: 580px; }
    }
}

@media screen and (max-width: 840px) {
    .heroImageContainer {
        right: -130px;
        bottom: -90px;
        > img { width: 500px; }
    }
}

@media screen and (max-width: 720px) {
    .heroImageContainer {
        right: -110px;
        bottom: -80px;
        > img { width: 440px; }
    }
}

@media screen and (max-width: 600px) {
    .heroImageContainer {
        opacity: 0.22;
        right: -50px;
        bottom: 0;
        > img { width: 450px; }
    }
}

@media screen and (max-width: 450px) {
    .heroImageContainer {
        right: -30px;
        > img { width: 380px; }
    }
}

@media screen and (max-width: 380px) {
    .heroImageContainer {
        right: -20px;
        > img { width: 320px; }
    }
}
</style>
