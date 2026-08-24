<script setup lang="ts">
const props = defineProps<{
    title: string;
    image: string;
}>();
</script>

<template>
    <div :class="$style.card">
        <section :class="$style.image">
            <img :src="image" alt="" />
        </section>
        <section :class="$style.content">
            <div>
                <h2>{{title}}</h2>
                <div :class="$style.text">
                    <p><slot /></p>
                    <img src="/images/arrow.svg" alt="Learn more" />
                </div>
            </div>
        </section>
        <div :class="$style.decorations">
            <div :class="$style.dots">
                <img src="/images/dots.svg" alt="" />
                <img src="/images/dots.svg" alt="" />
            </div>
            <div :class="$style.cylinders">
                <div></div>
                <div></div>
            </div>
        </div>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.card {
    width: 350px;
    height: 500px;
    border: 4px solid var(--color-background-primary);
    border-radius: 24px;
    padding: 32px;
    position: relative;
    transition: 200ms all ease-in-out;
    
    .image {
        height: 40%;
    }

    .content {
        height: 60%;
        
        >div {
            height: 100%;
            display: flex;
            flex-direction: column;
        }
        
        h2 {
            font-size: 32px;
            line-height: 1.2;
        }
        
        .text {
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            flex-grow: 1;
            
            p {
                margin-top: 8px;
                font-size: 18px;
            }
            
            img {
                width: 24px;
                height: 24px;
                padding: 4px;
                background-color: var(--color-primary);
            }
        }
    }
    
    .decorations {
        position: absolute;
        inset: 0;
        pointer-events: none;
        overflow: hidden;
        
        .dots {
            position: absolute;
            inset: 0;
            
            img {
                position: absolute;
                object-fit: contain;
                
                &:nth-child(1) {
                    top: 0;
                    left: 20%;
                    width: 160px;
                    height: 160px;
                }
                
                &:nth-child(2) {
                    bottom: -10%;
                    right: 0;
                    width: 128px;
                    height: 128px;
                }
            }
        }
        
        .cylinders {
            position: absolute;
            inset: 0;
            
            div {
                position: absolute;
                border-radius: 9999px;
                background-color: var(--color-background-primary);
                transition: 200ms all ease-in-out;

                &:nth-child(1) {
                    width: 20px;
                    height: 128px;
                    top: 20%;
                    right: 0;
                    transform: translateX(50%);
                }

                &:nth-child(2) {
                    width: 128px;
                    height: 20px;
                    bottom: 0;
                    right: 15%;
                    transform: translateY(50%);
                }
            }
        }
    }
}

.card:hover {
    border-color: var(--color-primary);
    
    .decorations {
        .cylinders {
            div {
                background-color: var(--color-primary);
            }
        }
    }
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>