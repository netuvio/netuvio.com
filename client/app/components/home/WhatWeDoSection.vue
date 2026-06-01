<script setup lang="ts">

import {useScroll, useTransform, useMotionValueEvent, AnimatePresence} from "motion-v";
import ServiceCard from "~/components/home/ServiceCard.vue";

const { t } = useI18n();

const sectionRef = ref<HTMLElement | null>(null);

const { scrollYProgress } = useScroll({
    target: sectionRef
});

const scrollSection = useTransform(scrollYProgress, (v) => {
    if (v < 0.001) return "notScrolled";
    if (v < 0.33) return "first";
    if (v < 0.66) return "second";
    return "third";
});

const currentSection = ref("notScrolled");

const scrollToProgress = (progress: number) => {
    if (!sectionRef.value) return;
    const rect = sectionRef.value.getBoundingClientRect();
    const scrollTop = window.scrollY || document.documentElement.scrollTop;
    
    const startScroll = rect.top + scrollTop - window.innerHeight;
    const totalScrollable = rect.height + window.innerHeight;
    
    window.scrollTo({
        top: startScroll + progress * totalScrollable,
        behavior: "smooth"
    });
};

onMounted(() => {
    useMotionValueEvent(scrollSection, "change", (v) => {
        currentSection.value = v;
    });
});
</script>

<template>
    <section :class="[$style.sectionContainer, 'theme-secondary']">
        <section ref="sectionRef" :class="$style.section">
            <div :class="$style.spacer"></div>
            <div :class="['container', $style.container]" id="services">
                <div :class="[$style.wrapper, $style[currentSection]]">
                    <ul :class="$style.servicesList">
                        <li @click="scrollToProgress(0.33)">Design</li>
                        <li @click="scrollToProgress(0.50)">Development</li>
                        <li @click="scrollToProgress(0.66)">Hosting</li>
                    </ul>
                    
                    <div :class="$style.description">
                        <AnimatePresence mode="wait">
                            <ServiceCard
                                v-if="currentSection === 'first'"
                                key="first"
                                title="Lorem ipsum dolor sit amet"
                                image="/images/design.png"
                                :bulletPoints="[
                                    'Lorem ipsum dolor sit amet',
                                    'Lorem ipsum dolor sit amet',
                                    'Lorem ipsum dolor sit amet'
                                ]"
                            >
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis ipsum sed nisi finibus dapibus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae.
                            </ServiceCard>
                            <ServiceCard
                                v-if="currentSection === 'second'"
                                key="second"
                                title="Lorem ipsum dolor sit amet"
                                image="/images/development.png"
                                :bulletPoints="[
                                    'Lorem ipsum dolor sit amet',
                                    'Lorem ipsum dolor sit amet',
                                    'Lorem ipsum dolor sit amet'
                                ]"
                            >
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis ipsum sed nisi finibus dapibus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae.
                            </ServiceCard>
                            <ServiceCard
                                v-if="currentSection === 'third'"
                                key="third"
                                title="Lorem ipsum dolor sit amet"
                                image="/images/hosting.png"
                                :bulletPoints="[
                                    'Lorem ipsum dolor sit amet',
                                    'Lorem ipsum dolor sit amet',
                                    'Lorem ipsum dolor sit amet'
                                ]"
                            >
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque quis ipsum sed nisi finibus dapibus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae.
                            </ServiceCard>
                        </AnimatePresence>
                    </div>
                </div>
            </div>
        </section>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.sectionContainer {
    width: 100%;
    min-height: 4000px;
    height: 4000px;
    position: relative;
    overflow-x: clip;
    padding-bottom: 200px;
}

.section {
    position: relative;
    overflow-x: clip;
    padding-top: 800px;
    height: 100%;

    .spacer {
        //padding-bottom: 600px;
    }

    .container {
        height: 100%;

        .wrapper {
            position: sticky;
            z-index: 1;
            top: 50%;
            transform: translateY(-50%);
            
            .servicesList {
                list-style: none;
                padding: 0;
                margin-bottom: 32px;

                li {
                    font-weight: 700;
                    font-size: 64px;
                    transition: color 0.2s ease-in-out;
                    cursor: pointer;
                    line-height: 1.2em;
                }
            }
            
            .description {
                height: 640px;
                transition: opacity 0.4s ease-in-out;

                //div {
                //    display: flex;
                //    flex-direction: row;
                //    gap: 32px;
                //    
                //    img {
                //        object-fit: contain;
                //        border-radius: 20px;
                //        //border: 1px solid rgba(0, 0, 0, 0.2);
                //        //background-color: hsl(0, 0%, 98%);
                //        width: 400px;
                //        height: 400px;
                //    }
                //    
                //    p {
                //        border-radius: 36px;
                //        padding: 16px;
                //        background-color: hsl(0, 0%, 98%);
                //        border: 1px solid hsl(0, 0%, 95%);
                //    }
                //}
            }
            
            &.first {
                .servicesList li:nth-child(1) {
                    color: var(--color-primary);
                }
            }

            &.notScrolled {
                .description {
                    opacity: 0;
                }
            }

            &.second {
                .servicesList li:nth-child(2) {
                    color: var(--color-primary);
                }
            }

            &.third {
                .servicesList li:nth-child(3) {
                    color: var(--color-primary);
                }
            }
        }
    }
}
</style>