<script setup lang="ts">
import { computed } from "vue";
import type { Project } from "~/lib/types";
import Button from "~/components/Button.vue";
import TechnologyTag from "~/components/TechnologyTag.vue";
import { motion } from "motion-v";
import TablerWorld from '~icons/tabler/world';
import TablerGitMerge from '~icons/tabler/git-merge';
import Fa7SolidStar from '~icons/fa7-solid/star';

const props = defineProps<{
    project: Project;
    index?: number;
}>();

const { t } = useI18n();

const projectTypeLabel = computed(() => {
    return props.project.type === "Website" 
        ? t("projects.types.website") 
        : t("projects.types.graphicDesign");
});

function formatDate(date: string): string {
    return new Intl.DateTimeFormat(undefined, {
        year: 'numeric',
        month: 'short',
    }).format(new Date(date));
}

const dateRange = computed(() => {
    const startedAt = props.project.startedAt ? formatDate(props.project.startedAt) : null;
    const finishedAt = props.project.finishedAt ? formatDate(props.project.finishedAt) : null;

    if (startedAt && finishedAt) return `${startedAt} - ${finishedAt}`;
    if (!startedAt && finishedAt) return `${finishedAt}`;
    if (!finishedAt && startedAt) return `${startedAt} - Present`;
    return "";
});
</script>

<template>
    <motion.article 
        :class="$style.card"
        :initial="{ opacity: 0, y: 24 }"
        :whileInView="{ opacity: 1, y: 0 }"
        :inViewOptions="{ once: true }"
        :transition="{ duration: 0.5, delay: (index ?? 0) * 0.08 }"
    >
        <NuxtLinkLocale :to="`/projects/${project.slug}`" :class="$style.imageWrapper">
            <NuxtImg 
                v-if="project.imageUrls && project.imageUrls.length > 0"
                :src="project.imageUrls[0]" 
                :alt="project.title"
                :class="$style.image"
                loading="lazy"
            />
            <div v-else :class="$style.imagePlaceholder">
                <span>{{ project.title }}</span>
            </div>

            <div :class="$style.badges">
                <span v-if="project.isFeatured" :class="$style.featuredBadge">
                    <Fa7SolidStar />
                </span>
                <span :class="$style.typeBadge">{{ projectTypeLabel }}</span>
            </div>
        </NuxtLinkLocale>

        <div :class="$style.content">
            <div :class="$style.header">
                <div :class="$style.titleRow">
                    <NuxtLinkLocale :to="`/projects/${project.slug}`" :class="$style.titleLink">
                        <h2 :class="$style.title">{{ project.title }}</h2>
                    </NuxtLinkLocale>
                    <span v-if="dateRange" :class="$style.date">{{ dateRange }}</span>
                </div>
                <p :class="$style.description">
                    {{ project.description || project.body }}
                </p>
            </div>

            <div v-if="project.technologies && project.technologies.length > 0" :class="$style.technologies">
                <TechnologyTag 
                    v-for="tech in project.technologies" 
                    :key="tech" 
                    :technology="tech" 
                />
            </div>

            <div :class="$style.footer">
                <NuxtLinkLocale :to="`/projects/${project.slug}`">
                    <Button size="md" variant="secondary">
                        {{ t('projects.learnMore') }}
                    </Button>
                </NuxtLinkLocale>

                <div :class="$style.externalLinks" v-if="project.websiteUrl || project.sourceCodeUrl">
                    <a 
                        v-if="project.websiteUrl" 
                        :href="project.websiteUrl" 
                        target="_blank" 
                        rel="noopener noreferrer"
                        :title="t('projects.visitWebsite')"
                        :class="$style.externalLink"
                    >
                        <TablerWorld />
                    </a>
                    <a 
                        v-if="project.sourceCodeUrl" 
                        :href="project.sourceCodeUrl" 
                        target="_blank" 
                        rel="noopener noreferrer"
                        :title="t('projects.sourceCode')"
                        :class="$style.externalLink"
                    >
                        <TablerGitMerge />
                    </a>
                </div>
            </div>
        </div>
    </motion.article>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.card {
    background-color: var(--color-carbon-600);
    border: 1px solid var(--color-carbon-400);
    border-radius: 28px;
    overflow: hidden;
    display: flex;
    flex-direction: column;
    transition: border-color 0.25s ease, transform 0.25s ease, box-shadow 0.25s ease;

    &:hover {
        border-color: var(--color-carbon-300);
        transform: translateY(-4px);
        box-shadow: 0 16px 32px rgba(0, 0, 0, 0.35);

        .image {
            transform: scale(1.03);
        }

        .title {
            color: var(--color-primary);
        }
    }
}

.imageWrapper {
    position: relative;
    width: 100%;
    aspect-ratio: 16 / 10;
    overflow: hidden;
    background-color: var(--color-carbon-700);
    display: block;
}

.image {
    width: 100%;
    height: 100%;
    object-fit: cover;
    transition: transform 0.4s ease;
}

.imagePlaceholder {
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    color: var(--color-carbon-200);
    font-size: 20px;
    font-weight: 600;
}

.badges {
    position: absolute;
    top: 16px;
    left: 16px;
    display: flex;
    gap: 4px;
    z-index: 2;
}

.typeBadge {
    background-color: rgba(0, 0, 0, 0.7);
    backdrop-filter: blur(8px);
    color: var(--color-text-primary);
    font-size: 13px;
    font-weight: 600;
    padding: 6px 12px;
    border-radius: 9999px;
    border: 1px solid rgba(255, 255, 255, 0.12);
}

.featuredBadge {
    background-color: var(--color-primary);
    color: var(--color-text-secondary);
    border-radius: 9999px;
    text-transform: uppercase;
    letter-spacing: 0.5px;
    width: 33px;
    display: grid;
    place-items: center;
    
    svg {
        height: 100%;
    }
}

.content {
    padding: 24px;
    display: flex;
    flex-direction: column;
    flex: 1;
    gap: 18px;
}

.header {
    display: flex;
    flex-direction: column;
    gap: 8px;
}

.titleRow {
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 12px;
}

.titleLink {
    text-decoration: none;
    flex: 1;
}

.title {
    font-size: 26px;
    font-weight: 700;
    color: var(--color-text-primary);
    margin: 0;
    transition: color 0.2s ease;
}

.date {
    font-size: 14px;
    color: var(--color-carbon-100);
    white-space: nowrap;
}

.description {
    font-size: 15px;
    line-height: 1.55;
    color: var(--color-carbon-100);
    margin: 0;
    display: -webkit-box;
    -webkit-line-clamp: 3;
    -webkit-box-orient: vertical;
    overflow: hidden;
}

.technologies {
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
}

.footer {
    margin-top: auto;
    padding-top: 14px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-top: 1px solid var(--color-carbon-500);
}

.externalLinks {
    display: flex;
    gap: 8px;
}

.externalLink {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 36px;
    height: 36px;
    border-radius: 50%;
    border: 1px solid var(--color-carbon-400);
    background-color: var(--color-carbon-500);
    color: var(--color-carbon-100);
    font-size: 18px;
    transition: all 0.2s ease;

    &:hover {
        color: var(--color-primary);
        border-color: var(--color-primary);
        background-color: var(--color-carbon-400);
        transform: translateY(-2px);
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .card {
        border-radius: 20px;
    }

    .content {
        padding: 18px;
        gap: 14px;
    }

    .title {
        font-size: 22px;
    }

    .titleRow {
        flex-direction: column;
        gap: 4px;
    }
}
</style>
