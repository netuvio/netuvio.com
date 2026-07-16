<script setup lang="ts">
import Button from "~/components/Button.vue";
import GlassShapes from "~/components/home/GlassShapes.vue";
import { motion, useScroll, useTransform } from 'motion-v'
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import StarrySky from "~/components/home/StarrySky.vue";
import TypingTextAnimation from "~/components/TypingTextAnimation.vue";

const { t, locale } = useI18n();

const { scrollYProgress } = useScroll();
const textScale = useTransform(scrollYProgress, [0, 0.45], [1, 0.82]);
const bgScale = useTransform(scrollYProgress, [0, 1.5], [1, 0.82]);

</script>

<template>
    <main :class="$style.heroWrapper">
        <main :class="$style.hero">
            <StarrySky />
            <div :class="$style.inner">
                <motion.div :style="{ scale: bgScale, transformOrigin: 'center center' }">
                    <img src="/images/hero-circle.svg" alt="" draggable="false" />
                    <GlassShapes />
                </motion.div>
                <div :class="$style.inner">
                    <motion.div :style="{ scale: textScale, transformOrigin: 'center center' }">

                        <!-- nadpis -->
                        <h1>
                            <span class="sr-only">{{ t('home.hero.srOnly') }}</span>
                            <span :class="$style.colored"><RandomTextAnimation :text="t('home.hero.line1.colored')" :seoFriendly="false" :ariaHidden="true" /></span>&nbsp;<RandomTextAnimation :text="t('home.hero.line1.text')" :seoFriendly="false" :ariaHidden="true" /> <br/>
                            <span :class="$style.colored"><RandomTextAnimation :text="t('home.hero.line2.colored')" :seoFriendly="false" :ariaHidden="true" /></span>&nbsp;<RandomTextAnimation :text="t('home.hero.line2.text')" :seoFriendly="false" :ariaHidden="true" /> <br/>
                            <span :class="$style.colored"><RandomTextAnimation :text="t('home.hero.line3.colored')" :seoFriendly="false" :ariaHidden="true" /></span>&nbsp;<RandomTextAnimation :text="t('home.hero.line3.text')" :seoFriendly="false" :ariaHidden="true" />
                        </h1>

                        <!-- podnadpis -->
                        <h2>
                            <TypingTextAnimation 
                                :text="t('home.subtitle')"
                                :delay=".7"
                            />
                        </h2>
                        <motion.div
                            :initial="{ opacity: 0, y: 10 }"
                            :animate="{ opacity: 1, y: 0 }"
                            :transition="{
                                duration: .4,
                                delay: 1.8,
                            }"
                        >
                            <NuxtLinkLocale to="/#brief">
                                <Button size="xl" variant="primary">
                                    {{ t("home.learnMore") }}
                                </Button>
                            </NuxtLinkLocale>
                        </motion.div>
                    </motion.div>
                </div>
            </div>
        </main>
    </main>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.heroWrapper {
    width: 100%;
    height: clamp(760px, 75vw, 1088px);
}

.hero {
    position: fixed;
    top: 0;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-top: 0;
    width: 100%;
    overflow: hidden;

    >.inner {
        position: relative;
        user-select: none;

        >img {
            user-select: none;
            min-width: min(1088px, 165vw);
            width: min(1088px, 165vw);
        }

        >.inner {
            position: absolute;
            inset: 0;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            user-select: text;

            >div {
                width: min(100%, 100vw);
                display: flex;
                flex-direction: column;
                justify-content: center;
                align-items: center;
                padding: 0 12px;

                h1 {
                    color: var(--color-text-primary);
                    font-size: clamp(52px, 5.55vw, 80px);
                    text-align: center;

                    > .colored {
                        color: var(--color-primary);
                    }
                }

                h2 {
                    text-align: center;
                    color: var(--color-lime-200);
                    font-weight: 400;
                    margin-top: 20px;
                    max-width: 700px;
                }

                button {
                    margin-top: 40px;
                    box-shadow: 0 0 64px 0 hsl(from var(--color-primary) h s l / 0.2);
                    position: relative;
                    z-index: 1;
                }
            }
        }
    }
}

// Laptops Responsive
@media screen and (max-width: $laptopBreakpoint) {
    .heroWrapper {
        height: clamp(820px, 82vw, 1088px);
    }
    
    .hero {
        > .inner {
            > img {
                min-width: min(1000px, 150vw);
                width: min(1000px, 150vw);
            }
            
            >.inner > div {
                h1 {
                    font-size: clamp(50px, 6.8vw, 70px);
                }
                
                h2 {
                    font-size: 20px;
                    margin-top: 12px;
                }

                button {
                    padding: 16px 24px;
                    font-size: 22px;
                    min-height: 0;
                    margin-top: 32px;
                }
            }
        }
    }
}

// tablet
@media screen and (max-width: $tabletBreakpoint) {
    .heroWrapper {
        height: clamp(720px, 90vw, 870px);
    }

    .hero {
        padding-top: 48px;
        
        > .inner {
            > img {
                min-width: min(760px, 145vw);
                width: min(760px, 145vw);
            }

            >.inner > div {
                h1 {
                    font-size: clamp(38px, 6.5vw, 50px);
                }

                h2 {
                    font-size: 16px;
                }

                button {
                    padding: 16px 24px;
                    font-size: 20px;
                    min-height: 0;
                    margin-top: 32px;
                }
            }
        }
    }
}

// mobile
@media screen and (max-width: $mobileBreakpoint) {
    .heroWrapper {
        height: clamp(600px, 155vw, 720px);
    }

    .hero {
        padding-top: 64px;

        > .inner {
            > img {
                min-width: min(620px, 175vw);
                width: min(620px, 175vw);
            }

            >.inner > div {
                margin-top: -0px;
                
                h1 {
                    font-size: clamp(30px, 10vw, 42px);
                    width: min(100%, 720px);
                }

                h2 {
                    font-size: 12px;
                }

                button {
                    padding: 12px 20px;
                    font-size: 16px;
                    min-height: 0;
                    margin-top: 20px;
                }
            }
        }
    }
}
</style>
