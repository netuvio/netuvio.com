<script setup lang="ts">
const { t } = useI18n();

import { motion } from "motion-v";
import Gradient from "~/components/home/Gradient.vue";
import IonMail from '~icons/ion/mail';
import Fa7BrandsLinkedinIn from '~icons/fa7-brands/linkedin-in';
import Fa7BrandsInstagram from '~icons/fa7-brands/instagram';
import Fa7BrandsFacebookF from '~icons/fa7-brands/facebook-f';
import Fa7BrandsGithub from '~icons/fa7-brands/github';
</script>

<template>
    <footer :class="['theme-primary', $style.footer]" id="footer">
        <Gradient :class="$style.gradient" />
        <div :class="['container', $style.container]">
            <section :class="$style.linkSection">
                <motion.div 
                    :class="$style.info"
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6 }"
                >
                    <NuxtLinkLocale to="/">
                        <div :class="$style.logo"></div>
                    </NuxtLinkLocale>
                    <span>{{ t('footer.description') }}</span>
                    <span :class="$style.email"><a href="mailto:info@netuvio.com"><IonMail/></a> info@netuvio.com</span>
                    <ul :class="$style.links">
                        <li><a target="_blank" href="https://github.com/netuvio"><Fa7BrandsGithub/></a></li>
                        <li><a target="_blank" href="https://www.linkedin.com/company/netuvio"><Fa7BrandsLinkedinIn/></a></li>
                        <li><a target="_blank" href="https://www.instagram.com/netuvio_com/"><Fa7BrandsInstagram/></a></li>
                        <li><a target="_blank" href="https://www.facebook.com/netuvio"><Fa7BrandsFacebookF/></a></li>
                    </ul>
                </motion.div>
                <motion.nav
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.1 }"
                >
                    <h1>{{ t('footer.design.title') }}</h1>
                    <ul>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                    </ul>
                </motion.nav>
                <motion.nav
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.2 }"
                >
                    <h1>{{ t('footer.development.title') }}</h1>
                    <ul>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                    </ul>
                </motion.nav>
                <motion.nav
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.3 }"
                >
                    <h1>{{ t('footer.hosting.title') }}</h1>
                    <ul>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#">{{ t('footer.loremIpsum') }}</NuxtLinkLocale></li>
                    </ul>
                </motion.nav>
                <motion.nav
                    :initial="{ opacity: 0, y: 30 }"
                    :whileInView="{ opacity: 1, y: 0 }"
                    :inViewOptions="{ once: true }"
                    :transition="{ duration: 0.6, delay: 0.4 }"
                >
                    <h1>{{ t('footer.about.title') }}</h1>
                    <ul>
                        <li><NuxtLinkLocale to="/#brief">{{ t('footer.about.brief') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#services">{{ t('footer.about.services') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#process">{{ t('footer.about.process') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/projects">{{ t('footer.about.projects') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#why-choose-us">{{ t('footer.about.whyChooseUs') }}</NuxtLinkLocale></li>
                        <li><NuxtLinkLocale to="/#contact">{{ t('footer.about.contact') }}</NuxtLinkLocale></li>
                    </ul>
                </motion.nav>
            </section>
            <motion.hr 
                :initial="{ scaleX: 0, opacity: 0 }"
                :whileInView="{ scaleX: 1, opacity: 1 }"
                :inViewOptions="{ once: true }"
                :transition="{ duration: 1, delay: 0.5, ease: 'easeOut' }"
            />
            <motion.section 
                :class="$style.copyrightSection"
                :initial="{ opacity: 0, y: 20 }"
                :whileInView="{ opacity: 1, y: 0 }"
                :inViewOptions="{ once: true }"
                :transition="{ duration: 0.6, delay: 0.6 }"
            >
                <div>&copy; {{(new Date()).getUTCFullYear()}} Netuvio, {{ t('footer.allRightsReserved') }}</div>
                <div :class="$style.terms">
                    <NuxtLinkLocale to="/privacy">{{ t('footer.privacyPolicy') }}</NuxtLinkLocale>
                    <NuxtLinkLocale to="/terms">{{ t('footer.termsOfService') }}</NuxtLinkLocale>
                </div>
            </motion.section>
        </div>
    </footer>
</template>

<style module lang="scss">

.gradient {
    transform: rotate(180deg);
    position: absolute;
    top: -50px;
    z-index: -1;
}

.footer {
    padding: clamp(120px, 14vw, 200px) 0 50px;
    
    .linkSection {
        display: grid;
        grid-template-columns: minmax(280px, 40%) repeat(4, 1fr);
        gap: 24px;
        
        .info {
            margin-right: 64px;
            
            .logo {
                mask-image: url('/icons/logo-full.svg');
                mask-repeat: no-repeat;
                width: 100%;
                height: 40px;
                background-color: white;
                margin-bottom: 12px;
            }
            
            .email {
                display: flex;
                align-items: center;
                gap: 8px;
                margin-top: 16px;
                
                svg {
                    font-size: 20px;
                }
            }
            
            .links {
                list-style: none;
                padding: 0;
                display: flex;
                align-items: center;
                gap: 8px;
                margin-top: 16px;
                
                li a {
                    font-size: 24px;
                    border-radius: 9999px;
                    padding: 6px;
                    display: block;
                    background-color: var(--color-primary);
                    color: var(--color-background-primary);
                    transition: background-color 200ms;
                    
                    &:hover {
                        background-color: var(--color-lime-200);
                    }
                }
            }
        }
        
        nav {
            margin-top: 16px;
            
            h1 {
                font-size: 20px;
                font-weight: 600;
                margin-bottom: 12px;
            }
            
            ul {
                list-style: none;
                padding: 0;
                
                a {
                    color: var(--color-text-primary);
                    transition: color 0.15s ease-in-out;
                    padding: 4px 0;

                    &:hover {
                        color: var(--color-lime-300);
                    }
                }
            }
        }
    }
    
    .copyrightSection {
        display: flex;
        justify-content: space-between;
        gap: 16px;
        
        .terms {
            display: flex;
            gap: 16px;
        }
    }
    
    hr {
        width: 100%;
        border: none;
        //border-top: 1px solid var(--color-carbon-500);
        margin: 64px 0;
        height: 1px;
        background: radial-gradient(circle, var(--color-primary) 0%, transparent 100%);
        position: relative;
        overflow: visible;
    }
}

@media screen and (max-width: 1200px) {
    .footer .linkSection {
        grid-template-columns: repeat(4, 1fr);

        .info {
            grid-column: 1 / -1;
            margin-right: 0;
            max-width: 520px;
        }
    }
}

@media screen and (max-width: 800px) {
    .footer {
        .linkSection {
            grid-template-columns: repeat(2, minmax(0, 1fr));
        }

        .copyrightSection {
            flex-direction: column;
        }
    }
}

@media screen and (max-width: 520px) {
    .footer {
        .linkSection {
            grid-template-columns: 1fr;
        }

        .copyrightSection .terms {
            flex-direction: column;
            gap: 8px;
        }
    }
}
</style>
