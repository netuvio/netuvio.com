<script setup lang="ts">
import { ref, watch, onMounted, onUnmounted } from "vue";
import type { Project } from "~/lib/types";
import Button from "~/components/Button.vue";
import TablerList from "~icons/tabler/list";
import TablerWorld from "~icons/tabler/world";
import TablerGitMerge from "~icons/tabler/git-merge";
import TablerX from "~icons/tabler/x";

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

const emit = defineEmits<{
    (e: "navigate", id: string): void;
}>();

const { t } = useI18n();

const isDrawerOpen = ref(false);

const handleNavigate = (id: string) => {
    emit("navigate", id);
    isDrawerOpen.value = false;
};

const handleKeyDown = (e: KeyboardEvent) => {
    if (e.key === "Escape" && isDrawerOpen.value) {
        isDrawerOpen.value = false;
    }
};

watch(isDrawerOpen, (isOpen) => {
    if (typeof document === "undefined") return;
    if (isOpen) {
        document.body.style.overflow = "hidden";
    } else {
        document.body.style.overflow = "";
    }
});

onMounted(() => {
    window.addEventListener("keydown", handleKeyDown);
});

onUnmounted(() => {
    window.removeEventListener("keydown", handleKeyDown);
    if (typeof document !== "undefined") {
        document.body.style.overflow = "";
    }
});
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
                            @click.prevent="handleNavigate(item.id)"
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

        <button
            v-if="tocItems.length > 0"
            :class="$style.floatingToggleBtn"
            @click="isDrawerOpen = true"
            type="button"
            :aria-label="t('projects.detail.tableOfContents')"
        >
            <TablerList :class="$style.toggleIcon" />
            <span :class="$style.toggleText">{{ t("projects.detail.tableOfContents") }}</span>
        </button>

        <Teleport to="body">
            <Transition name="fade">
                <div
                    v-if="isDrawerOpen"
                    :class="$style.drawerBackdrop"
                    @click="isDrawerOpen = false"
                />
            </Transition>

            <Transition name="drawer-slide">
                <aside v-if="isDrawerOpen" :class="$style.drawerPanel" role="dialog" aria-modal="true">
                    <div :class="$style.drawerHeader">
                        <div :class="$style.drawerTitleWrapper">
                            <TablerList :class="$style.drawerTitleIcon" />
                            <h3>{{ t("projects.detail.tableOfContents") }}</h3>
                        </div>
                        <button
                            :class="$style.drawerCloseBtn"
                            @click="isDrawerOpen = false"
                            type="button"
                            aria-label="Close"
                        >
                            <TablerX />
                        </button>
                    </div>

                    <div :class="$style.drawerBody">
                        <nav :class="$style.drawerTocNav" v-if="tocItems.length > 0">
                            <ul :class="$style.tocList">
                                <li
                                    v-for="item in tocItems"
                                    :key="item.id"
                                    :class="[$style.tocItem, item.level === 3 && $style.tocSubItem]"
                                >
                                    <a
                                        :href="`#${item.id}`"
                                        :class="[$style.tocLink, activeSectionId === item.id && $style.activeTocLink]"
                                        @click.prevent="handleNavigate(item.id)"
                                    >
                                        {{ item.title }}
                                    </a>
                                </li>
                            </ul>
                        </nav>
                    </div>

                    <div :class="$style.drawerFooter" v-if="project.websiteUrl || project.sourceCodeUrl">
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
                </aside>
            </Transition>
        </Teleport>
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
    align-self: start;

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
            padding: 8px 12px;
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

.floatingToggleBtn {
    display: none;
    position: fixed;
    bottom: 24px;
    right: 24px;
    z-index: 1000;
    align-items: center;
    gap: 10px;
    background-color: var(--color-carbon-700);
    border: 2px solid var(--color-primary);
    border-radius: 9999px;
    padding: 12px 22px;
    color: var(--color-text-primary);
    cursor: pointer;
    backdrop-filter: blur(12px);
    transition: all 0.25s cubic-bezier(0.2, 0, 0, 1);

    .toggleIcon {
        font-size: 18px;
        color: var(--color-primary);
    }

    .toggleText {
        font-size: 14px;
        font-weight: 700;
        letter-spacing: 0.3px;
    }

    &:hover {
        transform: translateY(-2px) scale(1.03);
        background-color: var(--color-carbon-600);
    }

    &:active {
        transform: translateY(0) scale(0.98);
    }
}

.drawerBackdrop {
    position: fixed;
    inset: 0;
    background-color: rgba(0, 0, 0, 0.75);
    backdrop-filter: blur(4px);
    z-index: 2001;
}

.drawerPanel {
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    width: 360px;
    max-width: 86vw;
    background-color: var(--color-carbon-700);
    border-left: 2px solid var(--color-carbon-400);
    z-index: 2002;
    display: flex;
    flex-direction: column;
    box-shadow: -20px 0 40px rgba(0, 0, 0, 0.6);

    .drawerHeader {
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 24px;
        border-bottom: 1px solid var(--color-carbon-500);

        .drawerTitleWrapper {
            display: flex;
            align-items: center;
            gap: 10px;

            .drawerTitleIcon {
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

        .drawerCloseBtn {
            background: transparent;
            border: 1px solid var(--color-carbon-400);
            border-radius: 50%;
            width: 36px;
            height: 36px;
            display: flex;
            align-items: center;
            justify-content: center;
            color: var(--color-carbon-100);
            font-size: 18px;
            cursor: pointer;
            transition: all 0.2s ease;

            &:hover {
                color: var(--color-text-primary);
                border-color: var(--color-primary);
                background-color: rgba(255, 255, 255, 0.05);
            }
        }
    }

    .drawerBody {
        flex: 1;
        overflow-y: auto;
        padding: 20px 24px;

        &::-webkit-scrollbar {
            width: 4px;
        }

        &::-webkit-scrollbar-thumb {
            background: var(--color-carbon-400);
            border-radius: 4px;
        }
    }

    .drawerFooter {
        display: flex;
        flex-direction: column;
        gap: 10px;
        padding: 20px 24px;
        border-top: 1px solid var(--color-carbon-500);
        background-color: var(--color-carbon-700);
    }
}

@media screen and (max-width: 1500px) {
    .sidebar {
        display: none;
    }

    .floatingToggleBtn {
        display: inline-flex;
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .floatingToggleBtn {
        bottom: 20px;
        right: 20px;
        padding: 10px 18px;

        .toggleText {
            font-size: 13px;
        }
    }
}
</style>

<style scoped>
:deep(.fade-enter-active),
:deep(.fade-leave-active) {
    transition: opacity 0.25s ease;
}

:deep(.fade-enter-from),
:deep(.fade-leave-to) {
    opacity: 0;
}

:deep(.drawer-slide-enter-active),
:deep(.drawer-slide-leave-active) {
    transition: transform 0.3s cubic-bezier(0.16, 1, 0.3, 1);
}

:deep(.drawer-slide-enter-from),
:deep(.drawer-slide-leave-to) {
    transform: translateX(100%);
}
</style>
