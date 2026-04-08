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
    background-color: #101010;
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
        max-width: 500px;
        aspect-ratio: 1/1;
        background-color: #50288B;
        mask-image: url('~/../public/icons/logo.svg');
        mask-size: contain;
        mask-repeat: no-repeat;
        mask-position: center;
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