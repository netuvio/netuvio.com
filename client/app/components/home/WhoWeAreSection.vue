<script setup lang="ts">
import { motion } from 'motion-v';
import Title from "~/components/typography/Title.vue";
import WhoWeAreServiceCard from "~/components/home/WhoWeAreServiceCard.vue";

const { t } = useI18n();

const cards = [
    {
        title: "Lorem ipsum dolor sit amet",
        image: "/images/who-we-are-card-1.svg",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    },
    {
        title: "Lorem ipsum dolor sit amet",
        image: "/images/who-we-are-card-1.svg",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    },
    {
        title: "Lorem ipsum dolor sit amet",
        image: "/images/who-we-are-card-1.svg",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    },
    {
        title: "Lorem ipsum dolor sit amet",
        image: "/images/who-we-are-card-1.svg",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    }
];
</script>

<template>
    <section class="theme-secondary" :class="$style.section" id="whoWeAre">
        <div :class="['container', $style.container]">
            <motion.div
                :initial="{ opacity: 0, y: 20 }"
                :whileInView="{ opacity: 1, y: 0 }"
                :inViewOptions="{ once: true }"
                :transition="{ duration: 0.5, ease: 'easeOut' }"
            >
                <Title>{{ t('whoWeAre.title') }}</Title>
            </motion.div>

            <motion.div
                :initial="{ opacity: 0, y: 30 }"
                :whileInView="{ opacity: 1, y: 0 }"
                :inViewOptions="{ once: true }"
                :transition="{ duration: 0.6, delay: 0.15, ease: 'easeOut' }"
            >
                <h2 :class="$style.subtitle" v-html="t('whoWeAre.subtitle')"></h2>
            </motion.div>

            <section :class="$style.cards">
                <motion.div
                    v-for="(card, index) in cards"
                    :key="index"
                    :class="$style.cardWrapper"
                    :initial="{ opacity: 0, y: 40 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.55, delay: 0.2 + index * 0.1, ease: 'easeOut' }"
                >
                    <WhoWeAreServiceCard
                        :title="card.title"
                        :image="card.image"
                    >
                        {{ card.description }}
                    </WhoWeAreServiceCard>
                </motion.div>
            </section>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding: 180px 0 250px;
    
    .container {
        .subtitle {
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
                z-index: -1;
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
        
        .cards {
            display: flex;
            gap: 32px;
            margin-top: 64px;
            justify-content: space-between;
            flex-wrap: wrap;

            .cardWrapper {
                display: flex;
            }
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>
