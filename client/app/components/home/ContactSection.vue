<script setup lang="ts">
import Button from "~/components/Button.vue";
import { parsePhoneNumberFromString } from 'libphonenumber-js';
import { motion, AnimatePresence } from "motion-v";

const { t } = useI18n();

const sectionRef = ref(null);

const form = reactive({
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    service: '',
    message: ''
});

const errors = reactive({
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    service: '',
    message: ''
});

const isSubmitting = ref(false);
const showSuccess = ref(false);

watch(() => form.phone, (newValue) => {
    form.phone = newValue.replace(/[^\d\s\-+()]/g, '');
});

const parsedPhoneNumber = computed(() => {
    return parsePhoneNumberFromString(form.phone);
});

const validate = () => {
    let isValid = true;
    Object.keys(errors).forEach(key => (errors[key as keyof typeof errors] = ''));

    if (!form.firstName.trim()) {
        errors.firstName = 'First name is required';
        isValid = false;
    }
    if (!form.lastName.trim()) {
        errors.lastName = 'Last name is required';
        isValid = false;
    }
    if (!form.email.trim()) {
        errors.email = 'Email is required';
        isValid = false;
    } else if (!/^\S+@\S+\.\S+$/.test(form.email)) {
        errors.email = 'Invalid email format';
        isValid = false;
    }
    if (!form.service) {
        errors.service = 'Please select a service';
        isValid = false;
    }
    if (!form.message.trim()) {
        errors.message = 'Message is required';
        isValid = false;
    }
    
    if (form.phone.trim() && !parsedPhoneNumber.value?.isPossible()) {
        errors.phone = 'Phone number is invalid';
        isValid = false;
    }

    return isValid;
};

const handleSubmit = async () => {
    if (!validate()) return;

    isSubmitting.value = true;
    
    try {
        // Simulate API call
        await new Promise(resolve => setTimeout(resolve, 1500));
        
        console.log('Form submitted:', form);
        showSuccess.value = true;
        
        // Reset form
        Object.assign(form, {
            firstName: '',
            lastName: '',
            email: '',
            phone: '',
            service: '',
            message: ''
        });
        
        setTimeout(() => (showSuccess.value = false), 5000);
    } catch (error) {
        console.error('Submission failed:', error);
    } finally {
        isSubmitting.value = false;
    }
};
</script>

<template>
    <section :class="[$style.section, 'theme-secondary']" id="contact" ref="sectionRef">
        <div :class="['container', $style.container]">
            <h1>Contact Us</h1>
            <transition name="fade" mode="out-in">
                <div v-if="showSuccess" :class="$style.successMessage">
                    <h2>Thank you!</h2>
                    <p>Your message has been sent successfully. We'll get back to you soon.</p>
                </div>
                <form v-else @submit.prevent="handleSubmit">
                    <section>
                        <label :class="{ [$style.hasError]: errors.firstName }">
                            <span>First Name *</span>
                            <input v-model="form.firstName" />
                            <span :class="$style.errorText">{{ errors.firstName }}</span>
                        </label>
                        <label :class="{ [$style.hasError]: errors.lastName }">
                            <span>Last Name *</span>
                            <input v-model="form.lastName" />
                            <span :class="$style.errorText">{{ errors.lastName }}</span>
                        </label>
                    </section>
                    <section>
                        <label :class="{ [$style.hasError]: errors.email }">
                            <span>E-Mail *</span>
                            <input v-model="form.email" type="email" />
                            <span :class="$style.errorText">{{ errors.email }}</span>
                        </label>
                        <label :class="{ [$style.hasError]: errors.phone }">
                            <span>Phone Number</span>
                            <div :class="$style.phone">
                                <div :class="$style.flagWrapper">
                                    <AnimatePresence mode="popLayout">
                                        <motion.img
                                            :key="parsedPhoneNumber?.country ?? 'UN'"
                                            :src="`https://flagcdn.com/24x18/${parsedPhoneNumber?.country?.toLowerCase() ?? 'un'}.png`"
                                            alt="Country Flag"
                                            :initial="{ opacity: 0, scale: 0.5 }"
                                            :animate="{ opacity: 1, scale: 1 }"
                                            :exit="{ opacity: 0, scale: 0.5 }"
                                            :transition="{ duration: 0.2, ease: 'easeOut' }"
                                        />
                                    </AnimatePresence>
                                </div>
                                <input v-model="form.phone" placeholder="+420 123 456 789" />
                            </div>
                            <span :class="$style.errorText">{{ errors.phone }}</span>
                        </label>
                    </section>
                    <section :class="[$style.services, { [$style.hasError]: errors.service }]">
                        <span>What do you need? *</span>
                        
                        <div>
                            <label>
                                <input type="radio" v-model="form.service" value="design" />
                                Design
                            </label>
                            <label>
                                <input type="radio" v-model="form.service" value="development" />
                                Development
                            </label>
                            <label>
                                <input type="radio" v-model="form.service" value="hosting" />
                                Hosting
                            </label>
                            <label>
                                <input type="radio" v-model="form.service" value="other" />
                                Something else
                            </label>
                        </div>
                        <span :class="$style.errorText">{{ errors.service }}</span>
                    </section>
                    <label :class="{ [$style.hasError]: errors.message }">
                        <span>Message *</span>
                        <textarea v-model="form.message" />
                        <span :class="$style.errorText">{{ errors.message }}</span>
                    </label>
                    <span>
                        <Button type="submit" size="lg" :disabled="isSubmitting">
                            {{ isSubmitting ? 'Sending...' : 'Send' }}
                        </Button>
                    </span>
                </form>
            </transition>
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

            .errorText {
                color: #ff3333;
                font-size: 0.85rem;
                margin-top: 4px;
                min-height: 1.2rem;
                font-weight: 500;
            }
        }
        
        .hasError {
            input, textarea, .phone {
                border-color: #ff3333 !important;
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

        input, textarea, .phone {
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
        
        .phone input {
            padding: 0;
            background-color: transparent;
            border: none;
            outline: none;
            border-radius: 0;
            width: 100%;
            
            &:focus {
                outline: none;
                border: none;
            }
        }
        
        .phone {
            display: flex;
            align-items: center;
            gap: 8px;
        }

        .flagWrapper {
            position: relative;
            width: 24px;
            height: 18px;
            flex-shrink: 0;
            display: flex;
            align-items: center;
            justify-content: center;

            img {
                width: 24px;
                height: 18px;
                display: block;
            }
        }

        textarea {
            min-height: 120px;
            resize: vertical;
        }
    }
    
    .successMessage {
        text-align: center;
        padding: 48px 0;
        
        h2 {
            color: var(--color-background-primary);
            margin-bottom: 16px;
        }
        
        p {
            font-size: 1.1rem;
            color: var(--color-carbon-600);
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