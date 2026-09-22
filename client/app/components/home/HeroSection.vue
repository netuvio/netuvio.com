<script setup lang="ts">
import { Button } from "@netuvio/ui/vue";
import { motion, useScroll, useTransform } from 'motion-v'
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import TypingTextAnimation from "~/components/TypingTextAnimation.vue";

const { t } = useI18n();

const { scrollYProgress } = useScroll();
const textScale = useTransform(scrollYProgress, [0, 0.45], [1, 0.82]);

const techRowRef = ref<HTMLElement | null>(null);
const canScrollLeft = ref(false);
const canScrollRight = ref(false);

const checkTechScroll = () => {
    if (!techRowRef.value) return;
    const { scrollLeft, scrollWidth, clientWidth } = techRowRef.value;
    canScrollLeft.value = scrollLeft > 4;
    canScrollRight.value = scrollLeft + clientWidth < scrollWidth - 4;
};

const scrollTech = (dir: "left" | "right") => {
    if (!techRowRef.value) return;
    const distance = 180;
    techRowRef.value.scrollBy({
        left: dir === "left" ? -distance : distance,
        behavior: "smooth"
    });
};

onMounted(() => {
    nextTick(() => {
        checkTechScroll();
    });
    window.addEventListener("resize", checkTechScroll);
});

