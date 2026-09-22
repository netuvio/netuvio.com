<script setup lang="ts">
import { ref, onMounted, onUnmounted, nextTick } from 'vue';
import { motion } from 'motion-v';
import Title from "~/components/typography/Title.vue";
import WhoWeAreServiceCard from "~/components/home/WhoWeAreServiceCard.vue";

const { t } = useI18n();

const cardsRowRef = ref<HTMLElement | null>(null);
const canScrollLeft = ref(false);
const canScrollRight = ref(false);

const checkCardsScroll = () => {
    if (!cardsRowRef.value) return;
    const { scrollLeft, scrollWidth, clientWidth } = cardsRowRef.value;
    canScrollLeft.value = scrollLeft > 4;
    canScrollRight.value = scrollLeft + clientWidth < scrollWidth - 4;
};

const scrollCards = (dir: "left" | "right") => {
    if (!cardsRowRef.value) return;
    const distance = 360;
    cardsRowRef.value.scrollBy({
        left: dir === "left" ? -distance : distance,
        behavior: "smooth"
    });
};

onMounted(() => {
    nextTick(() => {
        checkCardsScroll();
    });
    window.addEventListener("resize", checkCardsScroll);
});

onUnmounted(() => {
    window.removeEventListener("resize", checkCardsScroll);
});

const cards = [
    {
        title: "Lorem ipsum dolor sit amet",
        image: "/images/who-we-are-card-1.svg",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    },
    {
        title: "Lorem ipsum dolor sit amet",
        image: "/images/who-we-are-card-1.svg",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    },
    {
        title: "Lorem ipsum dolor sit amet",
        image: "/images/who-we-are-card-1.svg",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    },
    {
        title: "Lorem ipsum dolor sit amet",
        image: "/images/who-we-are-card-1.svg",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    }
];
</script>

