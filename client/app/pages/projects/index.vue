<script setup lang="ts">
import { ref, computed } from "vue";
import { useFetch } from "#app";
import type { Project, ProjectType } from "~/lib/types";
import ProjectCard from "~/components/projects/ProjectCard.vue";
import Button from "~/components/Button.vue";
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import { motion } from "motion-v";

const { t, locale } = useI18n();

const { data: projects, pending, error } = await useFetch<Project[]>("/api/v1/projects", {
    query: { locale }
});

type FilterType = "all" | ProjectType;
const activeFilter = ref<FilterType>("all");

const counts = computed(() => {
    const all = projects.value?.length || 0;
    const websites = projects.value?.filter(p => p.type === "Website").length || 0;
    const graphics = projects.value?.filter(p => p.type === "Graphics").length || 0;
    return { all, websites, graphics };
});

const filteredProjects = computed(() => {
    if (!projects.value) return [];
    if (activeFilter.value === "all") return projects.value;
    return projects.value.filter(p => p.type === activeFilter.value);
});
</script>

<template>
    <Head>
        <Title>{{ t("projects.allProjectsTitle") }} • Netuvio</Title>
        <Meta name="description" :content="t('projects.metaDescription')" />
    </Head>

    <div :class="$style.pageWrapper">
        <section :class="[$style.heroSection, 'theme-primary']" id="projects-hero">

            <div :class="$style.topographyBg"></div>
            <div :class="$style.ambientGlow"></div>

            <div :class="$style.bgWatermark" aria-hidden="true">
                NETUVIO // PROJECTS
            </div>

