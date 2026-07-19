<script setup lang="ts">
import {createError, useFetch, useRoute} from "#app";
import type {Project} from "~/lib/types";
import Markdown from "~/components/Markdown.vue";
import Lightbox from "~/components/lightbox/Lightbox.vue";
import LightboxItem from "~/components/lightbox/LightboxItem.vue";
import SimpleIconsNextdotjs from '~icons/simple-icons/nextdotjs'
import TechnologyTag from "~/components/TechnologyTag.vue";

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
            <Lightbox>
                <main>
                    <h1>{{ project.title }}</h1>
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
        h1 {
            color: var(--color-primary);
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
            .technologies {
                h2 {
                    margin-bottom: 4px;
                }
                
                ul {
                    list-style: none;
                    padding: 0;
                    display: flex;
                    margin-bottom: 16px;
                    gap: 12px;
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