<script setup lang="ts">
import { computed } from "vue";
import type { Project } from "~/lib/types";
import LightboxItem from "~/components/lightbox/LightboxItem.vue";

const props = defineProps<{
    project: Project;
}>();

const { t } = useI18n();

const galleryImages = computed(() => {
    return props.project.imageUrls || [];
});
</script>

<template>
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
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

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

@media screen and (max-width: $laptopBreakpoint) {
    .gallerySection .galleryGrid {
        grid-template-columns: repeat(2, 1fr);
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .gallerySection .galleryGrid {
        grid-template-columns: repeat(2, 1fr);
        gap: 16px;
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .gallerySection .galleryGrid {
        grid-template-columns: 1fr;
    }
}
</style>
