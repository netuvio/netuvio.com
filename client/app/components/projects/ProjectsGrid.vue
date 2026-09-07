<script setup lang="ts">
import type { Project } from "~/lib/types";
import ProjectCard from "~/components/projects/ProjectCard.vue";
import { Button } from "@netuvio/ui/vue";

defineProps<{
    projects: Project[];
    pending?: boolean;
}>();

defineEmits<{
    (e: "resetFilter"): void;
}>();

const { t } = useI18n();
</script>

<template>
    <div>
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

        <div v-else-if="projects.length > 0" :class="$style.grid">
            <ProjectCard 
                v-for="(project, index) in projects" 
                :key="project.id || project.slug"
                :project="project"
                :index="index"
            />
        </div>

        <div v-else :class="$style.emptyState">
            <div :class="$style.emptyIcon"></div>
            <p :class="$style.emptyText">{{ t("projects.noProjectsFound") }}</p>
            <Button variant="primary" size="md" @click="$emit('resetFilter')">
                {{ t("projects.resetFilter") }}
                <DrawnArrow />
            </Button>
        </div>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

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

@keyframes skeletonPulse {
    0%, 100% {
        opacity: 0.6;
    }
    50% {
        opacity: 0.25;
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .grid,
    .loadingGrid {
        grid-template-columns: 1fr;
        gap: 28px;
    }
}
</style>
