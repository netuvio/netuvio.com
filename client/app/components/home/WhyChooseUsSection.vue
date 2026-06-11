<script setup lang="ts">
import Button from "~/components/Button.vue";
import IonLayers from '~icons/ion/layers';

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
                    
                    <h2><i><IonLayers /></i>Flexible Services</h2>
                    <p>Every project is different. Whether you need design, development, hosting, or a complete solution, you can choose the services that fit your goals without paying for what you don't need.</p>
                </div>
                <div :class="$style.cta">
                    <section>
                        <h2>Ready to Bring Your Idea to Life?</h2>
                        <p>Whether you need a complete solution or help with a specific part of your project, we're here to help. Let's discuss your goals and find the right approach for your business.</p>
                    </section>
                    <section>
                        <NuxtLinkLocale to="/#contact">
                            <Button size="xl">Contact Us</Button>
                        </NuxtLinkLocale>
                    </section>
                </div>
                <div>
                    <h2>Modern Technology</h2>
                    <p>We build with modern frameworks, tools, and best practices to create fast, scalable, and maintainable solutions that are ready to grow with your business.</p>
                </div>
                <div>
                    <h2>Reliable Infrastructure</h2>
                    <p>As a small team, we work closely with every client. You'll communicate directly with the people building your project, ensuring clear communication, faster decisions, and a solution tailored to your needs.</p>
                </div>
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
        grid-template-columns: 1fr 1fr 450px;
        grid-template-rows: 1fr 1fr;
        gap: 24px;
        
        >div {
            border: 1px solid var(--color-carbon-50);
            width: 100%;
            height: 300px;
            border-radius: 30px;
            padding: 24px;
            
            h2 {
                font-size: 40px;
                display: flex;
                align-items: center;
                gap: 8px;
            }
        }
        
        .title {
            border: none;
            padding-left: 0;
        }
        
        .cta {
            background:
                radial-gradient(600px at top left, var(--color-primary) 0%, transparent 100%),
                radial-gradient(500px at 30% 0%, hsl(from var(--color-primary) calc(h - 20) s l) 0%, transparent 100%),
                radial-gradient(500px at bottom right, var(--color-primary) 0%, transparent 100%);
            grid-column: 3;
            grid-row: span 2;
            height: 100%;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }
        
        i {
            font-size: 36px;
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