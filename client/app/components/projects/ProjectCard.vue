<script setup lang="ts">
import { computed } from "vue";
import type { Project } from "~/lib/types";
import Button from "~/components/Button.vue";
import TechnologyTag from "~/components/TechnologyTag.vue";
import { motion } from "motion-v";
import TablerWorld from '~icons/tabler/world';
import TablerGitMerge from '~icons/tabler/git-merge';
import Fa7SolidStar from '~icons/fa7-solid/star'

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

const formattedIndex = computed(() => {
    return String((props.index ?? 0) + 1).padStart(2, "0");
});

function formatDate(date: string): string {
    return new Intl.DateTimeFormat(undefined, {
        year: "numeric",
        month: "short",
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
        :initial="{ opacity: 0, y: 30 }"
        :whileInView="{ opacity: 1, y: 0 }"
        :inViewOptions="{ once: true }"
        :transition="{ duration: 0.55, delay: (index ?? 0) * 0.08, ease: 'easeOut' }"
    >
        <div :class="$style.decorations">
            <img src="/images/dots.svg" :class="$style.dotsPattern" alt="" />
            <div :class="$style.cylinders">
                <div></div>
                <div></div>
            </div>
        </div>

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

            <div :class="$style.imageGloss"></div>

            <div :class="$style.badges">
                <span v-if="project.isFeatured" :class="$style.featuredBadge">
                    <Fa7SolidStar :class="$style.starIcon" />
                </span>
                <span :class="$style.typeBadge">{{ projectTypeLabel }}</span>
            </div>
        </NuxtLinkLocale>

        <div :class="$style.content">
            <div :class="$style.metaStrip">
                <div :class="$style.indexTag">
                    <span :class="$style.indexNumber">// {{ formattedIndex }}</span>
                </div>
            </div>
            <div :class="$style.header">
                <div>
                    <NuxtLinkLocale :to="`/projects/${project.slug}`" :class="$style.titleLink">
                        <h2 :class="$style.title">{{ project.title }}</h2>
                    </NuxtLinkLocale>

                    <span v-if="dateRange" :class="$style.dateTag">{{ dateRange }}</span>
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
                    <Button size="md" variant="primary" :arrow="true">
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
    border: 2px solid var(--color-carbon-400);
    border-radius: 30px;
    overflow: hidden;
    display: flex;
    flex-direction: column;
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

        .decorations .cylinders div {
            background-color: var(--color-primary);
            box-shadow: 0 0 16px var(--color-primary);
        }

        .metaStrip .pulsingDot {
            box-shadow: 0 0 12px var(--color-primary);
        }
    }
}

.dateTag {
    color: var(--color-carbon-200);
    letter-spacing: 0.5px;
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

    .cylinders {
        position: absolute;
        inset: 0;

        div {
            position: absolute;
            border-radius: 9999px;
            background-color: var(--color-carbon-400);
            transition: all 0.3s ease;

            &:nth-child(1) {
                width: 6px;
                height: 56px;
                top: 45%;
                right: 0;
                transform: translateX(50%);
            }

            &:nth-child(2) {
                width: 56px;
                height: 6px;
                bottom: 0;
                right: 25%;
                transform: translateY(50%);
            }
        }
    }
}

.metaStrip {
    display: flex;
    justify-content: space-between;
    align-items: center;
    z-index: 1;

    .indexTag {
        display: flex;
        align-items: center;
        gap: 12px;

        .indexNumber {
            font-family: monospace;
            font-size: 14px;
            font-weight: 700;
            color: var(--color-primary);
            letter-spacing: 1px;
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
    z-index: 1;
    border-bottom: 1px solid var(--color-carbon-500);
}

.image {
    width: 100%;
    height: 100%;
    object-fit: cover;
    transition: transform 0.5s cubic-bezier(0.2, 0, 0, 1);
}

.imageGloss {
    position: absolute;
    inset: 0;
    background: linear-gradient(135deg, rgba(255, 255, 255, 0.08) 0%, transparent 50%, rgba(0, 0, 0, 0.4) 100%);
    pointer-events: none;
}

.imagePlaceholder {
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    color: var(--color-carbon-200);
    font-size: 20px;
    font-weight: 700;
}

.badges {
    position: absolute;
    top: 14px;
    left: 14px;
    display: flex;
    gap: 8px;
    z-index: 2;
    height: 32px;
}

.typeBadge {
    background-color: rgba(15, 18, 14, 0.5);
    backdrop-filter: blur(12px);
    color: var(--color-text-primary);
    font-size: 12px;
    font-weight: 700;
    padding: 6px 14px;
    border-radius: 9999px;
    //border: 2px solid rgba(255, 255, 255, 0.16);
    border: 2px solid transparent;
    letter-spacing: 0.3px;
    text-transform: uppercase;
    height: 32px;
}

.featuredBadge {
    background-color: var(--color-primary);
    color: var(--color-text-secondary);
    font-weight: 800;
    border-radius: 9999px;
    text-transform: uppercase;
    height: 32px;
    letter-spacing: 0.6px;
    //border: 2px solid var(--color-border-primary);
    border: 2px solid transparent;

    .starIcon {
        width: 28px;
        height: 28px;
        padding: 5px;
    }
}

.content {
    padding: 24px 28px 28px;
    display: flex;
    flex-direction: column;
    flex: 1;
    gap: 20px;
    z-index: 1;
}

.header {
    display: flex;
    flex-direction: column;
    gap: 10px;
    
    >div {
        display: flex;
        align-items: center;
        gap: 12px;
        justify-content: space-between;
    }
}

.titleLink {
    text-decoration: none;
}

.title {
    font-size: clamp(24px, 2.4vw, 32px);
    font-weight: 800;
    color: var(--color-text-primary);
    margin: 0;
    line-height: 1.25;
    transition: color 0.2s ease;
}

.description {
    font-size: 15px;
    line-height: 1.6;
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

/* Card Footer */
.footer {
    margin-top: auto;
    padding-top: 18px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-top: 1px solid var(--color-carbon-500);
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
    font-size: 20px;
    transition: all 0.22s ease;

    &:hover {
        color: var(--color-primary);
        border-color: var(--color-primary);
        background-color: var(--color-carbon-400);
        transform: translateY(-2px);
        box-shadow: 0 6px 16px rgba(0, 0, 0, 0.4);
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .card {
        border-radius: 24px;
    }

    .metaStrip {
        padding: 12px 18px 10px;
    }

    .content {
        padding: 20px;
        gap: 16px;
    }

    .title {
        font-size: 22px;
    }
}
</style>
