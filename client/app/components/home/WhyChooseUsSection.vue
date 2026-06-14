<script setup lang="ts">
import Button from "~/components/Button.vue";
import IonLayers from '~icons/ion/layers';
import IonSparkles from '~icons/ion/sparkles';
import MaterialSymbolsHandshakeRounded from '~icons/material-symbols/handshake-rounded';
import { motion } from "motion-v";

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
                    <h2><i><IonLayers :style="{ fontSize: '35px' }" /></i> {{ t('whyChooseUs.flexibleServices.title') }}</h2>
                    <p>{{ t('whyChooseUs.flexibleServices.description') }}</p>
                </motion.div>
                <motion.div
                    :class="$style.cta"
                    :initial="{ opacity: 0, scale: 0.95 }"
                    :whileInView="{ opacity: 1, scale: 1 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.3 }"
                >
                    <section>
                        <h2>{{ t('whyChooseUs.cta.title') }}</h2>
                        <p>{{ t('whyChooseUs.cta.description') }}</p>
                    </section>
                    <section>
                        <NuxtLinkLocale to="/#contact">
                            <Button size="xl">{{ t('whyChooseUs.cta.button') }}</Button>
                        </NuxtLinkLocale>
                    </section>
                </motion.div>
                <motion.div
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.2 }"
                >
                    <h2><i><IonSparkles :style="{ fontSize: '32px' }" /></i> {{ t('whyChooseUs.modernTechnology.title') }}</h2>
                    <p>{{ t('whyChooseUs.modernTechnology.description') }}</p>
                </motion.div>
                <motion.div
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.3 }"
                >
                    <h2><i><MaterialSymbolsHandshakeRounded :style="{ fontSize: '38px' }" /></i> {{ t('whyChooseUs.personalApproach.title') }}</h2>
                    <p>{{ t('whyChooseUs.personalApproach.description') }}</p>
                </motion.div>
            </section>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding: 150px 0;
    
    .grid {
        display: grid;
        grid-template-columns: 1fr 1fr 450px;
        grid-template-rows: 1fr 1fr;
        gap: 24px;
        
        >div {
            border: 1px solid var(--color-carbon-50);
            width: 100%;
            height: 300px;
            border-radius: 30px;
            padding: 24px;
            
            h2 {
                font-size: 40px;
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
        
        .title {
            border: none;
            padding-left: 0;
        }
        
        .cta {
            background:
                radial-gradient(600px at top left, var(--color-primary) 0%, transparent 100%),
                radial-gradient(500px at 30% 0%, hsl(from var(--color-primary) calc(h - 20) s l) 0%, transparent 100%),
                radial-gradient(500px at bottom right, var(--color-primary) 0%, transparent 100%);
            grid-column: 3;
            grid-row: span 2;
            height: 100%;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
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
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>