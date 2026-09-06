<script setup lang="ts">
import { ref, onMounted, onUnmounted, nextTick, watch } from "vue";
import { createError, useFetch, useRoute } from "#app";
import type { Project } from "~/lib/types";
import Markdown from "~/components/Markdown.vue";
import Lightbox from "~/components/lightbox/Lightbox.vue";
import ProjectDetailHero from "~/components/project-detail/ProjectDetailHero.vue";
import ProjectDetailMetaBar from "~/components/project-detail/ProjectDetailMetaBar.vue";
import ProjectDetailSidebar, { type TocItem } from "~/components/project-detail/ProjectDetailSidebar.vue";
import ProjectDetailGallery from "~/components/project-detail/ProjectDetailGallery.vue";

const { locale } = useI18n();
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
            <ProjectDetailHero :project="project" />

            <ProjectDetailMetaBar :project="project" />

            <section :class="[$style.contentSection, 'theme-primary']" id="overview">
                <div :class="['container', $style.contentContainer]">
                    <div :class="$style.editorialLayout">
                        <main :class="$style.mainArticle">
                            <div :class="$style.sectionHeader">
                                <span :class="$style.sectionCounter">01 // OVERVIEW</span>
                            </div>

                            <Markdown :markdown="project.body" />
                        </main>

                        <ProjectDetailSidebar
                            :project="project"
                            :toc-items="tocItems"
                            :active-section-id="activeSectionId"
                            @navigate="scrollToSection"
                        />
                    </div>

                    <ProjectDetailGallery :project="project" />
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
        }
    }
}

@media screen and (max-width: $laptopBreakpoint) {
    .contentSection .contentContainer {
        .editorialLayout {
            grid-template-columns: 1fr 300px;
            gap: 40px;
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .contentSection .contentContainer {
        gap: 60px;

        .editorialLayout {
            grid-template-columns: 1fr;
            gap: 40px;
        }
    }
}
</style>