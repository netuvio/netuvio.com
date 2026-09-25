<script setup lang="ts">
const { t } = useI18n();
</script>

<template>
    <Head>
        <Title>{{ t('footer.termsOfService') }} • Netuvio</Title>
    </Head>

    <main :class="[$style.page, 'theme-primary']">
        <div :class="['container', $style.container]">
            <h1>{{ t('footer.termsOfService') }}</h1>
            <p>{{ t('footer.loremIpsum') }}</p>
        </div>
    </main>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.page {
    min-height: 80vh;
    padding: clamp(140px, 15vw, 200px) 0 80px;

    .container {
        display: flex;
        flex-direction: column;
        gap: 24px;
        max-width: 800px;

        h1 {
            font-size: clamp(2rem, 4vw, 3rem);
            margin-bottom: 8px;
        }

        p {
            color: var(--color-text-secondary);
            line-height: 1.6;
        }
    }
}
</style>
