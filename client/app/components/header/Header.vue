<script setup lang="ts">
import LocaleSelector from "~/components/header/LocaleSelector.vue";
import MobileMenu from "~/components/header/MobileMenu.vue";
import type {HeaderLink} from "~/lib/types";

const { t } = useI18n();

const links: HeaderLink[] = [
    {
        name: "Home",
        to: "/"
    },
    {
        name: "Home",
        to: "/"
    },
    {
        name: "Home",
        to: "/"
    },
    {
        name: "Home",
        to: "/"
    }
]
</script>

<template>
    <header>
        <nav class="container">
            <NuxtLinkLocale to="/" :class="$style.logo"></NuxtLinkLocale>
            <div :class="$style.links">
                <ul>
                    <li v-for="link in links" :key="link.to">
                        <NuxtLinkLocale :to="link.to">{{ link.name }}</NuxtLinkLocale>
                    </li>
                </ul>
            </div>
            <LocaleSelector :class="$style.locales"/>
            <MobileMenu :class="$style.mobileMenu" :links="links" />
        </nav>
    </header>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

$headerHeight: 80px;

header {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    z-index: 1000;
    
    nav {
        height: $headerHeight;
        padding-top: 24px;
        padding-bottom: 24px;
        display: flex;
        align-items: center;
        justify-content: space-between;
        position: relative;
                
        .logo {
            display: block;
            mask-image: url("/icons/logo-full.svg");
            mask-size: contain;
            mask-repeat: no-repeat;
            mask-position: left center;
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
        
        .mobileMenu {
            display: none;
        }
    }
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
    header nav {
        .links, .locales {
            display: none;
        }
        
        .mobileMenu {
            display: block;
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>