<script setup lang="ts">
const { t } = useI18n();

const props = defineProps<{
    technology: string;
    to?: string;
}>();

const URL_MAP: Record<string, string> = {
    'Next.js': 'https://nextjs.org/',
    'React': 'https://reactjs.org/',
    'Nuxt': 'https://nuxt.com/',
    'ASP.NET': 'https://dotnet.microsoft.com/apps/aspnet',
    'PHP': 'https://www.php.net/',
    'Wordpress': 'https://wordpress.org/',
    'Affinity': 'https://www.affinity.studio/',
};

const techTo = computed(() => URL_MAP[props.technology]);
</script>

<template>
    <a :href="to ?? techTo" target="_blank" rel="noopener noreferrer" :class="[$style.icon, (to ?? techTo) && $style.validLink]">
        {{technology}}
    </a>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.icon {
    color: var(--color-carbon-100);
    border: 1px solid var(--color-carbon-300);
    padding: 4px 8px;
    border-radius: 9999px;
    font-weight: 500;
    font-size: var(--font-size-sm);
    transition: all 300ms ease;
    cursor: default;
    user-select: none;
}

.validLink:hover {
    color: var(--color-primary);
    border: 1px solid var(--color-primary);
    cursor: pointer;
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
    .icon {
        font-size: var(--font-size-xs);
        padding: 3px 8px;
    }
}
</style>
