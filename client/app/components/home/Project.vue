<script setup lang="ts">
defineProps<{
    title: string;
    imageUrl: string;    
}>();

const { t } = useI18n();
</script>

<template>
    <div :class="$style.project">
        <div>
            <h2>{{title}}</h2>
            <p><slot/></p>
        </div>
        <div>
            <img :src="imageUrl"  alt=""/>
        </div>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.project {
    display: flex;
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>