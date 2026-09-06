<script setup lang="ts">
import { createError, useFetch, useRoute } from "#app";
import type { Project } from "~/lib/types";
import Markdown from "~/components/Markdown.vue";
import Lightbox from "~/components/lightbox/Lightbox.vue";
import LightboxItem from "~/components/lightbox/LightboxItem.vue";
import TechnologyTag from "~/components/TechnologyTag.vue";
import Button from "~/components/Button.vue";
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import { computed, ref, onMounted, onUnmounted, nextTick, watch } from "vue";
import { motion } from "motion-v";
import TablerWorld from "~icons/tabler/world";
import TablerGitMerge from "~icons/tabler/git-merge";
import TablerArrowLeft from "~icons/tabler/arrow-left";
import TablerCalendar from "~icons/tabler/calendar";
import TablerPhoto from "~icons/tabler/photo";
import TablerExternalLink from "~icons/tabler/external-link";
import TablerList from "~icons/tabler/list";

interface TocItem {
    id: string;
    title: string;
    level: number;
}

const { t, locale } = useI18n();
const route = useRoute();

const { data: project, error } = await useFetch<Project>(`/api/v1/projects/${route.params.slug}`, {
    query: { locale }
});

if (error.value || !project.value) {
    throw createError({
        status: 404,
        statusText: "Project Not Found",
        fatal: true
    });
}

function formatDate(date: string): string {
    return new Intl.DateTimeFormat(locale.value === "cs" ? "cs-CZ" : "en-US", {
        year: "numeric",
        month: "short"
    }).format(new Date(date));
}

const dateRange = computed(() => {
    const startedAt = project.value?.startedAt ? formatDate(project.value.startedAt) : null;
    const finishedAt = project.value?.finishedAt ? formatDate(project.value.finishedAt) : null;

    if (startedAt && finishedAt) return `${startedAt} – ${finishedAt}`;
    if (!startedAt && finishedAt) return finishedAt;
    if (!finishedAt && startedAt) return `${startedAt} – ${locale.value === "cs" ? "Probíhá" : "Ongoing"}`;

    return "";
});

const isOngoing = computed(() => {
    return !!project.value?.startedAt && !project.value?.finishedAt;
});

const primaryImage = computed(() => {
    return project.value?.imageUrls?.[0] || "";
});

const galleryImages = computed(() => {
    if (!project.value?.imageUrls) return [];
    return project.value.imageUrls;
});

const tocItems = ref<TocItem[]>([]);
const activeSectionId = ref<string>("");
let observer: IntersectionObserver | null = null;

