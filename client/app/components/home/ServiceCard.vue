<script setup lang="ts">
const { t } = useI18n();

const props = defineProps<{
    title: string;
    bulletPoints?: string[];
    image: string;
}>();
</script>

<template>
    <div :class="$style.card">
        <section>
            <div>
                <h2>{{title}}</h2>
                <p>
                    <slot />
                </p>
                <ul v-if="bulletPoints">
                    <li v-for="point in bulletPoints">{{point}}</li>
                </ul>
            </div>
        </section>
        <section>
            <img :src="image"  alt=""/>
        </section>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.card {
    border: 2px solid var(--color-primary);
    border-radius: 30px;
    padding: 64px;
    display: flex;
    height: 640px;

    section:first-child {
        width: 60%;
        margin: auto 0;

        div {
            width: 70%;
            font-size: 20px;
            
            h2 {
                font-size: 48px;
                line-height: 56px;
                margin-bottom: 16px;
            }
            
            p {
                opacity: .8;
            }
            
            ul {
                list-style: none;
                padding: 0;
                font-weight: 500;
                margin-top: 16px;
                
                li {
                    position: relative;
                    padding-left: 20px;
                    margin-top: 4px;
                    
                    &::before {
                        position: absolute;
                        content: '';
                        height: 6px;
                        width: 6px;
                        top: 50%;
                        left: 4px;
                        transform: translateY(-50%);
                        border-radius: 10000px;
                        background: var(--color-primary);
                        z-index: 0;
                    }
                }
            }
        }
    }

    section:last-child {
        margin-left: auto;
        padding: 64px;
        background: linear-gradient(to top, var(--color-lime-300) 0%, var(--color-primary) 100%);
        border-radius: 32px;
        
        img {
            box-shadow: 0 0 16px 0 rgba(0, 0, 0, 0.2);
            border-radius: 35px;
        }
    }
}
</style>