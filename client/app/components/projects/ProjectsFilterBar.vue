<script setup lang="ts">
import type { ProjectType } from "~/lib/types";

type FilterType = "all" | ProjectType;

defineProps<{
    modelValue: FilterType;
    counts: {
        all: number;
        websites: number;
        graphics: number;
    };
}>();

defineEmits<{
    (e: "update:modelValue", value: FilterType): void;
}>();

const { t } = useI18n();
</script>

<template>
    <div :class="$style.filterBarWrapper">
        <div :class="$style.filters">
            <button
                :class="[$style.filterBtn, modelValue === 'all' && $style.activeFilter]"
                @click="$emit('update:modelValue', 'all')"
            >
                <span>{{ t("projects.allFilter") }}</span>
                <span :class="$style.filterCount">{{ counts.all }}</span>
            </button>

            <button
                :class="[$style.filterBtn, modelValue === 'Website' && $style.activeFilter]"
                @click="$emit('update:modelValue', 'Website')"
            >
                <span>{{ t("projects.types.website") }}</span>
                <span :class="$style.filterCount">{{ counts.websites }}</span>
            </button>

            <button
                :class="[$style.filterBtn, modelValue === 'Graphics' && $style.activeFilter]"
                @click="$emit('update:modelValue', 'Graphics')"
            >
                <span>{{ t("projects.types.graphicDesign") }}</span>
                <span :class="$style.filterCount">{{ counts.graphics }}</span>
            </button>
        </div>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.filterBarWrapper {
    display: flex;
    justify-content: flex-start;
    padding-bottom: 8px;

    .filters {
        display: inline-flex;
        flex-wrap: wrap;
        gap: 8px;
        background-color: var(--color-carbon-700);
        border: 2px solid var(--color-carbon-400);
        border-radius: 10000px;
        padding: 8px;

        .filterBtn {
            display: inline-flex;
            align-items: center;
            gap: 10px;
            background: transparent;
            color: var(--color-carbon-100);
            border: none;
            border-radius: 10000px;
            padding: 10px 22px;
            font-size: 15px;
            font-weight: 700;
            cursor: pointer;
            transition: all 0.2s cubic-bezier(0.2, 0, 0, 1);
            user-select: none;

            &:hover {
                color: var(--color-text-primary);
                background-color: rgba(255, 255, 255, 0.06);
            }

            &.activeFilter {
                background-color: var(--color-primary);
                color: var(--color-text-secondary);

                .filterCount {
                    background-color: rgba(0, 0, 0, 0.2);
                    color: var(--color-text-secondary);
                }
            }

            .filterCount {
                background-color: var(--color-carbon-500);
                color: var(--color-carbon-100);
                font-size: 12px;
                font-weight: 700;
                padding: 2px 8px;
                border-radius: 9999px;
                transition: all 0.2s ease;
            }
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .filterBarWrapper .filters {
        width: 100%;
        border-radius: 20px;
        padding: 6px;

        .filterBtn {
            flex: 1;
            justify-content: center;
            padding: 8px 14px;
            font-size: 13px;
        }
    }
}
</style>
