<script setup lang="ts">
import {useScroll, useTransform, motion, useMotionValueEvent, AnimatePresence} from "motion-v";

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

const sections = computed<Section[]>(() => [
    {
        title: t('process.sections.consultation.title'),
        description: t('process.sections.consultation.description')
    },
    {
        title: t('process.sections.choose.title'),
        description: t('process.sections.choose.description')
    },
    {
        title: t('process.sections.build.title'),
        description: t('process.sections.build.description')
    },
    {
        title: t('process.sections.support.title'),
        description: t('process.sections.support.description')
    },
]);

function parseDescription(description: string) {
    return description
        .split("\n")
        .map(line => {
            line = line.trim();
            if (line.startsWith("- ")) {
                return `<li>${line.slice(2)}</li>`;
            }
            
            if (line === "") {
                return "<br/>";
            }

            return line;
        })
        .join("");
}

const activeSectionValue = useTransform(scrollYProgress, (v) => {
    const count = sections.value.length;

    if (v < START) return 0;
    if (v >= END) return count - 1;

    const progress = (v - START) / (END - START);
    return Math.floor(progress * count);
});

const activeSection = ref(0);

useMotionValueEvent(activeSectionValue, "change", (v) => {
    activeSection.value = v as number;
});

</script>

<template>
    <section :class="[$style.section, 'theme-primary']" id="process" ref="sectionRef">
        <div :class="$style.bg"></div>
        <div :class="$style.transition"></div>
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
                    <h1 v-html="t('process.title')"></h1>
                </div>
                <AnimatePresence mode="wait">
                    <motion.div 
                        :class="$style.content"
                        :key="activeSection"
                        :initial="{ opacity: 0, x: 40 }"
                        :animate="{ opacity: 1, x: 0 }"
                        :exit="{ opacity: 0, x: -40 }"
                        :transition="{ duration: 0.2, ease: 'easeInOut' }"
                    >
                        <h2>{{activeSection + 1}}. {{sections[activeSection]?.title}}</h2>
<!--                        <p>{{sections[activeSection]?.description}}</p>-->
                        <p v-html="parseDescription(sections[activeSection]?.description ?? '')"/>
                    </motion.div>
                </AnimatePresence>
            </div>
        </div>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.transition {
    width: 100%;
    height: 50px;
    background-color: var(--color-primary);
    position: absolute;
    top: -50px;
}

.bg {
    position: sticky;
    inset: 0;
    height: 100vh;
    margin-bottom: -100vh;
    mask-image: url("/patterns/line-in-motion.svg");
    mask-repeat: repeat;
    mask-size: 128px;
    background-color: var(--color-carbon-700);
    opacity: .5;
}

.section {
    min-height: 400vh;
    z-index: 1;
    position: relative;
    overflow-x: clip;
}

.container {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    margin-top: calc(40vh + clamp(180px, 21vw, 300px));
    height: 40vh;
    position: sticky;
    top: 50%;
    transform: translateY(-50%);
    
    .timeline {
        display: flex;
        margin-left: 30%;
        position: relative;
        width: max-content;
        
        .timeSection {
            display: flex;
            align-items: center;
            
            .dot {
                position: relative;
                width: 18px;
                height: 18px;
                border-radius: 50%;
                background-color: var(--color-carbon-400);
                transition: all 0.2s ease-in-out;
                
                &.active {
                    background-color: var(--color-primary);
                    box-shadow: 0 0 8px 2px hsla(81, 84%, 56%, 0.5);
                    scale: 1.2;
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
        flex-grow: 1;
        padding-top: 10%;
        gap: 32px;
        
        .left {
            width: 50%;
            color: var(--color-primary);
            
            h1 {
                position: relative;
                
                span {
                    color: var(--color-background-primary);
                    background-color: var(--color-primary);
                    padding: 0 12px;
                    border-radius: 16px;
                }
            }
        }
        
        .content {
            width: 50%;
            
            h2 {
                font-size: clamp(28px, 2.5vw, 36px);
                margin-bottom: 12px;
            }
            
            p {
                li {
                    margin-left: 40px;
                }
            }
        }
    }
}

@media screen and (max-width: $laptopBreakpoint) {
    .container .timeline {
        margin-left: 18%;

        .timeSection .line {
            width: 420px;
        }
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .section {
        min-height: 320vh;
    }

    .container {
        height: 54vh;

        .timeline {
            margin-left: 10%;

            .timeSection {
                .dot span {
                    width: 220px;
                    font-size: 15px;
                }

                .line {
                    width: 300px;
                }
            }
        }

        .mainSection {
            flex-direction: column;
            gap: 24px;
            padding-top: 12%;

            .left,
            .content {
                width: 100%;
            }
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .section {
        min-height: 300vh;
    }

    .container {
        height: 62vh;

        .timeline {
            margin-left: 18%;

            .timeSection {
                .dot {
                    width: 16px;
                    height: 16px;

                    span {
                        width: 150px;
                        font-size: 13px;
                    }
                }

                .line {
                    width: 220px;
                }
            }
        }

        .mainSection {
            padding-top: 18%;

            .content p li {
                margin-left: 24px;
            }
        }
    }
}
</style>
