<script setup lang="ts">
import { Button, Card } from "@netuvio/ui/vue";
import IonLayers from '~icons/ion/layers';
import IonSparkles from '~icons/ion/sparkles';
import MaterialSymbolsHandshakeRounded from '~icons/material-symbols/handshake-rounded';
import MaterialSymbolsCheckCircleRounded from '~icons/material-symbols/check-circle-rounded';
import { motion } from "motion-v";
import DrawnArrow from "~/components/DrawnArrow.vue";

const { t, tm } = useI18n();
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
                    <div :class="$style.titleContent">
                        <h1>{{ t('whyChooseUs.title') }}</h1>
                        <p :class="$style.titleLead">{{ t('whyChooseUs.subtitle') }}</p>
                    </div>
                </motion.div>

                <motion.div
                    :class="$style.flexibleCard"
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.1 }"
                >
                    <Card>
                        <div :class="$style.cardBody">
                            <div :class="$style.cardHeader">
                                <h2><i><IonLayers :style="{ fontSize: '32px' }" /></i> {{ t('whyChooseUs.flexibleServices.title') }}</h2>
                                <p>{{ t('whyChooseUs.flexibleServices.description') }}</p>
                            </div>
                            <ul :class="$style.bulletList">
                                <li v-for="point in (tm('whyChooseUs.flexibleServices.bulletPoints') as string[])" :key="point">
                                    <MaterialSymbolsCheckCircleRounded :class="$style.checkIcon" />
                                    <span>{{ point }}</span>
                                </li>
                            </ul>
                        </div>
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
                        <div :class="$style.ctaBody">
                            <div :class="$style.ctaContent">
                                <h2>{{ t('whyChooseUs.cta.title') }}</h2>
                                <p>{{ t('whyChooseUs.cta.description') }}</p>
                                <ul :class="[$style.bulletList, $style.ctaList]">
                                    <li v-for="point in (tm('whyChooseUs.cta.bulletPoints') as string[])" :key="point">
                                        <MaterialSymbolsCheckCircleRounded :class="[$style.checkIcon, $style.ctaCheckIcon]" />
                                        <span>{{ point }}</span>
                                    </li>
                                </ul>
                            </div>
                            <div :class="$style.ctaAction">
                                <NuxtLinkLocale to="/#contact">
                                    <Button size="xl" variant="secondary">{{ t('whyChooseUs.cta.button') }} <DrawnArrow /></Button>
                                </NuxtLinkLocale>
                            </div>
                        </div>
                    </Card>
                </motion.div>

                <motion.div
                    :class="$style.modernCard"
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.2 }"
                >
                    <Card>
                        <div :class="$style.cardBody">
                            <div :class="$style.cardHeader">
                                <h2><i><IonSparkles :style="{ fontSize: '30px' }" /></i> {{ t('whyChooseUs.modernTechnology.title') }}</h2>
                                <p>{{ t('whyChooseUs.modernTechnology.description') }}</p>
                            </div>
                            <ul :class="$style.bulletList">
                                <li v-for="point in (tm('whyChooseUs.modernTechnology.bulletPoints') as string[])" :key="point">
                                    <MaterialSymbolsCheckCircleRounded :class="$style.checkIcon" />
                                    <span>{{ point }}</span>
                                </li>
                            </ul>
                        </div>
                    </Card>
                </motion.div>

                <motion.div
                    :class="$style.personalCard"
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.3 }"
                >
                    <Card>
                        <div :class="$style.cardBody">
                            <div :class="$style.cardHeader">
                                <h2><i><MaterialSymbolsHandshakeRounded :style="{ fontSize: '34px' }" /></i> {{ t('whyChooseUs.personalApproach.title') }}</h2>
                                <p>{{ t('whyChooseUs.personalApproach.description') }}</p>
                            </div>
                            <ul :class="$style.bulletList">
                                <li v-for="point in (tm('whyChooseUs.personalApproach.bulletPoints') as string[])" :key="point">
                                    <MaterialSymbolsCheckCircleRounded :class="$style.checkIcon" />
                                    <span>{{ point }}</span>
                                </li>
                            </ul>
                        </div>
                    </Card>
                </motion.div>
            </section>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding: clamp(60px, 7vw, 110px) 0;
    
    .grid {
        display: grid;
        grid-template-columns: 1fr 1fr minmax(320px, 390px);
        gap: 24px;
        align-items: stretch;
        
        >div {
            width: 100%;
            display: flex;
            
            >div {
                width: 100%;
                display: flex;
                flex-direction: column;
            }
        }
        
        .title {
            grid-column: 1;
            grid-row: 1;
            padding: 12px 8px;
            display: flex;
            flex-direction: column;
            justify-content: center;

            .titleContent {
                display: flex;
                flex-direction: column;
                gap: 12px;
            }

            h1 {
                font-size: clamp(28px, 2.8vw, 44px);
                line-height: 1.15;
                font-weight: 900;
                margin: 0;
                letter-spacing: -1px;
            }

            .titleLead {
                font-size: 15px;
                line-height: 1.5;
                color: var(--color-carbon-300);
                margin: 0;
            }
        }

        .flexibleCard {
            grid-column: 2;
            grid-row: 1;
        }

        .cta {
            grid-column: 3;
            grid-row: 1 / span 2;
        }

        .modernCard {
            grid-column: 1;
            grid-row: 2;
        }

        .personalCard {
            grid-column: 2;
            grid-row: 2;
        }
    }
}