<template>
    <section class="theme-secondary" :class="$style.section" id="whoWeAre">
        <div :class="['container', $style.container]">
            <motion.div
                :initial="{ opacity: 0, y: 20 }"
                :whileInView="{ opacity: 1, y: 0 }"
                :inViewOptions="{ once: true }"
                :transition="{ duration: 0.5, ease: 'easeOut' }"
            >
                <Title>{{ t('whoWeAre.title') }}</Title>
            </motion.div>

            <motion.div
                :initial="{ opacity: 0, y: 30 }"
                :whileInView="{ opacity: 1, y: 0 }"
                :inViewOptions="{ once: true }"
                :transition="{ duration: 0.6, delay: 0.15, ease: 'easeOut' }"
            >
                <h2 :class="$style.subtitle" v-html="t('whoWeAre.subtitle')"></h2>
            </motion.div>

            <div :class="$style.cardsCarouselWrapper">
                <button
                    type="button"
                    :class="[$style.scrollArrow, $style.scrollLeft, !canScrollLeft && $style.arrowDisabled]"
                    @click="scrollCards('left')"
                    aria-label="Scroll left"
                >
                    <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                        <polyline points="15 18 9 12 15 6"></polyline>
                    </svg>
                </button>

                <section ref="cardsRowRef" :class="$style.cards" @scroll="checkCardsScroll">
                    <motion.div
                        v-for="(card, index) in cards"
                        :key="index"
                        :class="$style.cardWrapper"
                        :initial="{ opacity: 0, y: 40 }"
                        :whileInView="{ opacity: 1, y: 0 }"
                        :inViewOptions="{ once: true }"
                        :transition="{ duration: 0.55, delay: 0.2 + index * 0.1, ease: 'easeOut' }"
                    >
                        <WhoWeAreServiceCard
                            :title="card.title"
                            :image="card.image"
                        >
                            {{ card.description }}
                        </WhoWeAreServiceCard>
                    </motion.div>
                </section>

                <button
                    type="button"
                    :class="[$style.scrollArrow, $style.scrollRight, !canScrollRight && $style.arrowDisabled]"
                    @click="scrollCards('right')"
                    aria-label="Scroll right"
                >
                    <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                        <polyline points="9 18 15 12 9 6"></polyline>
                    </svg>
                </button>
            </div>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding: 130px 0 220px;
    
    .container {
        .subtitle {
            font-size: 48px;
            white-space: pre;
            line-height: 1.2;
            position: relative;
            width: max-content;
            margin-top: 16px;

            &::after {
                content: "";
                display: block;
                position: absolute;
                top: -50px;
                right: -50px;
                width: 85px;
                height: 73px;
                background-image: url("/images/star-1.svg");
                background-size: contain;
                background-repeat: no-repeat;
                background-position: center;
                z-index: -1;
            }

            span {
                position: relative;
                display: inline-block;

                &::before {
                    content: "";
                    display: block;
                    position: absolute;
                    left: 50%;
                    top: 50%;
                    transform: translate(-50%, -50%);
                    height: 64px;
                    width: 256px;
                    background-image: url("/images/squiggle-3.svg");
                    background-size: contain;
                    background-repeat: no-repeat;
                    background-position: center;
                    z-index: -1;
                }
            }
        }
        
        .cardsCarouselWrapper {
            position: relative;
            margin-top: 64px;
            width: 100%;

            .scrollArrow {
                position: absolute;
                top: 50%;
                transform: translateY(-50%);
                z-index: 10;
                width: 46px;
                height: 46px;
                border-radius: 50%;
                display: flex;
                align-items: center;
                justify-content: center;
                background: rgba(255, 255, 255, 0.25);
                backdrop-filter: blur(12px);
                -webkit-backdrop-filter: blur(12px);
                border: 1px solid rgba(255, 255, 255, 0.4);
                color: #000;
                box-shadow: 0 6px 20px rgba(0, 0, 0, 0.3);
                cursor: pointer;
                transition: all 0.25s ease;
                padding: 0;

                &:hover {
                    background: rgba(255, 255, 255, 0.45);
                    transform: translateY(-50%) scale(1.08);
                    color: #000;
                }

                &.arrowDisabled {
                    opacity: 0;
                    pointer-events: none;
                }
            }

            .scrollLeft {
                left: 12px;
            }

            .scrollRight {
                right: 12px;
            }
        }

        .cards {
            display: flex;
            gap: 32px;
            margin-top: 0;
            overflow-x: auto;
            flex-wrap: nowrap;
            scrollbar-width: none;
            -ms-overflow-style: none;
            scroll-behavior: smooth;
            scroll-snap-type: x mandatory;
            padding: 16px 8px;

            &::-webkit-scrollbar {
                display: none;
            }

            .cardWrapper {
                display: flex;
                flex-shrink: 0;
                scroll-snap-align: start;
            }
        }
    }
}

@media screen and (max-width: $laptopBreakpoint) {
    .section {
        padding: 120px 0 160px;

        .container {
            .cardsCarouselWrapper {
                margin-top: 48px;

                .scrollArrow {
                    width: 42px;
                    height: 42px;
                }

                .cards {
                    gap: 24px;
                }
            }
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .section {
        padding: 100px 0 120px;

        .container {
            .subtitle {
                font-size: clamp(32px, 4.5vw, 42px);
                white-space: normal;
                width: auto;
                max-width: 100%;

                &::after {
                    top: -30px;
                    right: 0;
                    width: 60px;
                    height: 52px;
                }
            }

            .cardsCarouselWrapper {
                margin-top: 40px;

                .scrollLeft {
                    left: 8px;
                }

                .scrollRight {
                    right: 8px;
                }

                .cards {
                    gap: 20px;
                }
            }
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .section {
        padding: 85px 0 90px;

        .container {
            .subtitle {
                font-size: clamp(26px, 8vw, 34px);
                white-space: normal;
                width: auto;
                max-width: 100%;

                &::after {
                    display: none;
                }

                span::before {
                    width: 100%;
                    height: 48px;
                }
            }

            .cardsCarouselWrapper {
                margin-top: 32px;

                .scrollArrow {
                    width: 38px;
                    height: 38px;
                }

                .scrollLeft {
                    left: 4px;
                }

                .scrollRight {
                    right: 4px;
                }

                .cards {
                    gap: 16px;
                    padding: 8px 4px;
                }
            }
        }
    }
}
</style>
