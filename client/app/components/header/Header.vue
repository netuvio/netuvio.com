<script setup lang="ts">
import LocaleSelector from "~/components/header/LocaleSelector.vue";
import MobileMenu from "~/components/header/MobileMenu.vue";
import type {HeaderLink} from "~/lib/types";
import {ref} from "vue";

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
];

const hasScrolled = ref(false);

const handleScroll = () => {
    hasScrolled.value = window.scrollY > 50;
};

onMounted(() => {
    window.addEventListener("scroll", handleScroll);
});

onUnmounted(() => {
    window.removeEventListener("scroll", handleScroll);
});
</script>

<template>
    <header :class="{ [$style.scrolled]: hasScrolled }">
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
    transition: all 0.3s ease;
    padding: 0 12px;

    &.scrolled {
        margin: 16px 0;
        
        nav {
            background-color: var(--color-background-header);
            border-radius: 10000px;
            box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
            padding: 24px;
        }
    }
    
    nav {
        height: $headerHeight;
        padding-top: 24px;
        padding-bottom: 24px;
        display: flex;
        align-items: center;
        justify-content: space-between;
        position: relative;
        transition: all 0.3s ease;
                
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
            left: 50%;
            transform: translateX(-50%);
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
                    position: relative;
                    
                    &::before {
                        content: "";
                        position: absolute;
                        height: 3px;
                        bottom: 2px;
                        width: 0;
                        left: 50%;
                        transform: translateX(-50%);
                        background-color: var(--color-primary);
                        border-radius: 10000px;
                        transition: width 0.3s ease;
                    }
                    
                    &:hover::before {
                        width: calc(100% - 32px + 4px);
                    }
                }
            }
        }
        
        .mobileMenu {
            display: none;
            justify-content: center;
            align-items: center;
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
            display: flex;
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>