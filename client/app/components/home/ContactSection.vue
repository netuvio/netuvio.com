<script setup lang="ts">
import { Button, Input, Textarea } from "@netuvio/ui/vue";
import { parsePhoneNumberFromString } from 'libphonenumber-js';
import { motion, AnimatePresence } from "motion-v";

const { t } = useI18n();

const phoneInputRef = ref<any>(null);

const MAX_LENGTHS = {
    firstName: 50,
    lastName: 50,
    email: 100,
    phone: 25,
    message: 2000
};

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

watch(() => form.phone, (newValue) => {
    form.phone = newValue.replace(/[^\d\s\-+()]/g, '');
});

const parsedPhoneNumber = computed(() => {
    return parsePhoneNumberFromString(form.phone, 'CZ');
});

const validate = () => {
    let isValid = true;
    Object.keys(errors).forEach(key => (errors[key as keyof typeof errors] = ''));

    if (!form.firstName.trim()) {
        errors.firstName = t('contact.validation.firstNameRequired');
        isValid = false;
    } else if (form.firstName.length > MAX_LENGTHS.firstName) {
        errors.firstName = t('contact.validation.maxLength', { max: MAX_LENGTHS.firstName });
        isValid = false;
    }

    if (!form.lastName.trim()) {
        errors.lastName = t('contact.validation.lastNameRequired');
        isValid = false;
    } else if (form.lastName.length > MAX_LENGTHS.lastName) {
        errors.lastName = t('contact.validation.maxLength', { max: MAX_LENGTHS.lastName });
        isValid = false;
    }

    if (!form.email.trim()) {
        errors.email = t('contact.validation.emailRequired');
        isValid = false;
    } else if (!/^\S+@\S+\.\S+$/.test(form.email)) {
        errors.email = t('contact.validation.emailInvalid');
        isValid = false;
    } else if (form.email.length > MAX_LENGTHS.email) {
        errors.email = t('contact.validation.maxLength', { max: MAX_LENGTHS.email });
        isValid = false;
    }

    if (!form.service) {
        errors.service = t('contact.validation.serviceRequired');
        isValid = false;
    }

    if (!form.message.trim()) {
        errors.message = t('contact.validation.messageRequired');
        isValid = false;
    } else if (form.message.length > MAX_LENGTHS.message) {
        errors.message = t('contact.validation.maxLength', { max: MAX_LENGTHS.message });
        isValid = false;
    }
    
    if (form.phone.trim() && !parsedPhoneNumber.value?.isValid()) {
        errors.phone = t('contact.validation.phoneInvalid');
        isValid = false;
    } else if (form.phone.length > MAX_LENGTHS.phone) {
        errors.phone = t('contact.validation.maxLength', { max: MAX_LENGTHS.phone });
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
        
        // Reset form
        Object.assign(form, {
            firstName: '',
            lastName: '',
            email: '',
            phone: '',
            service: '',
            message: ''
        });
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
            <motion.h1 
                :initial="{ opacity: 0, y: 30 }"
                :whileInView="{ opacity: 1, y: 0 }"
                :inViewOptions="{ once: true }"
                :transition="{ duration: 0.6 }"
            >
                {{ t('contact.title') }}
            </motion.h1>
            <form @submit.prevent="handleSubmit">
                <motion.section
                    :initial="{ opacity: 0, y: 20 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.5, delay: 0.1 }"
                >
                    <Input
                        v-model="form.firstName"
                        name="firstName"
                        autocomplete="given-name"
                        :label="`${t('contact.firstName')} *`"
                        :error="errors.firstName"
                        :maxlength="MAX_LENGTHS.firstName"
                        size="lg"
                        block
                    />
                    <Input
                        v-model="form.lastName"
                        name="lastName"
                        autocomplete="family-name"
                        :label="`${t('contact.lastName')} *`"
                        :error="errors.lastName"
                        :maxlength="MAX_LENGTHS.lastName"
                        size="lg"
                        block
                    />
                </motion.section>
                <motion.section
                    :initial="{ opacity: 0, y: 20 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.5, delay: 0.2 }"
                >
                    <Input
                        v-model="form.email"
                        type="email"
                        name="email"
                        autocomplete="email"
                        :label="`${t('contact.email')} *`"
                        :error="errors.email"
                        :maxlength="MAX_LENGTHS.email"
                        size="lg"
                        block
                    />
                    <Input
                        ref="phoneInputRef"
                        v-model="form.phone"
                        type="tel"
                        name="phone"
                        autocomplete="tel"
                        :label="t('contact.phoneNumber')"
                        :placeholder="t('contact.phonePlaceholder')"
                        :error="errors.phone"
                        :maxlength="MAX_LENGTHS.phone"
                        size="lg"
                        block
                        @click:prefix="phoneInputRef?.focus?.()"
                    >
                        <template #prefix>
                            <div :class="$style.flagWrapper">
                                <AnimatePresence mode="popLayout">
                                    <motion.img
                                        :key="parsedPhoneNumber?.country ?? 'UN'"
                                        :src="`https://flagcdn.com/24x18/${parsedPhoneNumber?.country?.toLowerCase() ?? 'un'}.png`"
                                        :title="parsedPhoneNumber?.country ?? 'UN'"
                                        :alt="t('contact.countryFlag')"
                                        :initial="{ opacity: 0, scale: 0.5 }"
                                        :animate="{ opacity: 1, scale: 1 }"
                                        :exit="{ opacity: 0, scale: 0.5 }"
                                        :transition="{ duration: 0.2, ease: 'easeOut' }"
                                    />
                                </AnimatePresence>
                            </div>
                        </template>
                    </Input>
                </motion.section>
                <motion.section 
                    :class="[$style.services, { [$style.hasError]: errors.service }]"
                    :initial="{ opacity: 0, y: 20 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.5, delay: 0.3 }"
                >
                    <span>{{ t('contact.whatDoYouNeed') }} *</span>
                    
                    <div>
                        <label>
                            <input type="radio" v-model="form.service" value="design" />
                            {{ t('contact.services.design') }}
                        </label>
                        <label>
                            <input type="radio" v-model="form.service" value="development" />
                            {{ t('contact.services.development') }}
                        </label>
                        <label>
                            <input type="radio" v-model="form.service" value="hosting" />
                            {{ t('contact.services.hosting') }}
                        </label>
                        <label>
                            <input type="radio" v-model="form.service" value="other" />
                            {{ t('contact.services.other') }}
                        </label>
                    </div>
                    <span :class="$style.errorText">{{ errors.service }}</span>
                </motion.section>
                <motion.label 
                    :class="[$style.textareaField, { [$style.hasError]: errors.message }]"
                    :initial="{ opacity: 0, y: 20 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.5, delay: 0.4 }"
                >
                    <div :class="$style.labelHeader">
                        <span>{{ t('contact.message') }} *</span>
                        <span :class="$style.charCount">{{ form.message.length }} / {{ MAX_LENGTHS.message }}</span>
                    </div>
                    <Textarea 
                        v-model="form.message" 
                        :maxlength="MAX_LENGTHS.message"
                        size="lg"
                        radius="lg"
                    />
                    <span :class="$style.errorText">{{ errors.message }}</span>
                </motion.label>
                <motion.span
                    :initial="{ opacity: 0, y: 20 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.5, delay: 0.5 }"
                >
                    <Button type="submit" size="lg" :disabled="isSubmitting">
                        {{ isSubmitting ? t('contact.sending') : t('contact.send') }}
                        <DrawnArrow />
                    </Button>
                </motion.span>
            </form>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    padding: clamp(90px, 10vw, 150px) 0;
    
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

            > * {
                flex: 1;
                min-width: 0;
            }
        }

        .textareaField, .services {
            display: flex;
            flex-direction: column;
            width: 100%;
            gap: 8px;
            font-weight: bold;
            
            >span {
                margin-left: 12px;
            }

            .labelHeader {
                display: flex;
                justify-content: space-between;
                align-items: center;
                padding-right: 12px;

                span {
                    margin-left: 12px;
                }

                .charCount {
                    font-size: 0.8rem;
                    font-weight: 500;
                    color: var(--color-carbon-400);
                    margin: 0;
                }
            }

            .errorText {
                color: #ff3333;
                font-size: 0.85rem;
                margin-top: 4px;
                min-height: 1.2rem;
                font-weight: 500;
            }
        }
        
        .services {
            >div {
                display: flex;
                flex-wrap: wrap;
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

        textarea {
            min-height: 240px !important;
        }

        .flagWrapper {
            position: relative;
            width: 24px;
            height: 18px;
            flex-shrink: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;

            img {
                width: 24px;
                height: 18px;
                display: block;
                border-radius: 2px;
            }
        }
    }
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
    .section form {
        section {
            flex-direction: column;
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .section form {
        gap: 18px;

        .services > div label {
            width: 100%;
            min-height: 44px;
        }

        textarea {
            min-height: 44px;
        }
    }
}
</style>
