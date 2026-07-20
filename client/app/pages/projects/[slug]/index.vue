<script setup lang="ts">
import {createError, useFetch, useRoute} from "#app";
import type {Project} from "~/lib/types";
import Markdown from "~/components/Markdown.vue";
import Lightbox from "~/components/lightbox/Lightbox.vue";
import LightboxItem from "~/components/lightbox/LightboxItem.vue";
import TechnologyTag from "~/components/TechnologyTag.vue";
import {computed} from "vue";
import TablerWorld from '~icons/tabler/world';
import TablerGitMerge from '~icons/tabler/git-merge';

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

function formatDate(date: string): string {
    return new Intl.DateTimeFormat(undefined, {
        year: 'numeric',
        month: 'short',
    }).format(new Date(date));
}

const dateRange = computed(() => {
    const startedAt = project.value?.startedAt ? formatDate(project.value?.startedAt) : null;
    const finishedAt = project.value?.finishedAt ? formatDate(project.value?.finishedAt) : null;
    
    if (startedAt && finishedAt)
        return `${startedAt} – ${finishedAt}`;
    
    if (!startedAt && finishedAt)
        return `${finishedAt}`;
    
    if (!finishedAt && startedAt)
        return `${startedAt} – Ongoing`;
    
    return "";
})
</script>

<template>
    <Head>
        <Title>{{ project?.title }} • Netuvio</Title>
        <meta name="description" :content="project?.description ?? project?.body" />
    </Head>
    <section :class="[$style.section, 'theme-primary']" id="projects" ref="sectionRef">
        <div :class="['container', $style.container]" v-if="project">
            <Lightbox>
                <main>
                    <div :class="$style.title">
                        <h1>{{ project.title }}</h1> <span>{{ dateRange }}</span>
                    </div>
                    <p>{{ project.description }}</p>
                    <div :class="$style.info">
                        <div :class="$style.technologies">
                            <h2>Technologies</h2>
                            <ul>
                                <li v-for="technology in project.technologies" :key="technology">
                                    <TechnologyTag :technology="technology" />
                                </li>
                            </ul>
                        </div>
                        <ul :class="$style.links" v-if="project.websiteUrl || project.sourceCodeUrl">
                            <li v-if="project.websiteUrl">
                                <a :href="project.websiteUrl" target="_blank" rel="noopener noreferrer">
                                    <TablerWorld /> Visit Website
                                </a>
                            </li>
                            <li v-if="project.sourceCodeUrl">
                                <a :href="project.sourceCodeUrl" target="_blank" rel="noopener noreferrer">
                                    <TablerGitMerge /> Source Code
                                </a>
                            </li>
                        </ul>
                    </div>
                    <LightboxItem
                        :key="project.imageUrls[0]"
                        :src="project.imageUrls[0]"
                        :width="800"
                        :class-name="$style.lightboxItemWrapper"
                        :image-class-name="$style.lightboxItemImage"
                    />
                </main>
                
                <section :class="$style.body">
                    <Markdown :markdown="project.body" />
                </section>
                
                <section :class="$style.lightbox">
                    <h2>Gallery</h2>
                        <div :class="$style.lightboxItems">
                            <LightboxItem 
                                v-for="url in project.imageUrls" 
                                :key="url"
                                :src="url" 
                                :width="800" 
                                :class-name="$style.lightboxItemWrapper"
                                :image-class-name="$style.lightboxItemImage"
                            />
                        </div>
                </section>
            </Lightbox>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    margin: 128px 0 150px;
    
    main {
        .title {
            display: flex;
            justify-content: space-between;
            align-items: center;
            
            h1 {
                color: var(--color-primary);
            }
            
            span {
                font-size: 18px;
                color: var(--color-carbon-100);
            }
        }
        
        p {
            margin-bottom: 16px;
        }
        
        img {
            border-radius: 30px;
            width: 100%;
            height: 900px;
            object-fit: cover;
        }
        
        .info {
            display: flex;
            justify-content: space-between;
            align-items: end;
            margin-bottom: 16px;
            
            .technologies {
                h2 {
                    margin-bottom: 4px;
                }
                
                ul {
                    list-style: none;
                    padding: 0;
                    display: flex;
                    gap: 12px;
                }
            }
            
            .links {
                list-style: none;
                padding: 0;
                display: flex;
                
                color: var(--color-carbon-100);
                border: 1px solid var(--color-carbon-300);
                border-radius: 9999px;
                font-weight: 500;
                overflow: hidden;
                
                li {
                    a {
                        color: var(--color-carbon-100);
                        padding: 10px 14px;
                        border-right: 1px solid var(--color-carbon-300);
                        display: flex;
                        align-items: center;
                        gap: 4px;

                        &:hover {
                            color: var(--color-primary);
                        }
                    }
                    
                    &:last-child a {
                        border-right: none;
                    }
                }
            }
        }
    }
    
    .body {
        margin-top: 32px;
    }
    
    .lightbox {
        margin-top: 32px;
        
        h2 {
            font-size: 48px;
            padding-bottom: 8px;
        }
        
        .lightboxItems {
            display: grid;
            grid-template-columns: 1fr 1fr 1fr;
            gap: 16px;
        }

        .lightboxItemWrapper {
            aspect-ratio: 1 / 1;
            width: 100%;
            border-radius: 16px;
            overflow: hidden;
        }

        .lightboxItemImage {
            width: 100%;
            height: 100%;
            object-fit: cover;
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