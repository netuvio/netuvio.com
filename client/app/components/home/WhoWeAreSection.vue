<script setup lang="ts">

import Gradient from "~/components/home/Gradient.vue";
import {useScroll, useTransform, motion} from "motion-v";
import Title from "~/components/typography/Title.vue";

const { t } = useI18n();

</script>

<template>
    <section class="theme-secondary" :class="$style.section" id="whoWeAre">
        <div :class="['container', $style.container]">
            <Title>{{ t('whoWeAre.title') }}</Title>
            <h2 v-html="t('whoWeAre.subtitle')"></h2>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding-top: 180px;
    
    h2 {
        font-size: 48px;
        white-space: pre;
        line-height: 1.2;
        position: relative;
        width: max-content;
        margin-top: 16px;
        
        &::after {
            content: "";
            display: block;
            position: absolute;
            top: -50px;
            right: -50px;
            width: 85px;
            height: 73px;
            background-image: url("/images/star-1.svg");
            background-size: contain;
            background-repeat: no-repeat;
            background-position: center;
        }
        
        span {
            position: relative;
            display: inline-block;

            &::before {
                content: "";
                display: block;
                position: absolute;
                left: 50%;
                top: 50%;
                transform: translate(-50%, -50%);
                height: 64px;
                width: 256px;
                background-image: url("/images/squiggle-3.svg");
                background-size: contain;
                background-repeat: no-repeat;
                background-position: center;
                z-index: -1;
            }
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>
