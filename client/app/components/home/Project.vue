<script setup lang="ts">
import { Button } from "@netuvio/netuvio-ui/vue";
import { motion } from "motion-v";
import type {ProjectType} from "~/lib/types";

const props = defineProps<{
    title: string;
    imageUrl?: string;
    type: ProjectType;
    slug: string;
}>();

const { t } = useI18n();
</script>

<template>
    <motion.div 
        :class="$style.project"
        :initial="{ opacity: 0, y: 16 }"
        :whileInView="{ opacity: 1, y: 0 }"
        :inViewOptions="{ once: true }"
        :transition="{ delay: .3 }"
    >
        <div :class="$style.info">
            <section :class="$style.top">
                <h2>{{title}}</h2>
                <h3>{{ type === "Website" ? t('projects.types.website') : t('projects.types.graphicDesign') }}</h3>
                <p><slot/></p>
            </section>
            <section :class="$style.bottom">
                <NuxtLinkLocale :to="`/projects/${slug}`">
                    <Button>{{ t('projects.learnMore') }} <DrawnArrow /></Button>
                </NuxtLinkLocale>
            </section>
        </div>
        <div :class="$style.image">
            <NuxtImg v-if="imageUrl" :src="imageUrl"  alt=""/>
<!--            TODO: add a fallback image -->
        </div>
    </motion.div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.project {
    display: flex;
    height: 600px;
    gap: 32px;
    
    .info {
        width: 40%;
        background-color: var(--color-carbon-600);
        border: 1px solid var(--color-carbon-400);
        border-radius: 30px;
        padding: 24px;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        
        .top {
            
            h2 {
                font-size: 36px;
                color: var(--color-primary);
            }
            
            h3 {
                margin-top: -4px;
                margin-bottom: 8px;
                font-size: 18px;
                opacity: 0.8;
                font-weight: normal;
            }
        }
    }
    
    .image {
        width: 60%;
        overflow: hidden;
        display: flex;
        align-items: center;
        justify-content: center;
        border-radius: 30px;
        outline: 1px solid hsla(0, 0%, 100%, 0.1);
        outline-offset: -1px;
        
        img {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .project {
        flex-direction: column;
        min-height: 0;

        .info,
        .image {
            width: 100%;
        }

        .image {
            aspect-ratio: 16 / 10;
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .project {
        gap: 20px;

        .info,
        .image {
            border-radius: 24px;
        }

        .info .top h2 {
            font-size: clamp(28px, 9vw, 36px);
        }
    }
}
</style>
