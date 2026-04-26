<script setup lang="ts">
const { t } = useI18n();
</script>

<template>
    <section :class="[$style.section, 'theme-primary']">
        <div class="container">
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    min-height: 200vh;
    z-index: 1;
    position: relative;
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>