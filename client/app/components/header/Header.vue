<script setup lang="ts">
import LocaleSelector from "~/components/header/LocaleSelector.vue";

const { t } = useI18n();
</script>

<template>
    <header>
        <nav class="container">
            <NuxtLinkLocale to="/" :class="$style.logo"></NuxtLinkLocale>
            <div :class="$style.links">
                <ul>
                    <li><NuxtLinkLocale to="/">Home</NuxtLinkLocale></li>
                    <li><NuxtLinkLocale to="/">Home</NuxtLinkLocale></li>
                    <li><NuxtLinkLocale to="/">Home</NuxtLinkLocale></li>
                    <li><NuxtLinkLocale to="/">Home</NuxtLinkLocale></li>
                </ul>
            </div>
            <LocaleSelector/>
        </nav>
    </header>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

$headerHeight: 80px;

header {
    position: fixed;
    width: 100%;
    z-index: 1000;
    
    nav {
        height: $headerHeight;
        padding: 24px 0;
        display: flex;
        align-items: center;
        justify-content: space-between;
        position: relative;
                
        .logo {
            display: block;
            mask-image: url("/icons/logo-full.svg");
            mask-size: contain;
            mask-repeat: no-repeat;
            mask-position: center;
            background-color: var(--color-text-primary);
            width: 200px;
            height: 100%;
            z-index: 1;
        }
        
        // Has to be absolute to be in the center and not spaced by other elements
        .links {
            position: absolute;
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            
            ul {
                list-style: none;
                padding: 0;
                display: flex;
                align-items: center;
                font-weight: 600;
                
                li a {
                    padding: 8px 16px;
                    color: var(--color-text-primary);
                }
            }
        }
    }
}
</style>