onUnmounted(() => {
    window.removeEventListener("resize", checkTechScroll);
});

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
                    <!-- Squiggle -->
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 155.04 73.442"
                        :class="$style.squiggle"
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
            <div :class="$style.technologies">
                <span>
                    {{ t("home.techWeUse") }}
                    <img :class="$style.squiggle" src="/images/squiggle-2.svg" alt="" />
                </span>
                <div :class="$style.techCarouselWrapper">
                    <button
                        type="button"
                        :class="[$style.scrollArrow, $style.scrollLeft, !canScrollLeft && $style.arrowDisabled]"
                        @click="scrollTech('left')"
                        aria-label="Scroll left"
                    >
                        <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                            <polyline points="15 18 9 12 15 6"></polyline>
                        </svg>
                    </button>
                    <div ref="techRowRef" :class="$style.techRow" @scroll="checkTechScroll">
                        <img src="/icons/tech/dotnet.svg" alt="DotNet" />
                        <img src="/icons/tech/vuejs.svg" alt="Vue.js" />
                        <img src="/icons/tech/react.svg" alt="React" />
                        <img src="/icons/tech/docker.svg" alt="Docker" />
                        <img src="/icons/tech/postgresql.svg" alt="PostgreSQL" />
                    </div>
                    <button
                        type="button"
                        :class="[$style.scrollArrow, $style.scrollRight, !canScrollRight && $style.arrowDisabled]"
                        @click="scrollTech('right')"
                        aria-label="Scroll right"
                    >
                        <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                            <polyline points="9 18 15 12 9 6"></polyline>
                        </svg>
                    </button>
                </div>
            </div>
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
    right: -200px;
    bottom: -200px;
    display: flex;
    justify-content: flex-end;
    align-items: center;
    pointer-events: none;
    user-select: none;
    z-index: 1;

    >img {
        width: 1200px;
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
        width: 1000px;
        height: 1000px;
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
            max-width: 600px;
            position: relative;
            
            .squiggle {
                position: absolute;
                top: -40px;
                left: 100%;
                width: 155px;
                height: 73px;
            }
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
        //max-width: 1500px;
    }
    
    .technologies {
        position: absolute;
        left: 50%;
        bottom: 0;
        transform: translate(-50%, 50%);
        background-color: var(--color-background-primary);
        padding: 50px 50px;
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
            position: relative;
            
            .squiggle {
                position: absolute;
                top: 100%;
                left: 0;
            }
        }
        
        .techCarouselWrapper {
            position: relative;
            display: flex;
            align-items: center;
            width: 100%;
            min-width: 0;

            .techRow {
                display: flex;
                align-items: center;
                justify-content: space-between;
                gap: 24px;
                width: 100%;
                overflow-x: auto;
                scrollbar-width: none;
                -ms-overflow-style: none;
                scroll-behavior: smooth;

                &::-webkit-scrollbar {
                    display: none;
                }

                img {
                    height: 40px;
                    object-fit: contain;
                    flex-shrink: 0;
                }
            }

            .scrollArrow {
                position: absolute;
                top: 50%;
                transform: translateY(-50%);
                z-index: 5;
                width: 38px;
                height: 38px;
                border-radius: 50%;
                display: none;
                align-items: center;
                justify-content: center;
                background: rgba(255, 255, 255, 0.12);
                backdrop-filter: blur(12px);
                -webkit-backdrop-filter: blur(12px);
                border: 1px solid rgba(255, 255, 255, 0.25);
                color: var(--color-primary);
                box-shadow: 0 4px 16px rgba(0, 0, 0, 0.4);
                cursor: pointer;
                transition: all 0.25s ease;
                padding: 0;

                &:hover {
                    background: rgba(255, 255, 255, 0.22);
                    transform: translateY(-50%) scale(1.08);
                }

                &.arrowDisabled {
                    opacity: 0;
                    pointer-events: none;
                }
            }

            .scrollLeft {
                left: 8px;
            }

            .scrollRight {
                right: 8px;
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
    z-index: 9;
}

@media screen and (max-width: $laptopBreakpoint) {    
    .heroWrapper {
        height: auto;
        min-height: 90vh;
    }

    .hero {
        padding-top: 180px;

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

        .technologies {
            padding: 40px;
            gap: 48px;
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .heroWrapper {
        height: auto;
        min-height: 85vh;
        padding-bottom: 80px;
    }

    .heroImageContainer {
        right: -100px;
        bottom: -100px;

        > img {
            width: 750px;
        }

        .blackHole {
            width: 650px;
            height: 650px;
        }
    }

    .hero {
        padding-top: 120px;
        height: auto;

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

        .technologies {
            position: relative;
            left: auto;
            bottom: auto;
            transform: none;
            margin-top: 60px;
            border-radius: 32px;
            padding: 28px 20px;
            gap: 20px;
            flex-direction: column;
            align-items: center;

            .techCarouselWrapper {
                .scrollArrow {
                    display: flex;
                }

                .techRow {
                    gap: 32px;
                    padding: 4px 50px;
                    justify-content: flex-start;
                }
            }
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .heroWrapper {
        height: auto;
        min-height: 100svh;
        padding-bottom: 40px;
    }

    .heroImageContainer {
        opacity: 0.2;
        right: -60px;
        bottom: 0;

        > img {
            width: 420px;
        }

        .blackHole {
            width: 380px;
            height: 380px;
        }
    }

    .hero {
        padding-top: 90px;
        height: auto;

        >.textContainer {
            margin-top: 0;
            
            h1 {
                font-size: clamp(30px, 10vw, 42px);
                width: min(100%, 720px);
            }

            h2 {
                font-size: 14px;

                .squiggle {
                    display: none;
                }
            }

            button {
                padding: 12px 20px;
                font-size: 16px;
                min-height: 0;
                margin-top: 20px;
            }
        }

        .technologies {
            position: relative;
            left: auto;
            bottom: auto;
            transform: none;
            margin-top: 40px;
            border-radius: 24px;
            padding: 20px 14px;
            gap: 16px;
            width: 100%;

            span {
                font-size: 16px;
                text-align: center;
            }

            .techCarouselWrapper {
                .scrollArrow {
                    display: flex;
                    width: 34px;
                    height: 34px;
                }

                .techRow {
                    gap: 24px;
                    padding: 4px 44px;
                    justify-content: flex-start;

                    img {
                        height: 30px;
                    }
                }
            }
        }
    }
}
</style>
