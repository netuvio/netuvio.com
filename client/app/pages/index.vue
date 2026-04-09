<script setup lang="ts">
const { t } = useI18n();
</script>

<template>
    <div :class="$style.wp"></div>

    <div :class="$style.centerdiv">
        <div :class="$style.logo" />
<!--        <h1>{{ t("common.comingSoon") }}</h1>-->
    </div>
</template>

<style module lang="scss">
@use "../assets/app" as *;

.wp {
    position: fixed;
    width: 100vw;
    height: 100vh;
    background-color: #11120d;
    z-index: 0;
}

.centerdiv {
    position: relative;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh; // Full viewport height
    text-align: center;
    flex-direction: column;
    z-index: 1;

    h1 {
        font-size: 64px;
    }

    p {
        font-size: 24px;
    }

    >.logo {
        width: 80vw;
        max-width: 600px;
        aspect-ratio: 1/1;
        //background-color: #50288B;
        //mask-image: url('~/../public/icons/logo.svg');
        //mask-size: contain;
        //mask-repeat: no-repeat;
        //mask-position: center;
        background-image: url(~/../public/icons/logo_neon.svg);
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        filter: drop-shadow(0px 0px 120px rgba(171, 237, 49, 0.4));
        animation: idle 8s infinite steps(1, end);

        @keyframes idle {
            0%, 10% {
                opacity: 1;
            }

            11%, 12% {
                opacity: 0.7;
            }

            13%, 30% {
                opacity: 1;
            }

            31%, 32% {
                opacity: 0.7;
            }

            33%, 60% {
                opacity: 1;
            }

            61%, 62% {
                opacity: 0.7;
            }

            63%, 64% {
                opacity: 1;
            }

            65%, 66% {
                opacity: 0.7;
            }

            67%, 100% {
                opacity: 1;
            }
        }
    }

    h1 {
        color: #50288B;
        text-transform: lowercase;
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