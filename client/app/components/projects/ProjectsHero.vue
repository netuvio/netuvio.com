<script setup lang="ts">
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import { motion } from "motion-v";

const { t } = useI18n();
</script>

<template>
    <section :class="[$style.heroSection, 'theme-primary']" id="projects-hero">
        <div :class="$style.topographyBg"></div>
        <div :class="$style.ambientGlow"></div>

        <div :class="$style.bgWatermark" aria-hidden="true">
            NETUVIO // PROJECTS
        </div>

        <div :class="['container', $style.heroContainer]">
            <motion.div 
                :class="$style.heroText"
                :initial="{ opacity: 0, y: 30 }"
                :animate="{ opacity: 1, y: 0 }"
                :transition="{ duration: 0.7, ease: 'easeOut' }"
            >
                <div :class="$style.titleWrapper">
                    <h1 :class="$style.title">
                        <RandomTextAnimation 
                            :text="t('projects.allProjects')" 
                            :interval="15" 
                        />
                    </h1>
                    <span :class="$style.titleStar" />
                </div>

                <div :class="$style.subtitleWrapper">
                    <p :class="$style.subtitle">{{ t("projects.subtitle") }}</p>
                </div>
            </motion.div>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.heroSection {
    position: relative;
    padding-top: 180px;
    min-height: 80vh;
    display: flex;
    align-items: center;
    overflow: hidden;
    background-color: var(--color-background-primary);

    .topographyBg {
        position: absolute;
        inset: 0;
        width: 100%;
        height: 100%;
        background-image: radial-gradient(circle at 60% 40%, hsl(from var(--color-primary) h s l / 0.12) 10%, transparent 80%);
        mask-image: url("/patterns/topography-1.svg");
        mask-repeat: repeat;
        mask-size: 800px;
        opacity: 0.65;
        pointer-events: none;
        z-index: 1;
    }

    .ambientGlow {
        position: absolute;
        top: 20%;
        left: 45%;
        width: 700px;
        height: 700px;
        border-radius: 50%;
        background: radial-gradient(circle, hsl(from var(--color-primary) h s l / 0.16) 0%, transparent 70%);
        pointer-events: none;
        filter: blur(60px);
        z-index: 1;
    }

    .bgWatermark {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        font-size: clamp(80px, 14vw, 220px);
        font-weight: 900;
        letter-spacing: -2px;
        white-space: nowrap;
        color: transparent;
        -webkit-text-stroke: 1.5px rgba(255, 255, 255, 0.04);
        user-select: none;
        pointer-events: none;
        z-index: 1;
    }

    .heroContainer {
        position: relative;
        z-index: 4;

        .heroText {
            display: flex;
            flex-direction: column;
            align-items: flex-start;
            max-width: 900px;
            gap: 20px;

            .titleWrapper {
                position: relative;
                display: inline-flex;
                align-items: center;

                .title {
                    font-size: clamp(48px, 7vw, 84px);
                    font-weight: 900;
                    line-height: 1.15;
                    color: var(--color-text-primary);
                    margin: 0;
                    letter-spacing: -1.5px;
                    background: linear-gradient(180deg, var(--color-text-primary) 30%, var(--color-lime-200) 100%);
                    -webkit-background-clip: text;
                    background-clip: text;
                    -webkit-text-fill-color: transparent;
                }

                .titleStar {
                    position: absolute;
                    top: -24px;
                    right: -48px;
                    width: 52px;
                    height: 52px;
                    animation: starSpin 20s infinite linear;
                }
            }

            .subtitleWrapper {
                position: relative;
                max-width: 720px;

                .subtitle {
                    font-size: clamp(18px, 1.6vw, 24px);
                    line-height: 1.55;
                    font-weight: 500;
                    color: var(--color-carbon-100);
                    margin: 0;
                }
            }
        }
    }
}

@keyframes starSpin {
    from {
        transform: rotate(0deg);
    }
    to {
        transform: rotate(360deg);
    }
}

@media screen and (max-width: $laptopBreakpoint) {
    .heroSection .bgWatermark {
        font-size: 130px;
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .heroSection {
        padding-top: 140px;
        padding-bottom: 60px;
        min-height: auto;

        .bgWatermark {
            display: none;
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .heroSection {
        padding-top: 110px;
        padding-bottom: 40px;

        .heroContainer .heroText {
            .titleWrapper {
                .title {
                    font-size: clamp(36px, 11vw, 50px);
                }

                .titleStar {
                    width: 36px;
                    height: 36px;
                    top: -16px;
                    right: -32px;
                }
            }
        }
    }
}
</style>
