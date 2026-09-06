<script setup lang="ts">
import { computed } from "vue";
import { motion } from "motion-v";
import type { Project } from "~/lib/types";
import LightboxItem from "~/components/lightbox/LightboxItem.vue";
import Button from "~/components/Button.vue";
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import TablerWorld from "~icons/tabler/world";
import TablerGitMerge from "~icons/tabler/git-merge";
import TablerArrowLeft from "~icons/tabler/arrow-left";
import TablerPhoto from "~icons/tabler/photo";

const props = defineProps<{
    project: Project;
}>();

const { t } = useI18n();

const primaryImage = computed(() => {
    return props.project.imageUrls?.[0] || "";
});
</script>

<template>
    <section :class="[$style.heroSection, 'theme-primary']" id="project-hero">
        <div :class="$style.topographyBg"></div>

        <div :class="$style.bgWatermark" aria-hidden="true">
            {{ project.slug.toUpperCase() }}
        </div>

        <div :class="$style.dots">
            <img src="/images/dots.svg" :class="$style.dots1" alt="" />
            <img src="/images/dots.svg" :class="$style.dots2" alt="" />
            <img src="/images/dots.svg" :class="$style.dots3" alt="" />
        </div>

        <div :class="['container', $style.heroContainer]">
            <motion.div
                :class="$style.topBar"
                :initial="{ opacity: 0, y: -20 }"
                :animate="{ opacity: 1, y: 0 }"
                :transition="{ duration: 0.5 }"
            >
                <NuxtLinkLocale to="/projects">
                    <Button variant="tertiary" :arrow="false">
                        <TablerArrowLeft :class="$style.buttonIcon" />
                        {{ t("projects.detail.backToProjects") }}
                    </Button>
                </NuxtLinkLocale>
            </motion.div>

            <div :class="$style.heroGrid">
                <motion.div
                    :class="$style.heroText"
                    :initial="{ opacity: 0, y: 30 }"
                    :animate="{ opacity: 1, y: 0 }"
                    :transition="{ duration: 0.6, ease: 'easeOut' }"
                >
                    <div :class="$style.titleWrapper">
                        <h1 :class="$style.title">
                            <RandomTextAnimation :text="project.title" :interval="15" />
                        </h1>
                    </div>

                    <div :class="$style.descriptionWrapper">
                        <p :class="$style.description" v-if="project.description">
                            {{ project.description }}
                        </p>
                    </div>

                    <motion.div
                        :class="$style.heroActions"
                        :initial="{ opacity: 0, y: 20 }"
                        :animate="{ opacity: 1, y: 0 }"
                        :transition="{ duration: 0.5, delay: 0.4 }"
                    >
                        <a
                            v-if="project.websiteUrl"
                            :href="project.websiteUrl"
                            target="_blank"
                            rel="noopener noreferrer"
                            :class="$style.actionLink"
                        >
                            <Button variant="tertiary" size="lg" :arrow="true">
                                <TablerWorld :class="$style.buttonIcon" />
                                {{ t("projects.visitWebsite") }}
                            </Button>
                        </a>

                        <a
                            v-if="project.sourceCodeUrl"
                            :href="project.sourceCodeUrl"
                            target="_blank"
                            rel="noopener noreferrer"
                            :class="$style.actionLink"
                        >
                            <Button variant="secondary" size="lg">
                                <TablerGitMerge :class="$style.buttonIcon" />
                                {{ t("projects.sourceCode") }}
                            </Button>
                        </a>
                    </motion.div>
                </motion.div>

                <motion.div
                    :class="$style.heroVisual"
                    :initial="{ opacity: 0, scale: 0.95, y: 40 }"
                    :animate="{ opacity: 1, scale: 1, y: 0 }"
                    :transition="{ duration: 0.8, delay: 0.2, ease: 'easeOut' }"
                    v-if="primaryImage"
                >
                    <div :class="$style.showcaseWrapper">                                
                        <div :class="$style.showcaseWindow">
                            <LightboxItem
                                :key="primaryImage"
                                :src="primaryImage"
                                :alt="project.title"
                                :width="1200"
                                :class-name="$style.primaryLightboxItem"
                                :image-class-name="$style.primaryImage"
                            />
                            <div :class="$style.zoomPill">
                                <TablerPhoto :class="$style.zoomIcon" />
                                <span>{{ t("projects.detail.zoomHint") }}</span>
                            </div>
                        </div>
                    </div>
                </motion.div>
            </div>
        </div>

        <div :class="$style.pageTransition"></div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.actionLink {
    text-decoration: none;

    .buttonIcon {
        font-size: 18px;
        margin-right: 4px;
    }
}

