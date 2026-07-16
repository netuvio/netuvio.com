<script setup lang="ts">
import {createError, useFetch, useRoute} from "#app";
import type {Project} from "~/lib/types";
import Markdown from "~/components/Markdown.vue";

const { t, locale } = useI18n();

const route = useRoute();

const { data: project, error } = await useFetch<Project>(`/api/v1/projects/${route.params.slug}`, {
    query: { locale }
});

if (error.value || !project.value) {
    throw createError({
        status: 404,
        statusText: 'Project Not Found',
        fatal: true
    });
}
</script>

<template>
    <Head>
        <Title>{{ project?.title }} • Netuvio</Title>
        <meta name="description" :content="project?.description ?? project?.body" />
    </Head>
    <section :class="[$style.section, 'theme-primary']" id="projects" ref="sectionRef">
        <div :class="['container', $style.container]" v-if="project">
            <main>
                <h1>{{ project.title }}</h1>
                <p>{{ project.description }}</p>
                <NuxtImg
                    v-if="project.imageUrls[0]"
                    :src="project.imageUrls[0]"
                    :alt="project.title"
                />
            </main>
            
            <section :class="$style.body">
                <Markdown :markdown="project.body" />
            </section>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    margin: 128px 0 150px;
    
    main {
        img {
            border-radius: 30px;
            width: 100%;
            height: 900px;
            object-fit: cover;
            margin-top: 16px;
        }
    }
    
    .body {
        margin-top: 32px;
    }
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>