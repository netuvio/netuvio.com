<script setup lang="ts">
import MarkdownIt from "markdown-it";
import markdownItAnchor from "markdown-it-anchor";
import DOMPurify from "isomorphic-dompurify";

const props = defineProps<{
    markdown: string;
}>();

const md = new MarkdownIt({
    html: true,
    breaks: true
}).use(markdownItAnchor, {
    permalink: markdownItAnchor.permalink.linkInsideHeader({
        symbol: '#',
        placement: 'after',
        class: 'header-anchor',
        ariaHidden: true
    })
});

const rawMarkdown = md.render(props.markdown);
const cleanMarkdown = DOMPurify.sanitize(rawMarkdown, { ADD_ATTR: ['id', 'target'] });
</script>

<template>
    <article v-html="cleanMarkdown" :class="$style.markdown"></article>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.markdown {
    h2 {
        font-size: 32px;
    }
    
    h3, h4, h5, h6 {
        font-size: 26px;
    }
    
    h1, h2, h3, h4, h5, h6 {
        padding-top: 12px;
        position: relative;

        &:hover :global(.header-anchor) {
            opacity: 1;
        }
    }

    :global(.header-anchor) {
        text-decoration: none;
        color: hsl(81, 84%, 56%);
        margin-left: 8px;
        opacity: 0;
        transition: opacity 0.2s ease;
        
        &:hover {
            text-decoration: underline;
        }
    }

    hr {
        margin: 16px 0;
    }
    
    pre, code {
        background-color: var(--color-carbon-600);
        padding: 16px;
        border-radius: 8px;
    }
    
    code {
        padding: 4px 8px;
    }
    
    li {
        font-size: 20px;
    }

    table {
        width: 100%;
        border-collapse: collapse;
        font-size: 0.95rem;
        text-align: left;

        thead {
            th {
                padding: 14px 16px;
                font-weight: 600;
                text-transform: uppercase;
                font-size: 0.8rem;
                letter-spacing: 0.05em;
                border-bottom: 1px solid hsla(220, 13%, 91%, 0.5);
                color: hsl(81 84% 56%);
            }
        }

        tbody {
            tr {
                border-bottom: 1px solid hsla(220, 13%, 91%, 0.5);
                transition: background-color 0.2s ease;

                &:last-child {
                    border-bottom: none;
                }
            }

            td {
                padding: 14px 16px;
                line-height: 1.5;
            }
        }
    }
}
</style>