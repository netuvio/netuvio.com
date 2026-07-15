<script setup lang="ts">
import {useFetch, useRoute} from "#app";
import type {Project} from "~/lib/types";

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
    <section :class="[$style.section, 'theme-primary']" id="projects" ref="sectionRef">
        <div :class="['container', $style.container]">
            <h1>{{ project?.title }}</h1>
            <p>{{ project?.body }}</p>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    margin: 128px 0 150px;
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>