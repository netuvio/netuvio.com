<script setup lang="ts">
import { ref, onMounted, onUnmounted, watch } from "vue";

// cookie (persisted)
const enabledCookie = useCookie<boolean>('debug-enabled', {
    default: () => false,
});

// local state
const enabled = ref(enabledCookie.value);

// keep cookie in sync when toggled
watch(enabled, (val) => {
    enabledCookie.value = val;
});

const handler = (e: KeyboardEvent) => {
    if (e.key.toLowerCase() === 'k') {
        e.preventDefault();
        enabled.value = !enabled.value;
    }
};

onMounted(() => {
    window.addEventListener('keydown', handler);
});

onUnmounted(() => {
    window.removeEventListener('keydown', handler);
});
</script>

<template>
    <div :class="$style.debug" v-if="enabled">
        <ul :class="$style.breakpoints">
            <li :class="$style.mobile">mobile</li>
            <li :class="$style.tablet">tablet</li>
            <li :class="$style.laptop">laptop</li>
            <li :class="$style.desktop">desktop</li>
        </ul>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.debug {
    position: fixed;
    bottom: 0;
    left: 0;
    background-color: black;
    border: 1px solid dimgray;
    display: flex;
    flex-direction: column;
    z-index: 100000000;
    padding: 2px 4px;
    
    .breakpoints {
        list-style: none;
        padding: 0;
        
        // desktop
        @media screen and (min-width: $laptopBreakpoint+1px) {
            .desktop {
                display: block;
            }

            .mobile, .tablet, .laptop {
                display: none;
            }
        }
        
        // laptop
        @media screen and (max-width: $laptopBreakpoint) {
            .laptop {
                display: block;
            }

            .mobile, .tablet, .desktop {
                display: none;
            }
        }

        // tablet
        @media screen and (max-width: $tabletBreakpoint) {
            .tablet {
                display: block;
            }
            
            .mobile, .laptop, .desktop {
                display: none;
            }
        }

        // mobile
        @media screen and (max-width: $mobileBreakpoint) {
            .mobile {
                display: block;
            }

            .tablet, .laptop, .desktop {
                display: none;
            }
        }
    }
}
</style>