<script setup lang="ts">
import Button from "~/components/Button.vue";
import GlassShapes from "~/components/home/GlassShapes.vue";
import { motion } from 'motion-v'
import RandomTextAnimation from "~/components/RandomTextAnimation.vue";
import StarrySky from "~/components/home/StarrySky.vue";

const { t } = useI18n();
</script>

<template>
    <main :class="$style.hero">
        <StarrySky />
        <div :class="$style.inner">
            <img src="~/../public/images/hero-circle.svg" alt="" draggable="false" />
            <GlassShapes />
            <div :class="$style.inner">
                <div>
                    <h1>
                        <span :class="$style.colored"><RandomTextAnimation text="Modern" /></span>&nbsp;<RandomTextAnimation text="apps," /> <br/>
                        <span :class="$style.colored"><RandomTextAnimation text="powerful" /></span>&nbsp;<RandomTextAnimation text="hosting," /> <br/>
                        <span :class="$style.colored"><RandomTextAnimation text="zero" /></span>&nbsp;<RandomTextAnimation text="hassle" />
                    </h1>
                    <motion.h2
                        :initial="{ opacity: 0, y: 10 }"
                        :animate="{ opacity: 1, y: 0 }"
                        :transition="{ 
                            duration: .2,
                            ease: 'easeOut',
                            delay: 1,
                        }"
                    >Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean et sapien eget sapien pretium commodo.</motion.h2>
                    <motion.div
                        :initial="{ opacity: 0, y: 10 }"
                        :animate="{ opacity: 1, y: 0 }"
                        :transition="{ 
                            duration: .4,
                            delay: 1.2,
                        }"
                    >
                        <Button size="xl" variant="primary">
                            Learn More
                        </Button>
                    </motion.div>
                </div>
            </div>
        </div>
    </main>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.hero {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-top: 0;
    overflow-x: hidden;

    >.inner {
        position: relative;
        user-select: none;

        >img {
            user-select: none;
            min-width: 1088px;
            width: 1088px;
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
                    font-size: 80px;
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
    .hero {
        padding-top: 24px;
        
        > .inner {
            > img {
                min-width: 1000px;
                width: 1000px;
            }
            
            >.inner > div {
                h1 {
                    font-size: 70px;
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
    .hero {
        padding-top: 32px;
        margin-bottom: 100px;
        
        > .inner {
            > img {
                min-width: 700px;
                width: 700px;
            }

            >.inner > div {
                h1 {
                    font-size: 50px;
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
    .hero {
        padding-top: 32px;

        > .inner {
            > img {
                min-width: 600px;
                width: 600px;
            }

            >.inner > div {
                h1 {
                    font-size: max(10vw, 32px);
                    width: 200%;
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