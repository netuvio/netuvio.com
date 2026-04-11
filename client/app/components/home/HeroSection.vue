<script setup lang="ts">
import Button from "~/components/Button.vue";

const { t } = useI18n();
</script>

<template>
    <main :class="$style.hero">
        <div :class="$style.stars"></div>
        <div :class="$style.inner">
            <img src="~/../public/images/hero-circle.svg" alt="" draggable="false" />
            <div :class="$style.inner">
                <div>
                    <h1>
                        <span>Modern</span> apps, <br/>
                        <span>powerful</span> hosting, <br/>
                        <span>zero</span> hassle
                    </h1>
                    <h2>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean et sapien eget sapien pretium commodo.</h2>
                    <Button size="xl" variant="primary">
                        Learn More
                    </Button>
                </div>
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
            </div>
        </div>
    </main>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.hero {
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-top: 0;

    >.inner {
        position: relative;

        >img {
            //position: absolute;
            //inset: 0;
            user-select: none;
            min-width: 1088px;
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
                    margin-top: 8px;
                }

                button {
                    margin-top: 40px;
                    box-shadow: 0 0 64px 0 hsl(from var(--color-primary) h s l / 0.2);
                    position: relative;
                    z-index: 1;
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

}

// tablet
@media screen and (max-width: $tabletBreakpoint) {

}

// mobile
@media screen and (max-width: $mobileBreakpoint) {

}
</style>