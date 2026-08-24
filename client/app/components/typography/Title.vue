<script setup lang="ts">
</script>

<template>
    <h1 :class="$style.title"><slot /></h1>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.title {
    font-size: 20px;
    padding: 14px 24px;
    background-color: var(--color-background-primary);
    color: var(--color-text-primary);
    border-radius: 9999px;
    width: max-content;
    text-transform: uppercase;
}
</style>`