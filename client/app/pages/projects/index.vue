<script setup lang="ts">
import { ref, computed } from "vue";
import { useFetch } from "#app";
import type { Project, ProjectType } from "~/lib/types";
import ProjectsHero from "~/components/projects/ProjectsHero.vue";
import ProjectsFilterBar from "~/components/projects/ProjectsFilterBar.vue";
import ProjectsGrid from "~/components/projects/ProjectsGrid.vue";
import ProjectsCta from "~/components/projects/ProjectsCta.vue";

const { t, locale } = useI18n();

const { data: projects, pending } = await useFetch<Project[]>("/api/v1/projects", {
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
        <ProjectsHero />

        <section :class="[$style.contentSection, 'theme-primary']">
            <div :class="$style.transition"></div>
            <div :class="['container', $style.contentContainer]">
                <ProjectsFilterBar
                    v-model="activeFilter"
                    :counts="counts"
                />

                <ProjectsGrid
                    :projects="filteredProjects"
                    :pending="pending"
                    @reset-filter="activeFilter = 'all'"
                />

                <ProjectsCta />
            </div>
        </section>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.pageWrapper {
    width: 100%;
    overflow-x: clip;

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
        }
    }
}
</style>
