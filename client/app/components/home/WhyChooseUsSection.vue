<script setup lang="ts">
import { Button, Card } from "@netuvio/ui/vue";
import IonLayers from '~icons/ion/layers';
import IonSparkles from '~icons/ion/sparkles';
import MaterialSymbolsHandshakeRounded from '~icons/material-symbols/handshake-rounded';
import { motion } from "motion-v";
import DrawnArrow from "~/components/DrawnArrow.vue";

const { t } = useI18n();
</script>

<template>
    <section :class="[$style.section, 'theme-secondary']" id="why-choose-us" ref="sectionRef">
        <div :class="$style.transition"></div>
        <div :class="['container', $style.container]">
            <section :class="$style.grid">
                <motion.div
                    :class="$style.title"
                    :initial="{ opacity: 0, x: -30 }"
                    :whileInView="{ opacity: 1, x: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6 }"
                >
                    <h1>{{ t('whyChooseUs.title') }}</h1>
                </motion.div>
                <motion.div
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.1 }"
                >
                    <Card>
                        <h2><i><IonLayers :style="{ fontSize: '35px' }" /></i> {{ t('whyChooseUs.flexibleServices.title') }}</h2>
                        <p>{{ t('whyChooseUs.flexibleServices.description') }}</p>
                    </Card>
                </motion.div>
                <motion.div
                    :class="$style.cta"
                    :initial="{ opacity: 0, scale: 0.95 }"
                    :whileInView="{ opacity: 1, scale: 1 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.3 }"
                >
                    <Card 
                        variant="accent"
                        base3dOffset="10"
                        
                    >
                        <section>
                            <h2>{{ t('whyChooseUs.cta.title') }}</h2>
                            <p>{{ t('whyChooseUs.cta.description') }}</p>
                        </section>
                        <section>
                            <NuxtLinkLocale to="/#contact">
                                <Button size="xl" variant="secondary">{{ t('whyChooseUs.cta.button') }} <DrawnArrow /></Button>
                            </NuxtLinkLocale>
                        </section>
                    </Card>
                </motion.div>
                <motion.div
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.2 }"
                >
                    <Card>
                        <h2><i><IonSparkles :style="{ fontSize: '32px' }" /></i> {{ t('whyChooseUs.modernTechnology.title') }}</h2>
                        <p>{{ t('whyChooseUs.modernTechnology.description') }}</p>
                    </Card>
                </motion.div>
                <motion.div
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.3 }"
                >
                    <Card>
                        <h2><i><MaterialSymbolsHandshakeRounded :style="{ fontSize: '38px' }" /></i> {{ t('whyChooseUs.personalApproach.title') }}</h2>
                        <p>{{ t('whyChooseUs.personalApproach.description') }}</p>
                    </Card>
                </motion.div>
            </section>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding: clamp(90px, 10vw, 150px) 0;
    
    .grid {
        display: grid;
        grid-template-columns: 1fr 1fr minmax(360px, 450px);
        grid-template-rows: 1fr 1fr;
        gap: 24px;
        
        >div {
            //border: 1px solid var(--color-carbon-50);
            width: 100%;
            min-height: 300px;
            height: auto;
            //padding: 24px;
            
            >div {
                width: 100%;
                height: 100%;
                
                h2 {
                    font-size: clamp(28px, 2.8vw, 40px);
                    display: flex;
                    align-items: center;
                    gap: 8px;

                    i {
                        width: 40px;
                        height: 0;
                        display: flex;
                        align-items: center;
                        justify-content: center;

                        svg {
                            position: absolute;
                        }
                    }
                }
            }
        }
        
        .title {
            border: none;
            padding: clamp(24px, 3vw, 40px);
        }
        
        .cta {
            grid-column: 3;
            grid-row: span 2;
            
            >div {
                display: flex;
                flex-direction: column;
                justify-content: space-between;
            }
        }
    }
}

.transition {
    width: 100%;
    height: 50px;
    position: absolute;
    top: -25px;
    background: repeating-linear-gradient(-45deg, var(--color-background-secondary), var(--color-background-secondary) 30px, transparent 0px, transparent 60px);
}

@media screen and (max-width: $laptopBreakpoint) {
    .section .grid {
        grid-template-columns: 1fr 1fr minmax(320px, 380px);
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .section .grid {
        grid-template-columns: repeat(2, minmax(0, 1fr));
        grid-template-rows: auto;

        .cta {
            grid-column: 1 / -1;
            grid-row: auto;
            min-height: 320px;
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .section .grid {
        grid-template-columns: 1fr;

        > div {
            min-height: auto;
            border-radius: 24px;
        }

        .title {
            padding: 0;
        }
    }
}
</style>