.heroSection {
    position: relative;
    padding-top: 170px;
    padding-bottom: 120px;
    min-height: 85vh;
    background-color: var(--color-primary);
    display: flex;
    flex-direction: column;
    justify-content: center;

    .pageTransition {
        position: absolute;
        width: 100%;
        height: 50px;
        left: 0;
        bottom: -30px;
        background-color: var(--color-background-secondary);
        z-index: 20;
    }

    .topographyBg {
        position: absolute;
        inset: 0;
        width: 100%;
        height: 100%;
        background-image: radial-gradient(
            circle at 50% 50%,
            hsl(from var(--color-lime-500) h s l / 0.15) 20%,
            hsl(from var(--color-lime-600) h s l / 0.35) 100%
        );
        mask-image: url("/patterns/topography-1.svg");
        mask-repeat: repeat;
        mask-size: auto 100%;
        mask-position: center;
        pointer-events: none;
        z-index: 1;
    }

    .bgWatermark {
        position: absolute;
        top: 25%;
        left: 50%;
        transform: translate(-50%, -50%);
        font-size: clamp(90px, 15vw, 240px);
        font-weight: 900;
        letter-spacing: -3px;
        white-space: nowrap;
        color: transparent;
        -webkit-text-stroke: 2px rgba(0, 0, 0, 0.07);
        user-select: none;
        pointer-events: none;
        z-index: 1;
    }

    .dots {
        position: absolute;
        inset: 0;
        width: 100%;
        height: 100%;
        pointer-events: none;
        user-select: none;
        z-index: 1;

        img {
            position: absolute;
            opacity: 0.65;
        }

        .dots1 {
            top: 12%;
            left: 5%;
        }

        .dots2 {
            top: 22%;
            right: 18%;
        }

        .dots3 {
            bottom: 25%;
            left: 45%;
        }
    }

    .heroContainer {
        position: relative;
        z-index: 5;
        display: flex;
        flex-direction: column;
        gap: 32px;

        .topBar {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 16px;
            flex-wrap: wrap;

            .buttonIcon {
                margin-right: 4px;
            }
        }

        .heroGrid {
            display: grid;
            grid-template-columns: 1.1fr 0.9fr;
            gap: 48px;
            align-items: center;

            .heroText {
                display: flex;
                flex-direction: column;
                gap: 24px;

                .titleWrapper {
                    .title {
                        font-size: clamp(44px, 5.5vw, 76px);
                        font-weight: 900;
                        line-height: 1.08;
                        letter-spacing: -1.5px;
                        color: var(--color-text-secondary);
                        margin: 0;
                    }
                }

                .descriptionWrapper {
                    position: relative;
                    max-width: 620px;

                    .description {
                        font-size: clamp(17px, 1.4vw, 21px);
                        line-height: 1.55;
                        font-weight: 500;
                        color: var(--color-text-secondary);
                        margin: 0;
                        opacity: 0.9;
                    }
                }

                .heroActions {
                    display: flex;
                    align-items: center;
                    gap: 16px;
                    flex-wrap: wrap;
                    margin-top: 12px;
                }
            }

            .heroVisual {
                display: flex;
                justify-content: center;
                align-items: center;

                .showcaseWrapper {
                    position: relative;
                    width: 100%;
                    max-width: 600px;

                    .showcaseWindow {
                        position: relative;
                        z-index: 1;
                        background-color: var(--color-carbon-700);
                        border: 2px solid var(--color-carbon-400);
                        border-radius: 28px;
                        overflow: hidden;
                        box-shadow: 0 24px 48px -12px rgba(0, 0, 0, 0.5),
                                    0 0 40px 0 hsl(from var(--color-primary) h s l / 0.25);
                        transition: transform 0.3s cubic-bezier(0.2, 0, 0, 1);
                        aspect-ratio: 16 / 10;
                        cursor: pointer;

                        &:hover {                            
                            .primaryImage {
                                transform: scale(1.04);
                            }
                        }

                        .primaryLightboxItem {
                            width: 100%;
                            height: 100%;
                            border-radius: 0;

                            .primaryImage {
                                width: 100%;
                                height: 100%;
                                object-fit: cover;
                                transition: transform 0.4s ease;
                            }
                        }

                        .zoomPill {
                            position: absolute;
                            bottom: 14px;
                            right: 14px;
                            display: inline-flex;
                            align-items: center;
                            gap: 6px;
                            background-color: rgba(0, 0, 0, 0.7);
                            backdrop-filter: blur(8px);
                            border: 1px solid rgba(255, 255, 255, 0.2);
                            border-radius: 9999px;
                            padding: 6px 14px;
                            color: white;
                            font-size: 12px;
                            font-weight: 600;
                            pointer-events: none;
                            z-index: 2;

                            .zoomIcon {
                                font-size: 14px;
                                color: var(--color-primary);
                            }
                        }
                    }
                }
            }
        }
    }
}

@media screen and (max-width: $laptopBreakpoint) {
    .heroSection {
        .bgWatermark {
            font-size: 140px;
        }

        .heroContainer .heroGrid {
            gap: 36px;
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .heroSection {
        padding-top: 140px;
        padding-bottom: 90px;
        min-height: auto;

        .bgWatermark {
            display: none;
        }

        .heroContainer .heroGrid {
            grid-template-columns: 1fr;
            gap: 40px;

            .heroVisual {
                order: 2;
                max-width: 100%;
            }
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .heroSection {
        padding-top: 120px;
        padding-bottom: 70px;

        .heroContainer {
            gap: 24px;

            .heroGrid .heroText {
                .titleWrapper .title {
                    font-size: clamp(34px, 10vw, 46px);
                }

                .heroActions {
                    flex-direction: column;
                    align-items: stretch;
                    gap: 12px;

                    .actionLink {
                        width: 100%;

                        button {
                            width: 100%;
                        }
                    }
                }
            }
        }
    }
}
</style>
