<script setup lang="ts">
import { ref, computed } from "vue";
import { useFetch, useRoute } from "#app";
import type { Project, ProjectType } from "~/lib/types";
import ProjectCard from "~/components/projects/ProjectCard.vue";
import Button from "~/components/Button.vue";
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

    <section :class="[$style.section, 'theme-primary']" id="all-projects">
        <div :class="['container', $style.container]">
            <!-- Header section -->
            <motion.div 
                :class="$style.header"
                :initial="{ opacity: 0, y: 20 }"
                :animate="{ opacity: 1, y: 0 }"
                :transition="{ duration: 0.6 }"
            >
                <span :class="$style.badge">{{ t("projects.badge") }}</span>
                <h1 :class="$style.title">{{ t("projects.allProjects") }}</h1>
                <p :class="$style.subtitle">{{ t("projects.subtitle") }}</p>
            </motion.div>

            <!-- Filter tabs -->
            <div :class="$style.filters">
                <button
                    :class="[$style.filterBtn, activeFilter === 'all' && $style.activeFilter]"
                    @click="activeFilter = 'all'"
                >
                    {{ t("projects.allFilter") }}
                    <span :class="$style.filterCount">{{ counts.all }}</span>
                </button>

                <button
                    :class="[$style.filterBtn, activeFilter === 'Website' && $style.activeFilter]"
                    @click="activeFilter = 'Website'"
                >
                    {{ t("projects.types.website") }}
                    <span :class="$style.filterCount">{{ counts.websites }}</span>
                </button>

                <button
                    :class="[$style.filterBtn, activeFilter === 'Graphics' && $style.activeFilter]"
                    @click="activeFilter = 'Graphics'"
                >
                    {{ t("projects.types.graphicDesign") }}
                    <span :class="$style.filterCount">{{ counts.graphics }}</span>
                </button>
            </div>

            <!-- Loading state -->
            <div v-if="pending && (!projects || projects.length === 0)" :class="$style.loadingGrid">
                <div v-for="i in 4" :key="i" :class="$style.skeletonCard">
                    <div :class="$style.skeletonImage"></div>
                    <div :class="$style.skeletonBody">
                        <div :class="$style.skeletonLine" style="width: 60%"></div>
                        <div :class="$style.skeletonLine" style="width: 90%"></div>
                        <div :class="$style.skeletonLine" style="width: 40%"></div>
                    </div>
                </div>
            </div>

            <!-- Projects grid -->
            <div v-else-if="filteredProjects.length > 0" :class="$style.grid">
                <ProjectCard 
                    v-for="(project, index) in filteredProjects" 
                    :key="project.id || project.slug"
                    :project="project"
                    :index="index"
                />
            </div>

            <!-- Empty state -->
            <div v-else :class="$style.emptyState">
                <p :class="$style.emptyText">{{ t("projects.noProjectsFound") }}</p>
                <Button variant="secondary" size="md" @click="activeFilter = 'all'">
                    {{ t("projects.resetFilter") }}
                </Button>
            </div>

            <!-- Bottom CTA Section -->
            <motion.div 
                :class="$style.ctaCard"
                :initial="{ opacity: 0, y: 30 }"
                :whileInView="{ opacity: 1, y: 0 }"
                :inViewOptions="{ once: true }"
                :transition="{ duration: 0.6 }"
            >
                <div :class="$style.ctaContent">
                    <h2>{{ t("projects.cta.title") }}</h2>
                    <p>{{ t("projects.cta.subtitle") }}</p>
                </div>
                <NuxtLinkLocale to="/#contact">
                    <Button variant="primary" size="lg">
                        {{ t("projects.cta.button") }}
                    </Button>
                </NuxtLinkLocale>
            </motion.div>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    margin: 128px 0 120px;
}

.container {
    display: flex;
    flex-direction: column;
    gap: 48px;
}

.header {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 16px;
    max-width: 800px;
}

