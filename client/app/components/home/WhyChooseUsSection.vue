<script setup lang="ts">
const { t } = useI18n();
</script>

<template>
    <section :class="[$style.section, 'theme-secondary']" id="why-choose-us" ref="sectionRef">
        <div :class="$style.transition"></div>
        <div :class="['container', $style.container]">
            <section :class="$style.grid">
                <div :class="$style.title">
                    <h1>Why Choose Us</h1>
                </div>
                <div>
                    <h2>Lorem ipsum</h2>
                    <p>Lorem ipsum dolor sit amet.</p>
                </div>
                <div :class="$style.cta">dfgh</div>
                <div></div>
                <div></div>
            </section>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding: 150px 0;
    
    .grid {
        display: grid;
        grid-template-columns: 1fr 1fr 400px;
        grid-template-rows: 1fr 1fr;
        gap: 24px;
        
        >div {
            border: 1px solid var(--color-carbon-50);
            width: 100%;
            height: 300px;
            border-radius: 30px;
            padding: 24px;
            
            h2 {
                font-size: 48px;
            }
        }
        
        .title {
            border: none;
        }
        
        .cta {
            background-color: var(--color-primary);
            grid-column: 3;
            grid-row: span 2;
            height: 100%;
        }
    }
}

.transition {
    width: 100%;
    height: 50px;
    position: absolute;
    top: -25px;
    background: repeating-linear-gradient(-45deg, var(--color-background-secondary), var(--color-background-secondary) 30px, transparent 0px, transparent 60px);
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>