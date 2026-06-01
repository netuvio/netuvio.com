<script setup lang="ts">
import {useScroll, useTransform, motion, useMotionValueEvent} from "motion-v";

const { t } = useI18n();

const sectionRef = ref<HTMLElement | null>(null);

const { scrollYProgress } = useScroll({
    target: sectionRef,
    offset: ["start end", "end start"]
});

const START = 0.3;
const END = 0.7;

const scrollLeft = useTransform(scrollYProgress, [START, END], ["0%", "-100%"]);

interface Section {
    title: string;
    description: string;
}

const sections: Section[] = [
    {
        title: "Lorem ipsum dolor sit amet 1",
        description: "Lorem ipsum dolor sit amet 1",
    },
    {
        title: "Lorem ipsum dolor sit amet 2",
        description: "Lorem ipsum dolor sit amet 2",
    },
    {
        title: "Lorem ipsum dolor sit amet 3",
        description: "Lorem ipsum dolor sit amet 3",
    },
    {
        title: "Lorem ipsum dolor sit amet 4",
        description: "Lorem ipsum dolor sit amet 4",
    },
]

const activeSectionValue = useTransform(scrollYProgress, (v) => {
    if (v < START) return 0;
    if (v >= END) return sections.length - 1;
    const progress = (v - START) / (END - START);
    return Math.floor(progress * sections.length);
});

const activeSection = ref(0);

useMotionValueEvent(activeSectionValue, "change", (v) => {
    activeSection.value = v as number;
});

</script>

<template>
    <section :class="[$style.section, 'theme-primary']" id="process" ref="sectionRef">
        <div :class="['container', $style.container]">
            <motion.div :class="$style.timeline" :style="{ left: scrollLeft }">
                <div v-for="(section, index) in sections" :key="index" :class="$style.timeSection">
                    <div :class="[$style.dot, { [$style.active]: activeSection === index }]">
                        <span>{{section.title}}</span>
                    </div>
                    <div :class="$style.line"></div>
                </div>
            </motion.div>
            <div :class="$style.mainSection">
                <div :class="$style.left">
                    <h1>Process of getting a website</h1>
                </div>
                <div :class="$style.content">
                    <h2>{{sections[activeSection]?.title}}</h2>
                    <p>{{sections[activeSection]?.description}}</p>
                </div>
            </div>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.section {
    min-height: 400vh;
    z-index: 1;
    position: relative;
    padding-top: 300px;
}

.container {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    margin-top: 40vh;
    height: 70vh;
    position: sticky;
    top: 50%;
    transform: translateY(-50%);
    
    .timeline {
        display: flex;
        margin-left: 30%;
        position: relative;
        
        .timeSection {
            display: flex;
            align-items: center;
            
            .dot {
                position: relative;
                width: 22px;
                height: 22px;
                border-radius: 50%;
                background-color: var(--color-carbon-400);
                
                &.active {
                    background-color: var(--color-primary);
                }
                
                span {
                    position: absolute;
                    width: 300px;
                    left: 50%;
                    transform: translateX(-50%);
                    text-align: center;
                    bottom: 32px;
                    font-size: 18px;
                    font-weight: 500;
                }
            }
            
            .line {
                width: 600px;
                height: 3px;
                background-color: var(--color-carbon-500);
            }
            
            &:last-child .line {
                display: none;
            }
        }
    }
    
    .mainSection {
        display: flex;
        
        .left {
            width: 50%;
        }
        
        .content {
            width: 50%;
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