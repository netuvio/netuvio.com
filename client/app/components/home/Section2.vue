<script setup lang="ts">

import Gradient from "~/components/home/Gradient.vue";

const { t } = useI18n();
</script>

<template>
    <section>
        <Gradient />
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

section {
    width: 100%;
    min-height: 1500px;
    background-color: white;
    position: relative;
    overflow-x: clip;
}
</style>