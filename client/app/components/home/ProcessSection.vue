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

const sections: Section[] = [
    {
        title: "Consultation",
        description: `Every successful project starts with understanding your goals. We'll discuss your requirements, challenges, and vision to identify the best solution for your needs.
        \n
        This can be done through any way that suits you best, whether it's a quick call, a detailed questionnaire, or an in-person meeting. The goal is to ensure we're aligned and ready to move forward together.`
    },
    {
        title: "Choose what you need",
        description: `Every project is different, which is why we offer flexible services that can be used individually or combined into a complete solution.
        \n
        - UI/UX Design
        - Web Development
        - Application Development
        - Hosting & Infrastructure
        - Maintenance & Support
        - Full-Service Solutions
        \n
        Choose only what you need today and expand your services as your project grows.`
    },
    {
        title: "Build & Deploy",
        description: "Once the plan is clear, we bring your project to life. From creating intuitive designs and developing reliable applications to deploying them on modern infrastructure, we handle the technical work so you can focus on your business."
    },
    {
        title: "Ongoing Support",
        description: "Launching is only the beginning. We can continue to support your project with maintenance, updates, monitoring, hosting management, and future improvements as your needs evolve."
    },
];

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
                    <h1>From Idea to Launch,<br/>Your Way</h1>
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
    height: 40vh;
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
        
        .left {
            width: 50%;
            color: var(--color-primary);
        }
        
        .content {
            width: 50%;
            
            h2 {
                font-size: 36px;
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
}

@media screen and (max-width: $tabletBreakpoint) {
}

@media screen and (max-width: $mobileBreakpoint) {
}
</style>