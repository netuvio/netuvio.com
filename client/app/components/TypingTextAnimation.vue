<script setup lang="ts">
/**
 * Animates each word/letter of the text with a staggered animation using motion.
 */

import { motion } from "motion-v";

const props = withDefaults(defineProps<{
    text: string;
    
    type?: 'word' | 'letter';
    
    /** Delay of each child consecutively in seconds */
    staggerDuration?: number;
    
    /** Delay of the whole animation in seconds */
    delay?: number;
    
    /** Animation of children in motion values */
    itemAnimation?: any;
    
    /** Include raw hidden text for SEO and accessibility */
    seoFriendly?: boolean;
}>(), {
    type: 'word',
    staggerDuration: 0.07,
    delay: 0,
    itemAnimation: {
        hidden: { opacity: 0 },
        show: { opacity: 1 }
    },
    seoFriendly: true,
});

const textArray = computed(() => {
    return props.type === 'word'
        ? props.text.split(' ')
        : props.text.split('');
});

const container = {
    hidden: {},
    show: {
        transition: {
            staggerChildren: props.staggerDuration,
            delayChildren: props.delay,
        }
    }
};
</script>

<template>
    <span class="sr-only" v-if="seoFriendly">Modern apps, powerful hosting, zero hassle</span>
    
    <motion.span
        :variants="container"
        initial="hidden"
        animate="show"
        :aria-hidden="seoFriendly"
    >
        <motion.span 
            v-for="(x, index) in textArray"
            :key="index"
            :variants="props.itemAnimation"
            :aria-hidden="seoFriendly"
        >{{ x + (type === 'word' ? ' ' : '') }}</motion.span>
    </motion.span>
</template>