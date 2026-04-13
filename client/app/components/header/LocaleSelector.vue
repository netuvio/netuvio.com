<script setup lang="ts">
const { t } = useI18n();
</script>

<template>
    <select>
        <option>CZ</option>
        <option>EN</option>
    </select>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

select {
    background-color: var(--color-primary);
    outline: none;
    border: none;
    border-radius: 10000px;
    padding: 6px 12px;
    font-weight: 700;
    z-index: 1;
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>