<script setup lang="ts">
const { t, locale, setLocale, locales } = useI18n();
</script>

<template>
    <select :value="locale" @change="setLocale(($event.target as HTMLSelectElement).value)">
        <option v-for="loc in locales" :key="loc.code" :value="loc.code">{{ loc.name }}</option>
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