<script setup lang="ts">
import ProjectComponent from "~/components/home/Project.vue";
import {useFetch} from "#app";
import type {Project} from "~/lib/types";
import {watch} from "vue";

const { t, locale } = useI18n();

const { data: projects, pending, error } = useFetch<Project[]>("/api/v1/projects", {
    query: { locale }
});

const featuredProjects = computed(() => {
    return projects.value?.filter(p => p.isFeatured) || [];
});
</script>

<template>
    <section :class="[$style.section, 'theme-primary']" id="projects" ref="sectionRef" v-if="projects && projects.length > 0">
        <div :class="['container', $style.container]">
            <h1>{{ t('projects.featuredProjects') }}</h1>
            <ul>
                <li v-for="project in featuredProjects" :key="project.title">
                    <ProjectComponent 
                        :title="project.title" 
                        :imageUrl="project.imageUrls[0]" 
                        :type="project.type"
                        :slug="project.slug"
                    >
                        {{project.description ?? project.body}}
                    </ProjectComponent>
                </li>
            </ul>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    min-height: 100vh;
    padding: clamp(90px, 10vw, 150px) 0;
    
    h1 {
        margin-bottom: 32px;
    }

    ul {
        list-style: none;
        padding: 0;
        display: flex;
        flex-direction: column;
        gap: 32px;
        
        li:nth-child(even) {
            >div {
                flex-direction: row-reverse;
            }
        }
    }
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
    .section ul {
        li:nth-child(even) {
            >div {
                flex-direction: column;
            }
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>
