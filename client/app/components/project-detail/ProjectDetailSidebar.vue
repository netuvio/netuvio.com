<script setup lang="ts">
import type { Project } from "~/lib/types";
import Button from "~/components/Button.vue";
import TablerList from "~icons/tabler/list";
import TablerWorld from "~icons/tabler/world";
import TablerGitMerge from "~icons/tabler/git-merge";

export interface TocItem {
    id: string;
    title: string;
    level: number;
}

defineProps<{
    project: Project;
    tocItems: TocItem[];
    activeSectionId: string;
}>();

defineEmits<{
    (e: "navigate", id: string): void;
}>();

const { t } = useI18n();
</script>

<template>
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
                            @click.prevent="$emit('navigate', item.id)"
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
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.actionLink {
    text-decoration: none;

    .buttonIcon {
        font-size: 18px;
        margin-right: 4px;
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

@media screen and (max-width: $tabletBreakpoint) {
    .sidebar {
        position: static;
    }
}
</style>