.cardBody {
    display: flex;
    flex-direction: column;
    height: 100%;
    gap: 16px;

    .cardHeader {
        display: flex;
        flex-direction: column;
        gap: 8px;

        h2 {
            font-size: clamp(19px, 1.35vw, 26px);
            display: flex;
            align-items: center;
            gap: 10px;
            margin: 0;
            line-height: 1.25;

            i {
                width: 36px;
                height: 0;
                display: flex;
                align-items: center;
                justify-content: center;
                flex-shrink: 0;

                svg {
                    position: absolute;
                }
            }
        }
        
        p {
            font-size: 15px;
            line-height: 1.5;
            color: var(--color-carbon-350);
            margin: 0;
        }
    }
}

.bulletList {
    list-style: none;
    padding: 14px 0 0 0;
    margin: auto 0 0 0;
    border-top: 1px solid rgba(0, 0, 0, 0.08);
    display: flex;
    flex-direction: column;
    gap: 8px;

    li {
        display: flex;
        align-items: flex-start;
        gap: 9px;
        font-size: 14px;
        line-height: 1.4;
        font-weight: 500;
        color: var(--color-carbon-400);

        span {
            flex: 1;
        }
    }
}

.checkIcon {
    width: 17px;
    height: 17px;
    font-size: 17px;
    color: var(--color-lime-500);
    flex-shrink: 0;
    margin-top: 1px;
}

.ctaBody {
    display: flex;
    flex-direction: column;
    height: 100%;
    gap: 20px;

    .ctaContent {
        display: flex;
        flex-direction: column;
        gap: 12px;

        h2 {
            font-size: clamp(20px, 1.4vw, 28px);
            line-height: 1.25;
            margin: 0;
        }

        p {
            font-size: 15px;
            line-height: 1.5;
            color: var(--color-carbon-400);
            margin: 0;
        }
    }

    .ctaAction {
        margin-top: auto;
        padding-top: 12px;
    }
}

.ctaList {
    margin-top: 4px;
    border-top: 1px solid rgba(0, 0, 0, 0.12);

    li {
        color: var(--color-carbon-900);
    }

    .ctaCheckIcon {
        color: var(--color-carbon-800);
    }
}

.transition {
    width: 100%;
    height: 50px;
    position: absolute;
    top: -25px;
    background: repeating-linear-gradient(-45deg, var(--color-background-secondary), var(--color-background-secondary) 30px, transparent 0px, transparent 60px);
}

@media screen and (max-width: 1180px) {
    .section {
        padding: clamp(50px, 6vw, 80px) 0;

        .grid {
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: 20px;

            .title {
                grid-column: 1 / -1;
                grid-row: auto;
                padding: 4px 0 12px 0;

                .titleContent {
                    max-width: 680px;
                }
            }

            .flexibleCard {
                grid-column: 1;
                grid-row: auto;
            }

            .modernCard {
                grid-column: 2;
                grid-row: auto;
            }

            .personalCard {
                grid-column: 1;
                grid-row: auto;
            }

            .cta {
                grid-column: 2;
                grid-row: auto;
            }
        }
    }
}

@media screen and (max-width: 720px) {
    .section {
        padding: 40px 0;

        .grid {
            grid-template-columns: 1fr;
            gap: 16px;

            .title {
                grid-column: 1;
                padding: 0 0 6px 0;

                h1 {
                    font-size: clamp(26px, 7vw, 34px);
                }
            }

            .flexibleCard,
            .modernCard,
            .personalCard,
            .cta {
                grid-column: 1;
                grid-row: auto;
            }
        }
    }
}
</style>
