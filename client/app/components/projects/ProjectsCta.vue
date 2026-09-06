<script setup lang="ts">
import { motion } from "motion-v";
import Button from "~/components/Button.vue";

const { t } = useI18n();
</script>

<template>
    <motion.div 
        :class="$style.ctaBanner"
        :initial="{ opacity: 0, y: 40 }"
        :whileInView="{ opacity: 1, y: 0 }"
        :inViewOptions="{ once: true }"
        :transition="{ duration: 0.65, ease: 'easeOut' }"
    >
        <div :class="$style.ctaDecorations">
            <img src="/images/dots.svg" :class="$style.ctaDots" alt="" />
        </div>

        <div :class="$style.ctaContent">
            <h2>{{ t("projects.cta.title") }}</h2>
            <p>{{ t("projects.cta.subtitle") }}</p>
        </div>

        <div :class="$style.ctaAction">
            <NuxtLinkLocale to="/#contact">
                <Button variant="primary" size="xl" :arrow="true">
                    {{ t("projects.cta.button") }}
                </Button>
            </NuxtLinkLocale>
        </div>
    </motion.div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.ctaBanner {
    margin-top: 50px;
    position: relative;
    overflow: hidden;
    border-radius: 36px;
    background-color: var(--color-carbon-600);
    border: 2px solid var(--color-carbon-400);
    padding: clamp(48px, 6vw, 72px);
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 40px;

    .ctaDecorations {
        position: absolute;
        inset: 0;
        pointer-events: none;
        overflow: hidden;

        .ctaDots {
            position: absolute;
            top: -20px;
            right: 25%;
            width: 240px;
            opacity: 0.15;
        }
    }

    .ctaContent {
        max-width: 650px;
        position: relative;
        z-index: 1;

        h2 {
            font-size: clamp(32px, 4vw, 52px);
            font-weight: 900;
            color: var(--color-text-primary);
            margin-bottom: 16px;
            line-height: 1.15;
            letter-spacing: -1px;
            position: relative;
            display: inline-block;
        }

        p {
            font-size: clamp(16px, 1.4vw, 19px);
            color: var(--color-carbon-50);
            line-height: 1.6;
        }
    }

    .ctaAction {
        position: relative;
        z-index: 1;
        flex-shrink: 0;
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .ctaBanner {
        flex-direction: column;
        align-items: flex-start;
        padding: 36px 28px;
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .ctaBanner {
        border-radius: 24px;
        padding: 28px 20px;
    }
}
</style>
