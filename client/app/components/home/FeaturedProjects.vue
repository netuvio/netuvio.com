<script setup lang="ts">
import Project from "~/components/home/Project.vue";
import projects from "../../data/projects";

const { t } = useI18n();
</script>

<template>
    <section :class="[$style.section, 'theme-primary']" id="projects" ref="sectionRef">
        <div :class="['container', $style.container]">
            <h1>{{ t('projects.featuredProjects') }}</h1>
            <ul>
                <li v-for="project in projects.filter(p => p.isFeatured)" :key="project.name">
                    <Project :title="project.name" :imageUrl="project.imageUrl" :type="project.type" >
                        {{project.description}}
                    </Project>
                </li>
            </ul>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    min-height: 100vh;
    padding: 150px 0;
    
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
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>