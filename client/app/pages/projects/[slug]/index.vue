<script setup lang="ts">
import {useFetch, useRoute} from "#app";
import type {Project} from "~/lib/types";
import {computed} from "vue";

const { t, locale } = useI18n();

const route = useRoute();

const { data: project, error } = await useFetch<Project>(`/api/v1/projects/${route.params.slug}`, {
    query: { locale }
});

if (error.value || !project.value) {
    throw createError({
        statusCode: 404,
        statusMessage: 'Project Not Found',
        fatal: true
    });
}
</script>

<template>
    <div :class="$style.test">
        <h1>{{ project?.title }}</h1>
        <p>{{ project?.body }}</p>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.test {
    margin: 128px 64px 256px;
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>