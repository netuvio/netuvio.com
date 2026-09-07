<script setup lang="ts">
import { Select, type SelectOption, type SelectSize } from "@netuvio/ui/vue";
import { computed } from "vue";

const { locale, setLocale, locales } = useI18n();

const options = computed<SelectOption[]>(() =>
    locales.value.map((loc: any) => {
        const code = typeof loc === "string" ? loc : loc.code;
        const name = typeof loc === "string" ? loc : loc.name;
        return {
            value: code,
            label: name || code.toUpperCase(),
        };
    })
);

const onLocaleChange = (newLocale: string | number) => {
    setLocale(String(newLocale) as "en" | "cs");
};
</script>

<template>
    <Select
        :model-value="locale"
        :options="options"
        size="md"
        radius="full"
        align="right"
        aria-label="Select language"
        :class="$style.localeSelect"
        @update:model-value="onLocaleChange"
        theme="light"
    />
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.localeSelect {
    z-index: 10;
}
</style>

