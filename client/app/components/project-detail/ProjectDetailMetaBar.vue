<script setup lang="ts">
import { computed } from "vue";
import { motion } from "motion-v";
import type { Project } from "~/lib/types";
import TechnologyTag from "~/components/TechnologyTag.vue";
import TablerCalendar from "~icons/tabler/calendar";

const props = defineProps<{
    project: Project;
}>();

const { t, locale } = useI18n();

function formatDate(date: string): string {
    return new Intl.DateTimeFormat(locale.value === "cs" ? "cs-CZ" : "en-US", {
        year: "numeric",
        month: "short"
    }).format(new Date(date));
}

const dateRange = computed(() => {
    const startedAt = props.project.startedAt ? formatDate(props.project.startedAt) : null;
    const finishedAt = props.project.finishedAt ? formatDate(props.project.finishedAt) : null;

    if (startedAt && finishedAt) return `${startedAt} – ${finishedAt}`;
    if (!startedAt && finishedAt) return finishedAt;
    if (!finishedAt && startedAt) return `${startedAt} – ${locale.value === "cs" ? "Probíhá" : "Ongoing"}`;

    return "";
});

const isOngoing = computed(() => {
    return !!props.project.startedAt && !props.project.finishedAt;
});
</script>

<template>
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
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

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

@media screen and (max-width: $laptopBreakpoint) {
    .floatingMetaBar {
        padding: 28px 36px;
        gap: 32px;
    }
}

@media screen and (max-width: $tabletBreakpoint) {
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
}

@media screen and (max-width: $mobileBreakpoint) {
    .floatingMetaBar {
        padding: 20px 18px;
        border-radius: 20px;

        .metaColDetails {
            flex-direction: column;
            align-items: flex-start;
            gap: 16px;
        }
    }
}
</style>