const updateToc = () => {
    if (typeof document === "undefined") return;
    const headings = document.querySelectorAll<HTMLElement>('article[class*="markdown"] h2, article[class*="markdown"] h3');
    const items: TocItem[] = [];

    headings.forEach((heading) => {
        if (!heading.id) {
            heading.id = heading.innerText
                .toLowerCase()
                .replace(/[^a-z0-9]+/g, "-")
                .replace(/(^-|-$)/g, "");
        }
        const title = heading.innerText.replace(/#\s*$/, "").trim();
        if (title) {
            items.push({
                id: heading.id,
                title,
                level: heading.tagName === "H2" ? 2 : 3
            });
        }
    });

    tocItems.value = items;

    if (observer) {
        observer.disconnect();
    }

    if (items.length > 0 && typeof IntersectionObserver !== "undefined") {
        observer = new IntersectionObserver(
            (entries) => {
                for (const entry of entries) {
                    if (entry.isIntersecting) {
                        activeSectionId.value = entry.target.id;
                        break;
                    }
                }
            },
            {
                rootMargin: "-80px 0px -70% 0px"
            }
        );

        items.forEach((item) => {
            const el = document.getElementById(item.id);
            if (el) observer?.observe(el);
        });
    }
};

function scrollToSection(id: string) {
    const el = document.getElementById(id);
    if (el) {
        const y = el.getBoundingClientRect().top + window.pageYOffset - 110;
        window.scrollTo({ top: y, behavior: "smooth" });
    }
}

onMounted(() => {
    nextTick(() => {
        updateToc();
    });
});

watch([locale, () => project.value?.body], () => {
    nextTick(() => {
        updateToc();
    });
});

onUnmounted(() => {
    if (observer) {
        observer.disconnect();
    }
});
</script>

<template>
    <Head>
        <Title>{{ project?.title }} • Netuvio</Title>
        <Meta name="description" :content="project?.description ?? project?.body" />
    </Head>

    <div :class="$style.pageWrapper" v-if="project">
        <Lightbox>
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

            <div :class="['container', $style.floatingMetaContainer]">
                <motion.div
                    :class="$style.floatingMetaBar"
                    :initial="{ opacity: 0, y: 30 }"
                    :animate="{ opacity: 1, y: 0 }"
                    :transition="{ duration: 0.6, delay: 0.5 }"
                >
                    <div :class="$style.metaColTech">
                        <div :class="$style.metaLabel">
                            <span>{{ t("projects.detail.techStack") }}</span>
                        </div>
                        <ul :class="$style.technologiesList">
                            <li v-for="technology in project.technologies" :key="technology">
                                <TechnologyTag :technology="technology" />
                            </li>
                        </ul>
                    </div>

                    <div :class="$style.metaDivider"></div>

                    <div :class="$style.metaColDetails">
                        <div :class="$style.metaDetailItem" v-if="dateRange">
                            <span :class="$style.detailLabel">{{ t("projects.detail.timeline") }}</span>
                            <div :class="$style.detailVal">
                                <TablerCalendar :class="$style.detailIcon" />
                                <span>{{ dateRange }}</span>
                            </div>
                        </div>

                        <div :class="$style.metaDetailItem">
                            <span :class="$style.detailLabel">{{ t("projects.detail.projectStatus") }}</span>
                            <div :class="$style.detailVal">
                                <span>{{ isOngoing ? t("projects.detail.statusLive") : t("projects.detail.statusCompleted") }}</span>
                            </div>
                        </div>
                    </div>
                </motion.div>
            </div>

            <section :class="[$style.contentSection, 'theme-primary']" id="overview">
                <div :class="['container', $style.contentContainer]">
                    <div :class="$style.editorialLayout">
                        <main :class="$style.mainArticle">
                            <div :class="$style.sectionHeader">
                                <span :class="$style.sectionCounter">01 // OVERVIEW</span>
                            </div>

                            <Markdown :markdown="project.body" />
                        </main>

                        <aside :class="$style.sidebar">
                            <div :class="$style.sidebarCard">
                                <div :class="$style.sidebarHeader" v-if="tocItems.length > 0">
                                    <TablerList :class="$style.sidebarIcon" />
                                    <h3>{{ t("projects.detail.tableOfContents") }}</h3>
                                </div>

                                <nav :class="$style.tocNav" v-if="tocItems.length > 0">
                                    <ul :class="$style.tocList">
                                        <li
                                            v-for="item in tocItems"
                                            :key="item.id"
                                            :class="[$style.tocItem, item.level === 3 && $style.tocSubItem]"
                                        >
                                            <a
                                                :href="`#${item.id}`"
                                                :class="[$style.tocLink, activeSectionId === item.id && $style.activeTocLink]"
                                                @click.prevent="scrollToSection(item.id)"
                                            >
                                                {{ item.title }}
                                            </a>
                                        </li>
                                    </ul>
                                </nav>

                                <div :class="$style.sidebarActions" v-if="project.websiteUrl || project.sourceCodeUrl">
                                    <a
                                        v-if="project.websiteUrl"
                                        :href="project.websiteUrl"
                                        target="_blank"
                                        rel="noopener noreferrer"
                                        :class="$style.actionLink"
                                    >
                                        <Button variant="primary" size="md" :arrow="true" :block="true">
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
                                        <Button variant="secondary" size="md" :block="true">
                                            <TablerGitMerge :class="$style.buttonIcon" />
                                            {{ t("projects.sourceCode") }}
                                        </Button>
                                    </a>
                                </div>
                            </div>
                        </aside>
                    </div>

                    <section :class="$style.gallerySection" id="gallery" v-if="galleryImages.length > 0">
                        <div :class="$style.sectionHeader">
                            <span :class="$style.sectionCounter">02 // VISUAL ASSETS</span>
                            <h2 :class="$style.sectionHeading">{{ t("projects.detail.gallery") }}</h2>
                            <p :class="$style.gallerySubtitle">{{ t("projects.detail.gallerySubtitle") }}</p>
                        </div>

                        <div :class="$style.galleryGrid">
                            <div
                                v-for="(url, index) in galleryImages"
                                :key="url"
                                :class="$style.galleryCard"
                            >
                                <LightboxItem
                                    :src="url"
                                    :alt="`${project.title} - Asset #${index + 1}`"
                                    :width="800"
                                    :class-name="$style.galleryLightboxItem"
                                    :image-class-name="$style.galleryImage"
                                />
                            </div>
                        </div>
                    </section>
                </div>
            </section>
        </Lightbox>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.pageWrapper {
    width: 100%;
    overflow-x: clip;
    background-color: var(--color-background-primary);
}

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
        bottom: -25px;
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
        mask-size: 800px;
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

                    .blackHole {
                        position: absolute;
                        inset: -20px;
                        border-radius: 40px;
                        background: radial-gradient(
                            circle closest-side,
                            rgba(0, 0, 0, 0.4) 0%,
                            transparent 100%
                        );
                        filter: blur(30px);
                        z-index: 0;
                        pointer-events: none;
                    }

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

.floatingMetaContainer {
    position: relative;
    z-index: 20;
    margin-top: -60px;
}

.floatingMetaBar {
    background-color: var(--color-carbon-700);
    border: 2px solid var(--color-carbon-400);
    border-radius: 36px;
    padding: 32px 48px;
    box-shadow: 0 24px 48px rgba(0, 0, 0, 0.5),
                0 0 32px 0 rgba(0, 0, 0, 0.3);
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 48px;

    .metaColTech {
        display: flex;
        flex-direction: column;
        gap: 12px;
        flex: 1;

        .metaLabel {
            font-size: 14px;
            font-weight: 800;
            text-transform: uppercase;
            letter-spacing: 1.5px;
            color: var(--color-carbon-100);
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .technologiesList {
            list-style: none;
            padding: 0;
            margin: 0;
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }
    }

    .metaDivider {
        width: 1px;
        height: 60px;
        background-color: var(--color-carbon-400);
    }

    .metaColDetails {
        display: flex;
        align-items: center;
        gap: 40px;

        .metaDetailItem {
            display: flex;
            flex-direction: column;
            gap: 6px;

            .detailLabel {
                font-size: 12px;
                font-family: monospace;
                text-transform: uppercase;
                letter-spacing: 1px;
                color: var(--color-carbon-200);
            }

            .detailVal {
                display: flex;
                align-items: center;
                gap: 8px;
                font-size: 16px;
                font-weight: 700;
                color: var(--color-text-primary);
            }
        }
    }
}

.contentSection {
    padding: 70px 0 140px;
    background-color: var(--color-background-primary);
    position: relative;

    .contentContainer {
        display: flex;
        flex-direction: column;
        gap: 80px;

        .editorialLayout {
            display: grid;
            grid-template-columns: 1fr 340px;
            gap: 60px;
            align-items: start;

            .mainArticle {
                display: flex;
                flex-direction: column;
                gap: 32px;

                .sectionHeader {
                    display: flex;
                    flex-direction: column;
                    gap: 8px;

                    .sectionCounter {
                        font-family: monospace;
                        font-size: 13px;
                        font-weight: 800;
                        color: var(--color-primary);
                        letter-spacing: 2px;
                        text-transform: uppercase;
                    }
                }
            }

            .sidebar {
                position: sticky;
                top: 120px;

                .sidebarCard {
                    background-color: var(--color-carbon-700);
                    border: 2px solid var(--color-carbon-400);
                    border-radius: 28px;
                    padding: 24px;
                    display: flex;
                    flex-direction: column;
                    gap: 20px;
                    box-shadow: 0 16px 32px rgba(0, 0, 0, 0.3);

                    .sidebarHeader {
                        display: flex;
                        align-items: center;
                        gap: 10px;
                        padding-bottom: 12px;
                        border-bottom: 1px solid var(--color-carbon-500);

                        .sidebarIcon {
                            font-size: 20px;
                            color: var(--color-primary);
                        }

                        h3 {
                            font-size: 18px;
                            font-weight: 800;
                            color: var(--color-text-primary);
                            margin: 0;
                        }
                    }

                    .tocNav {
                        max-height: 380px;
                        overflow-y: auto;
                        padding-right: 4px;

                        &::-webkit-scrollbar {
                            width: 4px;
                        }

                        &::-webkit-scrollbar-thumb {
                            background: var(--color-carbon-400);
                            border-radius: 4px;
                        }

                        .tocList {
                            list-style: none;
                            padding: 0;
                            margin: 0;
                            display: flex;
                            flex-direction: column;
                            gap: 4px;

                            .tocItem {
                                .tocLink {
                                    display: block;
                                    font-size: 14px;
                                    line-height: 1.4;
                                    color: var(--color-carbon-100);
                                    text-decoration: none;
                                    padding: 6px 12px;
                                    border-radius: 8px;
                                    transition: all 0.2s ease;

                                    &:hover {
                                        color: var(--color-text-primary);
                                        background-color: rgba(255, 255, 255, 0.05);
                                    }

                                    &.activeTocLink {
                                        color: var(--color-primary);
                                        background-color: hsl(from var(--color-primary) h s l / 0.08);
                                    }
                                }

                                &.tocSubItem {
                                    padding-left: 14px;

                                    .tocLink {
                                        font-size: 13px;
                                        color: var(--color-carbon-200);

                                        &.activeTocLink {
                                            color: var(--color-primary);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    .sidebarActions {
                        display: flex;
                        flex-direction: column;
                        gap: 10px;
                        padding-top: 14px;
                        border-top: 1px solid var(--color-carbon-500);
                    }
                }
            }
        }

        .gallerySection {
            display: flex;
            flex-direction: column;
            gap: 32px;
            padding-top: 40px;
            border-top: 1px solid var(--color-carbon-500);

            .sectionHeader {
                display: flex;
                flex-direction: column;
                gap: 8px;

                .sectionCounter {
                    font-family: monospace;
                    font-size: 13px;
                    font-weight: 800;
                    color: var(--color-primary);
                    letter-spacing: 2px;
                    text-transform: uppercase;
                }

                .sectionHeading {
                    font-size: clamp(32px, 3.8vw, 48px);
                    font-weight: 900;
                    letter-spacing: -1px;
                    color: var(--color-text-primary);
                    margin: 0;
                }

                .gallerySubtitle {
                    font-size: 17px;
                    color: var(--color-carbon-100);
                    margin: 0;
                }
            }

            .galleryGrid {
                display: grid;
                grid-template-columns: repeat(3, 1fr);
                gap: 24px;

                .galleryCard {
                    position: relative;
                    border-radius: 24px;
                    overflow: hidden;
                    background-color: var(--color-carbon-700);
                    border: 2px solid var(--color-carbon-400);
                    transition: transform 0.3s cubic-bezier(0.2, 0, 0, 1),
                                border-color 0.3s ease,
                                box-shadow 0.3s ease;

                    &:hover {
                        border-color: var(--color-primary);
                        box-shadow: 0 16px 32px rgba(0, 0, 0, 0.4),
                                    0 0 24px hsl(from var(--color-primary) h s l / 0.15);

                        .galleryImage {
                            transform: scale(1.05);
                        }

                        .galleryCardOverlay {
                            opacity: 1;
                        }
                    }

                    .galleryLightboxItem {
                        width: 100%;
                        aspect-ratio: 4 / 3;
                        border-radius: 0;
                        overflow: hidden;

                        .galleryImage {
                            width: 100%;
                            height: 100%;
                            object-fit: cover;
                            transition: transform 0.4s ease;
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

    .floatingMetaBar {
        padding: 28px 36px;
        gap: 32px;
    }

    .contentSection .contentContainer {
        .editorialLayout {
            grid-template-columns: 1fr 300px;
            gap: 40px;
        }

        .gallerySection .galleryGrid {
            grid-template-columns: repeat(2, 1fr);
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

    .floatingMetaContainer {
        margin-top: -40px;
    }

    .floatingMetaBar {
        flex-direction: column;
        align-items: flex-start;
        padding: 28px 24px;
        gap: 24px;
        border-radius: 28px;

        .metaDivider {
            width: 100%;
            height: 1px;
        }

        .metaColDetails {
            width: 100%;
            justify-content: space-between;
        }
    }

    .contentSection .contentContainer {
        gap: 60px;

        .editorialLayout {
            grid-template-columns: 1fr;
            gap: 40px;

            .sidebar {
                position: static;
            }
        }

        .gallerySection .galleryGrid {
            grid-template-columns: repeat(2, 1fr);
            gap: 16px;
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

    .floatingMetaBar {
        padding: 20px 18px;
        border-radius: 20px;

        .metaColDetails {
            flex-direction: column;
            align-items: flex-start;
            gap: 16px;
        }
    }

    .contentSection .contentContainer {
        .gallerySection .galleryGrid {
            grid-template-columns: 1fr;
        }
    }
}

@keyframes radarPulse {
    0% {
        transform: scale(0.95);
        box-shadow: 0 0 0 0 hsla(81, 84%, 56%, 0.7);
    }
    70% {
        transform: scale(1.15);
        box-shadow: 0 0 0 8px hsla(81, 84%, 56%, 0);
    }
    100% {
        transform: scale(0.95);
        box-shadow: 0 0 0 0 hsla(81, 84%, 56%, 0);
    }
}
</style>