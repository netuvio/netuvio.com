<script setup lang="ts">
import {onMounted, onUnmounted, ref, computed, watch} from "vue";
import { motion, AnimatePresence } from "motion-v";

const props = withDefaults(defineProps<{
    text: string,
    
    /** Interval between each randomization in milliseconds (default: 50) */
    interval?: number,
    
    /** How many consecutive randomizations happen at a time */
    consecutiveCount?: number,
    
    delay?: number,

    /** Include raw hidden text for SEO and accessibility */
    seoFriendly?: boolean
    
    /** Hides the whole component from SEO (seoFriendly does this automatically) */
    ariaHidden?: boolean
}>(), {
    interval: 50,
    consecutiveCount: 1,
    delay: 0,
    seoFriendly: true,
    ariaHidden: false,
})

const chars = '!@#$%^&*()=+';

function getInitialScramble(char: string, index: number) {
    if (!/[a-zA-Z]/.test(char)) return char;
    return chars[Math.floor(Math.random() * chars.length)]!;
}

interface LetterWithCount {
    letter: string;
    count: number;
}

const randomizedText = ref<LetterWithCount[]>([]);

function initText() {
    randomizedText.value = props.text.split('').map((char, index) => ({
        letter: getInitialScramble(char, index),
        count: 0
    }));
}

initText();

interface LineItem {
    item: LetterWithCount;
    originalIndex: number;
}

interface TextLine {
    id: number;
    items: LineItem[];
}

const lines = computed<TextLine[]>(() => {
    const result: TextLine[] = [];
    let currentItems: LineItem[] = [];
    let lineId = 0;

    randomizedText.value.forEach((item, index) => {
        if (props.text[index] === '\n') {
            result.push({ id: lineId++, items: currentItems });
            currentItems = [];
        } else {
            currentItems.push({ item, originalIndex: index });
        }
    });

    result.push({ id: lineId, items: currentItems });
    return result;
});

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

const activeIntervals: ReturnType<typeof setInterval>[] = [];
let delayTimeout: ReturnType<typeof setTimeout> | null = null;

function clearAllTimers() {
    if (delayTimeout) {
        clearTimeout(delayTimeout);
        delayTimeout = null;
    }
    activeIntervals.forEach(clearInterval);
    activeIntervals.length = 0;
}

function startAnimation() {
    clearAllTimers();
    delayTimeout = setTimeout(() => {
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
            activeIntervals.push(interval);
        }
    }, props.delay);
}

onMounted(() => {
    startAnimation();
});

watch(() => props.text, () => {
    initText();
    startAnimation();
});

onUnmounted(() => {
    clearAllTimers();
});
</script>

<template>
    <span class="sr-only" v-if="seoFriendly">{{ props.text }}</span>
    
    <span :aria-hidden="seoFriendly || ariaHidden" :class="$style.wrapper">
        <span
            v-for="line in lines"
            :key="line.id"
            :class="[$style.line, lines.length === 1 && $style.singleLine]"
        >
            <AnimatePresence mode="popLayout">
                <motion.span
                    v-for="{ item, originalIndex } in line.items"
                    :key="`${originalIndex}-${item.letter}`"
                    :initial="{ opacity: 0, y: -10, filter: 'blur(4px)' }"
                    :animate="{ opacity: 1, y: 0, filter: 'blur(0px)' }"
                    :exit="{ opacity: 0, y: 10, filter: 'blur(4px)', position: 'absolute' }"
                    :transition="{ duration: 0.15 }"
                    :class="$style.letter"
                    :aria-hidden="seoFriendly || ariaHidden"
                >{{ item.letter }}</motion.span>
            </AnimatePresence>
        </span>
    </span>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.wrapper {
    position: relative;
    display: inline-block;
    max-width: 100%;
}

.line {
    display: block;
    white-space: nowrap;
}

.singleLine {
    display: inline-block;
}

.letter {
    display: inline-block;
    white-space: pre;
}

@media screen and (max-width: $laptopBreakpoint) {
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>