<!--            <GlassShapes :class="$style.glassShapes" />-->

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

        <section :class="[$style.contentSection, 'theme-primary']">
            <div :class="$style.transition"></div>
            <div :class="['container', $style.contentContainer]">
                <div :class="$style.filterBarWrapper">
                    <div :class="$style.filters">
                        <button
                            :class="[$style.filterBtn, activeFilter === 'all' && $style.activeFilter]"
                            @click="activeFilter = 'all'"
                        >
                            <span>{{ t("projects.allFilter") }}</span>
                            <span :class="$style.filterCount">{{ counts.all }}</span>
                        </button>

                        <button
                            :class="[$style.filterBtn, activeFilter === 'Website' && $style.activeFilter]"
                            @click="activeFilter = 'Website'"
                        >
                            <span>{{ t("projects.types.website") }}</span>
                            <span :class="$style.filterCount">{{ counts.websites }}</span>
                        </button>

                        <button
                            :class="[$style.filterBtn, activeFilter === 'Graphics' && $style.activeFilter]"
                            @click="activeFilter = 'Graphics'"
                        >
                            <span>{{ t("projects.types.graphicDesign") }}</span>
                            <span :class="$style.filterCount">{{ counts.graphics }}</span>
                        </button>
                    </div>
                </div>

                <!-- Loading skeleton state -->
                <div v-if="pending && (!projects || projects.length === 0)" :class="$style.loadingGrid">
                    <div v-for="i in 4" :key="i" :class="$style.skeletonCard">
                        <div :class="$style.skeletonImage"></div>
                        <div :class="$style.skeletonBody">
                            <div :class="$style.skeletonLine" style="width: 50%"></div>
                            <div :class="$style.skeletonLine" style="width: 85%"></div>
                            <div :class="$style.skeletonLine" style="width: 40%"></div>
                        </div>
                    </div>
                </div>

                <div v-else-if="filteredProjects.length > 0" :class="$style.grid">
                    <ProjectCard 
                        v-for="(project, index) in filteredProjects" 
                        :key="project.id || project.slug"
                        :project="project"
                        :index="index"
                    />
                </div>

                <div v-else :class="$style.emptyState">
                    <div :class="$style.emptyIcon"></div>
                    <p :class="$style.emptyText">{{ t("projects.noProjectsFound") }}</p>
                    <Button variant="primary" size="md" @click="activeFilter = 'all'">
                        {{ t("projects.resetFilter") }}
                    </Button>
                </div>

                <motion.div 
                    :class="$style.ctaBanner"
                    :initial="{ opacity: 0, y: 40 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.65, ease: 'easeOut' }"
                >
                    <div :class="$style.ctaDecorations">
                        <img src="/images/dots.svg" :class="$style.ctaDots" alt="" />
                    </div>

                    <div :class="$style.ctaContent">
                        <h2>{{ t("projects.cta.title") }}</h2>
                        <p>{{ t("projects.cta.subtitle") }}</p>
                    </div>

                    <div :class="$style.ctaAction">
                        <NuxtLinkLocale to="/#contact">
                            <Button variant="primary" size="xl" :arrow="true">
                                {{ t("projects.cta.button") }}
                            </Button>
                        </NuxtLinkLocale>
                    </div>
                </motion.div>
            </div>
        </section>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.pageWrapper {
    width: 100%;
    overflow-x: clip;

    .heroSection {
        position: relative;
        padding-top: 180px;
        min-height: 80vh;
        display: flex;
        align-items: center;
        overflow: hidden;
        background-color: var(--color-background-primary);

        .starrySky {
            position: absolute;
            inset: 0;
            pointer-events: none;
            z-index: 0;
            opacity: 0.8;
        }

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

        .glassShapes {
            position: absolute;
            inset: 0;
            pointer-events: none;
            z-index: 3;
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

                .badgeWrapper {
                    display: inline-flex;
                    align-items: center;
                    gap: 10px;
                    background-color: var(--color-carbon-600);
                    border: 1px solid var(--color-carbon-400);
                    padding: 8px 18px;
                    border-radius: 9999px;
                    box-shadow: 0 4px 16px rgba(0, 0, 0, 0.4);

                    .pulsingDot {
                        width: 8px;
                        height: 8px;
                        border-radius: 50%;
                        background-color: var(--color-primary);
                        box-shadow: 0 0 10px var(--color-primary);
                        animation: radarPulse 2s infinite ease-in-out;
                    }

                    .badge {
                        font-family: monospace;
                        font-size: 13px;
                        font-weight: 800;
                        text-transform: uppercase;
                        letter-spacing: 1.5px;
                        color: var(--color-primary);
                    }
                }

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

                    .subtitleSquiggle {
                        position: absolute;
                        bottom: -32px;
                        left: 0;
                        width: 140px;
                        height: auto;
                        opacity: 0.85;
                    }
                }

                .statsBar {
                    display: flex;
                    flex-wrap: wrap;
                    gap: 12px;
                    margin-top: 36px;

                    .statChip {
                        display: inline-flex;
                        align-items: center;
                        gap: 8px;
                        background-color: rgba(0, 0, 0, 0.45);
                        border: 1px solid var(--color-carbon-400);
                        border-radius: 9999px;
                        padding: 6px 16px;
                        font-family: monospace;
                        font-size: 13px;
                        backdrop-filter: blur(10px);

                        .statPrefix {
                            color: var(--color-primary);
                            font-weight: 800;
                        }

                        .statVal {
                            color: var(--color-carbon-50);
                            font-weight: 600;
                        }
                    }
                }
            }
        }
    }

    .contentSection {
        padding: 20px 0 140px;
        position: relative;
        
        .transition {
            position: absolute;
            top: -80px;
            left: 0;
            width: 100%;
            height: 80px;
            background: linear-gradient(to top, var(--color-background-primary) 0%, var(--color-background-primary) 60%, transparent 100%);
            pointer-events: none;
            z-index: 1;
        }

        .contentContainer {
            display: flex;
            flex-direction: column;
            gap: 48px;

            .filterBarWrapper {
                display: flex;
                justify-content: flex-start;
                padding-bottom: 8px;

                .filters {
                    display: inline-flex;
                    flex-wrap: wrap;
                    gap: 8px;
                    background-color: var(--color-carbon-700);
                    border: 2px solid var(--color-carbon-400);
                    border-radius: 10000px;
                    padding: 8px;

                    .filterBtn {
                        display: inline-flex;
                        align-items: center;
                        gap: 10px;
                        background: transparent;
                        color: var(--color-carbon-100);
                        border: none;
                        border-radius: 10000px;
                        padding: 10px 22px;
                        font-size: 15px;
                        font-weight: 700;
                        cursor: pointer;
                        transition: all 0.2s cubic-bezier(0.2, 0, 0, 1);
                        user-select: none;

                        &:hover {
                            color: var(--color-text-primary);
                            background-color: rgba(255, 255, 255, 0.06);
                        }

                        &.activeFilter {
                            background-color: var(--color-primary);
                            color: var(--color-text-secondary);

                            .filterCount {
                                background-color: rgba(0, 0, 0, 0.2);
                                color: var(--color-text-secondary);
                            }
                        }

                        .filterCount {
                            background-color: var(--color-carbon-500);
                            color: var(--color-carbon-100);
                            font-size: 12px;
                            font-weight: 700;
                            padding: 2px 8px;
                            border-radius: 9999px;
                            transition: all 0.2s ease;
                        }
                    }
                }
            }

            .grid {
                display: grid;
                grid-template-columns: repeat(2, 1fr);
                gap: 40px;
            }

            .loadingGrid {
                display: grid;
                grid-template-columns: repeat(2, 1fr);
                gap: 40px;

                .skeletonCard {
                    background-color: var(--color-carbon-600);
                    border: 2px solid var(--color-carbon-400);
                    border-radius: 30px;
                    overflow: hidden;
                    height: 520px;
                    display: flex;
                    flex-direction: column;

                    .skeletonImage {
                        width: 100%;
                        aspect-ratio: 16 / 10;
                        background-color: var(--color-carbon-500);
                        animation: skeletonPulse 1.6s infinite ease-in-out;
                    }

                    .skeletonBody {
                        padding: 28px;
                        display: flex;
                        flex-direction: column;
                        gap: 18px;
                        flex: 1;

                        .skeletonLine {
                            height: 20px;
                            background-color: var(--color-carbon-500);
                            border-radius: 8px;
                            animation: skeletonPulse 1.6s infinite ease-in-out;
                        }
                    }
                }
            }

            .emptyState {
                background-color: var(--color-carbon-600);
                border: 2px dashed var(--color-carbon-400);
                border-radius: 32px;
                padding: 80px 32px;
                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
                gap: 24px;
                text-align: center;

                .emptyIcon {
                    width: 56px;
                    height: 56px;
                    mask-image: url("/images/star-1.svg");
                    background-color: var(--color-text-primary);
                    mask-repeat: no-repeat;
                    mask-size: contain;
                }

                .emptyText {
                    font-size: 20px;
                    margin: 0;
                    font-weight: 600;
                }
            }

            .ctaBanner {
                margin-top: 50px;
                position: relative;
                overflow: hidden;
                border-radius: 36px;
                background-color: var(--color-carbon-600);
                border: 2px solid var(--color-carbon-400);
                padding: clamp(48px, 6vw, 72px);
                display: flex;
                justify-content: space-between;
                align-items: center;
                gap: 40px;

                .ctaDecorations {
                    position: absolute;
                    inset: 0;
                    pointer-events: none;
                    overflow: hidden;

                    .ctaDots {
                        position: absolute;
                        top: -20px;
                        right: 25%;
                        width: 240px;
                        opacity: 0.15;
                    }

                    .ctaStar {
                        position: absolute;
                        bottom: -20px;
                        left: 45%;
                        width: 90px;
                        opacity: 0.2;
                        animation: starSpin 30s infinite linear;
                    }
                }

                .ctaContent {
                    max-width: 650px;
                    position: relative;
                    z-index: 1;

                    h2 {
                        font-size: clamp(32px, 4vw, 52px);
                        font-weight: 900;
                        color: var(--color-text-primary);
                        margin-bottom: 16px;
                        line-height: 1.15;
                        letter-spacing: -1px;
                        position: relative;
                        display: inline-block;
                    }

                    p {
                        font-size: clamp(16px, 1.4vw, 19px);
                        color: var(--color-carbon-50);
                        line-height: 1.6;
                        
                    }
                }

                .ctaAction {
                    position: relative;
                    z-index: 1;
                    flex-shrink: 0;
                }
            }
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

        .contentSection .contentContainer {
            .grid,
            .loadingGrid {
                grid-template-columns: 1fr;
                gap: 28px;
            }

            .ctaBanner {
                flex-direction: column;
                align-items: flex-start;
                padding: 36px 28px;
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

                .subtitleWrapper .subtitleSquiggle {
                    display: none;
                }

                .statsBar {
                    margin-top: 20px;

                    .statChip {
                        font-size: 12px;
                        padding: 4px 12px;
                    }
                }
            }
        }

        .contentSection .contentContainer {
            .filterBarWrapper .filters {
                width: 100%;
                border-radius: 20px;
                padding: 6px;

                .filterBtn {
                    flex: 1;
                    justify-content: center;
                    padding: 8px 14px;
                    font-size: 13px;
                }
            }

            .ctaBanner {
                border-radius: 24px;
                padding: 28px 20px;

                .ctaContent h2 .ctaSquiggle {
                    display: none;
                }
            }
        }
    }
}

@keyframes skeletonPulse {
    0%, 100% {
        opacity: 0.6;
    }
    50% {
        opacity: 0.25;
    }
}
</style>