.badge {
    display: inline-block;
    font-size: 14px;
    font-weight: 600;
    text-transform: uppercase;
    letter-spacing: 1px;
    color: var(--color-primary);
    background-color: var(--color-carbon-600);
    border: 1px solid var(--color-carbon-400);
    padding: 6px 16px;
    border-radius: 9999px;
}

.title {
    font-size: clamp(36px, 5vw, 56px);
    font-weight: 700;
    color: var(--color-text-primary);
    line-height: 1.15;
    margin: 0;
}

.subtitle {
    font-size: clamp(16px, 1.3vw, 20px);
    line-height: 1.6;
    color: var(--color-carbon-100);
    margin: 0;
}

.filters {
    display: flex;
    flex-wrap: wrap;
    gap: 12px;
}

.filterBtn {
    display: inline-flex;
    align-items: center;
    gap: 8px;
    background-color: var(--color-carbon-600);
    color: var(--color-carbon-100);
    border: 1px solid var(--color-carbon-400);
    border-radius: 9999px;
    padding: 10px 20px;
    font-size: 15px;
    font-weight: 600;
    cursor: pointer;
    transition: all 0.2s ease;
    user-select: none;

    &:hover {
        color: var(--color-text-primary);
        border-color: var(--color-carbon-300);
        background-color: var(--color-carbon-500);
    }

    &.activeFilter {
        background-color: var(--color-primary);
        color: var(--color-text-secondary);
        border-color: var(--color-primary);

        .filterCount {
            background-color: rgba(0, 0, 0, 0.2);
            color: var(--color-text-secondary);
        }
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

.grid {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 32px;
}

.loadingGrid {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 32px;
}

.skeletonCard {
    background-color: var(--color-carbon-600);
    border: 1px solid var(--color-carbon-400);
    border-radius: 28px;
    overflow: hidden;
    height: 480px;
    display: flex;
    flex-direction: column;
}

.skeletonImage {
    width: 100%;
    aspect-ratio: 16 / 10;
    background-color: var(--color-carbon-500);
    animation: pulse 1.5s infinite ease-in-out;
}

.skeletonBody {
    padding: 24px;
    display: flex;
    flex-direction: column;
    gap: 16px;
    flex: 1;
}

.skeletonLine {
    height: 18px;
    background-color: var(--color-carbon-500);
    border-radius: 8px;
    animation: pulse 1.5s infinite ease-in-out;
}

@keyframes pulse {
    0%, 100% {
        opacity: 0.6;
    }
    50% {
        opacity: 0.3;
    }
}

.emptyState {
    background-color: var(--color-carbon-600);
    border: 1px solid var(--color-carbon-400);
    border-radius: 28px;
    padding: 64px 32px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    gap: 20px;
    text-align: center;
}

.emptyText {
    font-size: 18px;
    color: var(--color-carbon-100);
    margin: 0;
}

.ctaCard {
    background: linear-gradient(135deg, var(--color-carbon-600) 0%, var(--color-carbon-500) 100%);
    border: 1px solid var(--color-carbon-400);
    border-radius: 30px;
    padding: clamp(32px, 5vw, 48px);
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 32px;
    margin-top: 24px;

    .ctaContent {
        max-width: 620px;

        h2 {
            font-size: clamp(24px, 3.2vw, 36px);
            color: var(--color-primary);
            margin: 0 0 10px;
        }

        p {
            font-size: clamp(15px, 1.2vw, 17px);
            color: var(--color-carbon-100);
            line-height: 1.6;
            margin: 0;
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .grid,
    .loadingGrid {
        grid-template-columns: 1fr;
    }

    .ctaCard {
        flex-direction: column;
        align-items: flex-start;
        gap: 24px;
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .section {
        margin: 100px 0 80px;
    }

    .container {
        gap: 32px;
    }

    .filters {
        gap: 8px;
    }

    .filterBtn {
        padding: 8px 16px;
        font-size: 14px;
    }

    .ctaCard {
        border-radius: 20px;
        padding: 24px;
    }
}
</style>
