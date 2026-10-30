<script setup lang="ts">
import { computed } from "vue";
import { Button } from "@netuvio/ui/vue";
import { motion } from "motion-v";
import type { Project, ProjectType } from "~/lib/types";
import TablerWorld from '~icons/tabler/world';
import TablerGitMerge from '~icons/tabler/git-merge';
import DrawnArrow from "~/components/DrawnArrow.vue";

const props = withDefaults(defineProps<{
    project?: Project;
    title?: string;
    imageUrl?: string;
    type?: ProjectType;
    slug?: string;
    startedAt?: string;
    finishedAt?: string;
    websiteUrl?: string;
    sourceCodeUrl?: string;
    index?: number;
}>(), {});

const { t, locale } = useI18n();

const projectData = computed(() => {
    if (props.project) {
        return props.project;
    }
    return {
        id: props.slug || "",
        slug: props.slug || "",
        title: props.title || "",
        type: props.type || "Website",
        imageUrls: props.imageUrl ? [props.imageUrl] : [],
        startedAt: props.startedAt,
        finishedAt: props.finishedAt,
        websiteUrl: props.websiteUrl,
        sourceCodeUrl: props.sourceCodeUrl,
        body: "",
        isFeatured: true,
        technologies: [],
    } as Project;
});

const projectTypeLabel = computed(() => {
    return projectData.value.type === "Website" 
        ? t("projects.types.website") 
        : t("projects.types.graphicDesign");
});

function formatDate(date: string): string {
    return new Intl.DateTimeFormat(locale.value || undefined, {
        year: "numeric",
        month: "short",
    }).format(new Date(date));
}

const dateRange = computed(() => {
    const startedAt = projectData.value.startedAt ? formatDate(projectData.value.startedAt) : null;
    const finishedAt = projectData.value.finishedAt ? formatDate(projectData.value.finishedAt) : null;

    if (startedAt && finishedAt) return `${startedAt} - ${finishedAt}`;
    if (!startedAt && finishedAt) return `${finishedAt}`;
    if (!finishedAt && startedAt) return `${startedAt} - Present`;
    return "";
});
</script>

<template>
    <motion.div 
        :class="[$style.project, (index ?? 0) % 2 === 1 ? $style.reversed : '']"
        :initial="{ opacity: 0, y: 16 }"
        :whileInView="{ opacity: 1, y: 0 }"
        :inViewOptions="{ once: true }"
        :transition="{ duration: 0.55, delay: (index ?? 0) * 0.1, ease: 'easeOut' }"
    >
        <div :class="$style.info">
            <div :class="$style.decorations">
                <img src="/images/dots.svg" :class="$style.dotsPattern" alt="" />
            </div>

            <section :class="$style.top">
                <div :class="$style.header">
                    <NuxtLinkLocale :to="`/projects/${projectData.slug}`" :class="$style.titleLink">
                        <h2 :class="$style.title">{{ projectData.title }}</h2>
                    </NuxtLinkLocale>

                    <span v-if="dateRange" :class="$style.dateTag">{{ dateRange }}</span>

                    <p :class="$style.description">
                        <slot>{{ projectData.description || projectData.body }}</slot>
                    </p>
                </div>
            </section>

            <section :class="$style.bottom">
                <NuxtLinkLocale :to="`/projects/${projectData.slug}`">
                    <Button size="md" variant="primary">
                        {{ t('projects.learnMore') }}
                        <DrawnArrow />
                    </Button>
                </NuxtLinkLocale>

                <div :class="$style.externalLinks" v-if="projectData.websiteUrl || projectData.sourceCodeUrl">
                    <a 
                        v-if="projectData.websiteUrl" 
                        :href="projectData.websiteUrl" 
                        target="_blank" 
                        rel="noopener noreferrer"
                        :title="t('projects.visitWebsite')"
                        :class="$style.externalLink"
                    >
                        <TablerWorld />
                    </a>
                    <a 
                        v-if="projectData.sourceCodeUrl" 
                        :href="projectData.sourceCodeUrl" 
                        target="_blank" 
                        rel="noopener noreferrer"
                        :title="t('projects.sourceCode')"
                        :class="$style.externalLink"
                    >
                        <TablerGitMerge />
                    </a>
                </div>
            </section>
        </div>

        <NuxtLinkLocale :to="`/projects/${projectData.slug}`" :class="$style.imageWrapper">
            <NuxtImg 
                v-if="(projectData.imageUrls && projectData.imageUrls.length > 0) || imageUrl"
                :src="imageUrl || projectData.imageUrls[0]" 
                :alt="projectData.title"
                :class="$style.image"
                loading="lazy"
            />
            
            <div v-else :class="$style.imagePlaceholder">
                <span>{{ projectData.title }}</span>
            </div>

            <div :class="$style.imageGloss"></div>

            <div :class="$style.badges">
                <span :class="$style.typeBadge">{{ projectTypeLabel }}</span>
            </div>
        </NuxtLinkLocale>
    </motion.div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.project {
    display: flex;
    height: 540px;
    background-color: var(--color-carbon-600);
    border: 2px solid var(--color-carbon-400);
    border-radius: 30px;
    overflow: hidden;
    position: relative;
    box-shadow: 0 12px 36px rgba(0, 0, 0, 0.45);
    transition: all 0.3s cubic-bezier(0.2, 0, 0, 1);

    &:hover {
        border-color: var(--color-primary);
        transform: translateY(-6px);
        box-shadow: 0 24px 56px -8px rgba(0, 0, 0, 0.8), 0 0 36px -4px hsl(from var(--color-primary) h s l / 0.25);

        .title {
            color: var(--color-primary);
        }
    }

    &.reversed {
        flex-direction: row-reverse;

        .imageWrapper {
            border-left: none;
            border-right: 1px solid var(--color-carbon-500);
        }
    }
}

