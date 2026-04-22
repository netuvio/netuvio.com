<script setup lang="ts">

import Gradient from "~/components/home/Gradient.vue";
import {useScroll, useTransform, motion, AnimatePresence} from "motion-v";

const { t } = useI18n();

const sectionRef = ref<HTMLElement | null>(null);

const { scrollYProgress } = useScroll({
    target: sectionRef
});

const scrollSection = useTransform(scrollYProgress, (v) => {
    if (v < 0.001) return "notScrolled";
    if (v < 0.33) return "first";
    if (v < 0.66) return "second";
    return "third";
});

const currentSection = ref("notScrolled");

const scrollToProgress = (progress: number) => {
    if (!sectionRef.value) return;
    const rect = sectionRef.value.getBoundingClientRect();
    const scrollTop = window.scrollY || document.documentElement.scrollTop;
    
    const startScroll = rect.top + scrollTop - window.innerHeight;
    const totalScrollable = rect.height + window.innerHeight;
    
    window.scrollTo({
        top: startScroll + progress * totalScrollable,
        behavior: "smooth"
    });
};

const animationProps = {
    initial: { opacity: 0, y: 50 },
    animate: { opacity: 1, y: 0 },
    exit: { opacity: 0, y: 0 },
    transition: {
        ease: "easeOut",
        duration: .2,
    }
};

onMounted(() => {
    useMotionValueEvent(scrollSection, "change", (v) => {
        currentSection.value = v;
    });
});
</script>

<template>
    <section :class="[$style.sectionContainer, 'theme-secondary']" id="test">
        <section ref="sectionRef" :class="$style.section">
            <div :class="$style.spacer"></div>
            <div :class="['container', $style.container]">
                <div :class="[$style.wrapper, $style[currentSection]]">
                    <ul>
                        <li @click="scrollToProgress(0.33)">Design</li>
                        <li @click="scrollToProgress(0.50)">Development</li>
                        <li @click="scrollToProgress(0.66)">Hosting</li>
                    </ul>
                    
                    <div :class="$style.description">
                        <AnimatePresence mode="wait">
                            <motion.div
                                v-if="currentSection === 'first'"
                                key="first"
                                v-bind="animationProps"
                            >
                                <img src="/images/design.png" alt="" />
                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis semper, enim quis volutpat semper, nibh mi vulputate velit, quis tempor sem ante eget odio. Nullam eu justo mi. Nulla non augue mattis, sollicitudin arcu ut, commodo magna. In hac habitasse platea dictumst. Praesent sit amet maximus lectus, tempus consectetur urna. Mauris finibus feugiat mauris. Nam ultricies ipsum id justo tempus, at vulputate elit lobortis. Fusce a malesuada ipsum. Vivamus ut sapien at felis placerat ornare eget sed massa. Aliquam quis libero et leo bibendum efficitur quis sit amet tortor. Mauris scelerisque augue sit amet eros cursus pretium. Aliquam malesuada ultricies consequat. Sed non odio ac mauris rutrum sodales vel sit amet est. Vivamus semper purus nisi, ac laoreet justo bibendum sit amet. Duis vel eros at lacus tempus finibus. Nam vel ex fermentum, aliquet erat vel, condimentum urna.</p>
                            </motion.div>

                            <motion.div
                                v-else-if="currentSection === 'second'"
                                key="second"
                                v-bind="animationProps"
                            >
                                <img src="/images/development.png" alt="" />
                                <p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Aenean sed venenatis risus, ac fringilla ipsum. Donec ex leo, scelerisque ac bibendum at, convallis ac tortor. Mauris quis dui sed metus ultrices feugiat et vitae lacus. Phasellus eu ipsum non felis dignissim dapibus. Suspendisse volutpat, quam aliquam sodales consequat, quam sem vestibulum mauris, vel elementum velit felis ac urna. Pellentesque imperdiet enim ac scelerisque tincidunt. Donec porta sem nec massa facilisis eleifend. Nunc luctus cursus quam quis venenatis. Vestibulum scelerisque sodales libero at dignissim. Praesent libero felis, scelerisque ut metus vitae, tincidunt pharetra nisl. Etiam in viverra dolor, id pharetra augue. Quisque quis enim iaculis, faucibus quam at, aliquam libero. Sed sit amet metus nisl. Vivamus elementum ut quam ut mattis. Nunc mollis lacus id orci feugiat, eget porta quam commodo.</p>
                            </motion.div>

                            <motion.div
                                v-else-if="currentSection === 'third'"
                                key="third"
                                v-bind="animationProps"
                            >
                                <p>Praesent dictum, elit ut pharetra placerat, neque diam eleifend mi, et lobortis risus velit nec lacus. Vestibulum sed est urna. Nullam hendrerit nulla a felis blandit feugiat. Phasellus feugiat, urna sed rutrum consequat, quam ligula eleifend velit, in viverra mauris sem suscipit sem. Proin sed quam at neque molestie pellentesque. Sed volutpat in tortor sed consequat. Suspendisse et nisl sit amet turpis gravida scelerisque. Nullam vulputate convallis nulla, at auctor eros ultrices quis.</p>
                            </motion.div>
                        </AnimatePresence>
                    </div>
                </div>
            </div>
        </section>
    </section>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.sectionContainer {
    width: 100%;
    min-height: 4000px;
    height: 4000px;
    position: relative;
    overflow-x: clip;
    padding: 000px 0 400px;
}

.section {
    position: relative;
    overflow-x: clip;
    padding-top: 800px;
    height: 100%;

    .spacer {
        //padding-bottom: 600px;
    }

    .container {
        height: 100%;

        .wrapper {
            position: sticky;
            z-index: 1;
            top: 50%;
            transform: translateY(-50%);
            
            ul {
                list-style: none;
                padding: 0;
                margin-bottom: 32px;
                
                li {
                    font-weight: 700;
                    font-size: 48px;
                    transition: color 0.2s ease-in-out;
                    cursor: pointer;
                }
            }
            
            .description {
                height: 300px;
                
                div {
                    display: flex;
                    flex-direction: row;
                    gap: 32px;
                    
                    img {
                        object-fit: contain;
                        border-radius: 20px;
                        //border: 1px solid rgba(0, 0, 0, 0.2);
                        //background-color: hsl(0, 0%, 98%);
                        width: 400px;
                        height: 400px;
                    }
                    
                    p {
                        border-radius: 36px;
                        padding: 16px;
                        background-color: hsl(0, 0%, 98%);
                        border: 1px solid hsl(0, 0%, 95%);
                    }
                }
            }
            
            &.first {
                ul li:nth-child(1) {
                    color: var(--color-primary);
                }
            }

            &.second {
                ul li:nth-child(2) {
                    color: var(--color-primary);
                }
            }

            &.third {
                ul li:nth-child(3) {
                    color: var(--color-primary);
                }
            }
        }
    }
}
</style>