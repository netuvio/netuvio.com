<script setup lang="ts">
import Button from "~/components/Button.vue";

const { t } = useI18n();

const sectionRef = ref(null);
</script>

<template>
    <section :class="[$style.section, 'theme-secondary']" id="contact" ref="sectionRef">
        <div :class="['container', $style.container]">
            <h1>Contact Us</h1>
            <form>
                <section>
                    <label>
                        <span>First Name *</span>
                        <input name="firstName" required />
                    </label>
                    <label>
                        <span>Last Name *</span>
                        <input name="lastName" required />
                    </label>
                </section>
                <section>
                    <label>
                        <span>E-Mail *</span>
                        <input name="email" required />
                    </label>
                    <label>
                        <span>Phone Number</span>
                        <input name="phoneNumber"/>
                    </label>
                </section>
                <section :class="$style.services">
                    <span>What do you need? *</span>
                    
                    <div>
                        <label>
                            <input type="radio" name="service" value="design" />
                            Design
                        </label>
                        <label>
                            <input type="radio" name="service" value="development" />
                            Development
                        </label>
                        <label>
                            <input type="radio" name="service" value="hosting" />
                            Hosting
                        </label>
                        <label>
                            <input type="radio" name="service" value="other" required />
                            Something else
                        </label>
                    </div>
                </section>
                <label>
                    <span>Message *</span>
                    <textarea name="message" required />
                </label>
                <span><Button type="submit" size="lg">Send</Button></span>
            </form>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding: 150px 0;
    
    h1 {
        margin-bottom: 32px;
    }
    
    form {
        display: flex;
        flex-direction: column;
        gap: 24px;
        
        section {
            display: flex;
            justify-content: space-between;
            gap: 20px;
        }

        label, .services {
            display: flex;
            flex-direction: column;
            width: 100%;
            gap: 8px;
            font-weight: bold;
            
            >span {
                margin-left: 12px;
            }
        }
        
        .services {
            >div {
                display: flex;
                gap: 8px;
                
                label {
                    display: flex;
                    flex-direction: row;
                    gap: 8px;
                    align-items: center;
                    width: auto;
                    cursor: pointer;
                    padding: 8px 16px;
                    border-radius: 30px;
                    background-color: hsl(0, 0%, 97%);
                    transition: all 0.18s ease;
                    font-weight: 500;
                    margin: 0;
                    user-select: none;
                    
                    &:hover {
                        background-color: hsl(0, 0%, 90%);
                    }

                    &:has(input:checked) {
                        background-color: var(--color-background-primary);
                        color: white;
                    }
                    
                    input {
                        display: none;
                    }
                }
            }
        }

        input, textarea {
            padding: 12px 20px;
            background-color: hsl(0, 0%, 97%);
            border: 1px solid var(--color-carbon-50);
            color: inherit;
            border-radius: 30px;
            font-weight: normal;
            transition: all 0.18s ease;

            &:focus {
                outline: none;
                border: 1px solid var(--color-carbon-400);
            }
        }

        textarea {
            min-height: 120px;
            resize: vertical;
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