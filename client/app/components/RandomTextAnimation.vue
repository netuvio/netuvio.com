<script setup lang="ts">
import {onMounted, ref} from "vue";
import { motion, AnimatePresence } from "motion-v";

const props = withDefaults(defineProps<{
    text: string,
    interval?: number,
    consecutiveCount?: number,
    delay?: number,
}>(), {
    interval: 50,
    consecutiveCount: 1,
    delay: 0,
})

const chars = '!@#$%^&*()=_+';

function getInitialScramble(char: string, index: number) {
    if (!/[a-zA-Z]/.test(char)) return char;
    return chars[Math.floor(Math.random() * chars.length)]!;
}

interface LetterWithCount {
    letter: string;
    count: number;
}

const randomizedText = ref<LetterWithCount[]>(
    props.text.split('').map((char, index) => ({
        letter: getInitialScramble(char, index),
        count: 0
    }))
);

function getRandomNonRandomizedIndex() {
    const unsolvedIndices = [];

    // Find all indices that haven't been decoded yet
    for (let i = 0; i < props.text.length; i++) {
        if (randomizedText.value[i]?.letter !== props.text[i]) {
            unsolvedIndices.push(i);
        }
    }

    // If empty everything is decoded
    if (unsolvedIndices.length === 0) return -1;

    // Pick a random unsolved index
    const randomPick = Math.floor(Math.random() * unsolvedIndices.length);
    return unsolvedIndices[randomPick];
}

function convertLetter(index: number, chance: number) {
    const targetLetter = props.text[index];

    if (targetLetter === randomizedText.value[index]?.letter) return targetLetter;

    if (randomizedText.value[index]?.count! < 5 && Math.floor(Math.random() * 100) < chance) {
        return chars[Math.floor(Math.random() * chars.length)]!;
    }
    return targetLetter;
}

onMounted(() => {
    setTimeout(() => {
        for (let i = 0; i < props.consecutiveCount; i++) {
            const interval = setInterval(() => {
                const randomIndex = getRandomNonRandomizedIndex();

                // Clear interval when everything is decoded
                if (randomIndex === undefined || randomIndex === -1) {
                    clearInterval(interval);
                    return;
                }

                // Update the specific object in the array directly
                randomizedText.value[randomIndex]!.letter = convertLetter(randomIndex, 30)!;

                randomizedText.value[randomIndex]!.count++;

            }, props.interval);
        }
    }, props.delay);
});
</script>

<template>
    <span>
        <span class="sr-only">{{ props.text }}</span>

        <span aria-hidden="true" :class="$style.wrapper">
            <AnimatePresence mode="popLayout">
                <motion.span
                    v-for="(item, i) in randomizedText"
                    :key="`${i}-${item.letter}`"
                    :initial="{ opacity: 0, y: -10, filter: 'blur(4px)' }"
                    :animate="{ opacity: 1, y: 0, filter: 'blur(0px)' }"
                    :exit="{ opacity: 0, y: 10, filter: 'blur(4px)', position: 'absolute' }"
                    :transition="{ duration: 0.15 }"
                    :class="$style.letter"
                >
                    {{ item.letter }}
                </motion.span>
            </AnimatePresence>
        </span>
    </span>
</template>

<style module lang="scss">
.wrapper {
    white-space: pre-wrap; /* Preserves spaces and newlines */
    position: relative;
    display: inline-block;
}

.letter {
    display: inline-block; /* Required for transforms (y-axis) to work */
}
</style>