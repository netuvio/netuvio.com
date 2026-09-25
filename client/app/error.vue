<script setup lang="ts">
import { computed } from "vue";
import { useError, useHead, clearError, useI18n, useState } from "#imports";
import { ErrorPage } from "@netuvio/ui/vue";
import "@netuvio/ui/styles.css";
import type { WebTheme } from "~/lib/types";

const { locale } = useI18n();
const nuxtError = useError();
const theme = useState<WebTheme>("theme", () => "light");

const code = computed<number>(() => Number(nuxtError.value?.statusCode ?? 500));
const lang = computed(() => (locale.value?.startsWith("cs") ? "cs" : "en"));

useHead(() => ({
    title: `Error ${code.value} • Netuvio`,
    htmlAttrs: { "data-theme": theme.value ?? undefined },
    link: [
        {
            rel: "icon",
            type: "image/x-icon",
            href: "/favicon.ico"
        }
    ]
}));

function onActionClick(event: MouseEvent) {
    const target = (event.target as HTMLElement)?.closest("a");
    if (!target) return;
    const href = target.getAttribute("href");
    if (href === "/" || href === "") {
        event.preventDefault();
        clearError({ redirect: "/" });
    }
}
</script>

<template>
    <div @click="onActionClick">
        <ErrorPage
            :code="code"
            :lang="lang"
            :theme="theme ?? 'auto'"
            homepage-url="/"
        />
    </div>
</template>