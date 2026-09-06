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
    font-size: 18px;
    line-height: 1.7;
    color: var(--color-carbon-50);
    
    h1:first-child {
        margin-top: 0;
        padding-top: 0;
    }

    h1, h2, h3, h4, h5, h6 {
        color: var(--color-text-primary);
        font-weight: 800;
        letter-spacing: -0.5px;
        margin-top: 40px;
        margin-bottom: 16px;
        position: relative;

        &:hover :global(.header-anchor) {
            opacity: 1;
        }
    }

    h1 {
        font-size: 48px;
    }

    h2 {
        font-size: 30px;
        padding-bottom: 8px;
        border-bottom: 1px solid var(--color-carbon-400);
    }
    
    h3 {
        font-size: 24px;
    }

    h4, h5, h6 {
        font-size: 20px;
    }

    p {
        margin-bottom: 20px;
    }

    ul, ol {
        margin-bottom: 24px;
        padding-left: 24px;

        li {
            margin-bottom: 8px;
            font-size: inherit;
        }
    }

    blockquote {
        border-left: 4px solid var(--color-primary);
        background-color: var(--color-carbon-700);
        padding: 16px 24px;
        border-radius: 0 16px 16px 0;
        margin: 24px 0;
        font-style: italic;
        color: var(--color-carbon-50);
        
        p {
            margin: 0;
        }
    }

    pre {
        background-color: var(--color-carbon-700);
        border: 2px solid var(--color-carbon-400);
        border-radius: 16px;
        padding: 20px;
        overflow-x: auto;
        margin: 24px 0;
    }

    code {
        font-family: monospace;
        font-size: 0.9em;
        background-color: var(--color-carbon-600);
        border-radius: 6px;
        padding: 2px 6px;
    }

    pre code {
        background-color: transparent;
        padding: 0;
    }

    hr {
        margin: 24px 0;
        border: none;
        border-top: 1px solid var(--color-carbon-400);
    }

    :global(.header-anchor) {
        text-decoration: none;
        color: var(--color-primary);
        margin-left: 8px;
        opacity: 0;
        transition: opacity 0.2s ease;
        
        &:hover {
            text-decoration: underline;
        }
    }

    table {
        width: 100%;
        border-collapse: separate;
        border-spacing: 0;
        margin: 32px 0;
        background-color: var(--color-carbon-700);
        border: 2px solid var(--color-carbon-400);
        border-radius: 16px;
        overflow: hidden;

        th, td {
            padding: 14px 18px;

            &:last-child {
                border-right: none;
            }
        }

        th {
            background-color: var(--color-carbon-800);
            color: var(--color-primary);
            font-weight: 800;
            text-transform: uppercase;
            font-size: 13px;
            letter-spacing: 1px;
            border-bottom: 2px solid var(--color-carbon-400);
        }

        td {
            color: var(--color-carbon-50);
        }

        tbody tr:last-child td {
            border-bottom: none;
        }

        tbody tr:hover {
            background-color: rgba(255, 255, 255, 0.03);
        }
    }
}
</style>