<script setup lang="ts">
const { t } = useI18n();

const props = defineProps<{
    technology: string;
    to?: string;
}>();

const techTo = computed(() => {
    switch (props.technology) {
        case 'Next.js':
            return 'https://nextjs.org/';
        case 'React':
            return 'https://reactjs.org/';
        case 'Nuxt':
            return 'https://nuxt.com/';
        default:
            return null;
    }
});
</script>

<template>
    <a :href="to ?? techTo" target="_blank" rel="noopener noreferrer" :class="[$style.icon, (to ?? techTo) && $style.validLink]">
        {{technology}}
    </a>
</template>

<style module lang="scss">
.icon {
    color: var(--color-carbon-100);
    border: 1px solid var(--color-carbon-300);
    padding: 4px 8px;
    border-radius: 9999px;
    font-weight: 500;
    font-size: 14px;
    transition: all 300ms ease;
    cursor: default;
    user-select: none;
}

.validLink:hover {
    color: var(--color-primary);
    border: 1px solid var(--color-primary);
    cursor: pointer;
}
</style>