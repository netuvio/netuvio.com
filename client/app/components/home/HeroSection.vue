<script setup lang="ts">
import Button from "~/components/Button.vue";

const { t } = useI18n();
</script>

<template>
    <main :class="$style.hero">
        <div :class="$style.stars"></div>
        <div :class="$style.inner">
            <img src="~/../public/images/hero-circle.svg" alt="" draggable="false" />
            <div :class="$style.glass">
                <img
                    src="~/../public/images/glass-triangle-1.png"
                    alt=""
                    draggable="false"
                    :class="$style.triangle"
                />
                <img
                    src="~/../public/images/glass-hexagon-1.png"
                    alt=""
                    draggable="false"
                    :class="$style.hexagon"
                />
            </div>
            <div :class="$style.inner">
                <div>
                    <h1>
                        <span>Modern</span>&nbsp;apps, <br/>
                        <span>powerful</span>&nbsp;hosting, <br/>
                        <span>zero</span>&nbsp;hassle
                    </h1>
                    <h2>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean et sapien eget sapien pretium commodo.</h2>
                    <Button size="xl" variant="primary">
                        Learn More
                    </Button>
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

                    span {
                        color: var(--color-primary);
                    }
                }

                h2 {
                    text-align: center;
                    color: var(--color-lime-200);
                    font-weight: 400;
                    //font-size: clamp(16px, 3vw, 24px);
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
        
        .glass {
            position: absolute;
            inset: 0;
            pointer-events: none;

            .triangle {
                position: absolute;
                top: 10%;
                left: 5%;
            }

            .hexagon {
                position: absolute;
                left: 78%;
                top: 57%;
            }
        }
    }
    
    .stars {
        position: absolute;
        inset: 0;
        pointer-events: none;
        background-image: url("/images/starry-sky.png");
        background-size: 25%;
        opacity: 0.5;
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

            .glass {
                .hexagon {
                    left: 70%;
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

            .glass {
                > * {
                    transform: scale(0.7);
                }
                
                .triangle {
                    left: 0;
                    top: 0;
                }
                
                .hexagon {
                    left: 65%;
                    top: 50%;
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

            .glass {
                > * {
                    transform: scale(0.7);
                }

                .triangle {
                    left: 5%;
                    top: -5%;
                }

                .hexagon {
                    left: 55%;
                    top: 40%;
                }
            }
        }
    }
}
</style>