.decorations {
    position: absolute;
    inset: 0;
    pointer-events: none;
    overflow: hidden;
    z-index: 0;

    .dotsPattern {
        position: absolute;
        bottom: 20px;
        right: -30px;
        width: 150px;
        height: 150px;
        opacity: 0.15;
        object-fit: contain;
    }
}

.info {
    width: 44%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding: 36px 40px;
    position: relative;
    overflow: hidden;
    z-index: 1;

    .top {
        position: relative;
        z-index: 1;
    }
}

.header {
    display: flex;
    flex-direction: column;
}

.titleLink {
    text-decoration: none;
}

.title {
    font-size: var(--font-size-xl);
    font-weight: 800;
    color: var(--color-text-primary);
    margin: 0;
    line-height: 1.25;
    transition: color 0.2s ease;
}

.dateTag {
    color: var(--color-carbon-200);
    letter-spacing: 0.5px;
    font-size: var(--font-size-sm);
    margin-top: 6px;
    margin-bottom: 16px;
}

.description {
    font-size: var(--font-size-base);
    line-height: 1.6;
    color: var(--color-carbon-100);
    margin: 0;
    display: -webkit-box;
    -webkit-line-clamp: 7;
    -webkit-box-orient: vertical;
    overflow: hidden;
}

.bottom {
    margin-top: auto;
    padding-top: 20px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-top: 1px solid var(--color-carbon-500);
    position: relative;
    z-index: 1;
}

.externalLinks {
    display: flex;
    gap: 10px;
}

.externalLink {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    border: 1px solid var(--color-carbon-400);
    background-color: var(--color-carbon-500);
    color: var(--color-carbon-100);
    font-size: var(--font-size-lg);
    transition: all 0.22s ease;

    &:hover {
        color: var(--color-primary);
        border-color: var(--color-primary);
        background-color: var(--color-carbon-400);
        transform: translateY(-2px);
        box-shadow: 0 6px 16px rgba(0, 0, 0, 0.4);
    }
}

.imageWrapper {
    width: 56%;
    height: 100%;
    overflow: hidden;
    display: block;
    position: relative;
    background-color: var(--color-carbon-700);
    text-decoration: none;
    border-left: 1px solid var(--color-carbon-500);

    .image {
        width: 100%;
        height: 100%;
        object-fit: cover;
    }
}

.imageGloss {
    position: absolute;
    inset: 0;
    background: linear-gradient(135deg, rgba(255, 255, 255, 0.08) 0%, transparent 50%, rgba(0, 0, 0, 0.4) 100%);
    pointer-events: none;
    z-index: 1;
}

.imagePlaceholder {
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    color: var(--color-carbon-200);
    font-size: var(--font-size-lg);
    font-weight: 700;
}

.badges {
    position: absolute;
    top: 20px;
    left: 20px;
    display: flex;
    gap: 8px;
    z-index: 2;
    height: 32px;
}

.typeBadge {
    background-color: rgba(15, 18, 14, 0.5);
    backdrop-filter: blur(12px);
    color: var(--color-text-primary);
    font-size: var(--font-size-xs);
    font-weight: 700;
    padding: 6px 14px;
    border-radius: 9999px;
    border: 2px solid transparent;
    letter-spacing: 0.3px;
    text-transform: uppercase;
    height: 32px;
}

@media screen and (max-width: $laptopBreakpoint) {
    .project {
        height: 480px;
    }

    .info {
        padding: 28px 32px;
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .project,
    .project.reversed {
        flex-direction: column;
        height: auto;
        min-height: 0;
    }

    .imageWrapper {
        order: -1;
        width: 100%;
        height: auto;
        aspect-ratio: 16 / 10;
        border-left: none !important;
        border-right: none !important;
        border-bottom: 1px solid var(--color-carbon-500);
    }

    .info {
        width: 100%;
        padding: 28px 24px;
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .project {
        border-radius: 24px;
    }

    .info {
        padding: 22px 18px;
    }

    .bottom {
        padding-top: 16px;
        gap: 12px;
    }

    .badges {
        top: 14px;
        left: 14px;
    }
}
</style>
