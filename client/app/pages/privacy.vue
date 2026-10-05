<script setup lang="ts">
import { computed } from "vue";
import { motion } from "motion-v";
import { Button } from "@netuvio/ui/vue";
import Markdown from "~/components/Markdown.vue";
import { privacyCs, privacyEn } from "~/data/legal/privacy";
import TablerArrowLeft from "~icons/tabler/arrow-left";
import TablerCalendar from "~icons/tabler/calendar";
import TablerShieldCheck from "~icons/tabler/shield-check";
import TablerMail from "~icons/tabler/mail";
import TablerLock from "~icons/tabler/lock";
import TablerInfoCircle from "~icons/tabler/info-circle";

const { t, locale } = useI18n();

const privacyMarkdown = computed(() => {
    return locale.value === "cs" ? privacyCs : privacyEn;
});
</script>

<template>
    <Head>
        <Title>{{ t('privacy.title') }} • Netuvio</Title>
        <Meta name="description" :content="t('privacy.subtitle')" />
    </Head>

    <div :class="[$style.page, 'theme-primary']">
        <div :class="$style.topographyBg"></div>

        <div :class="['container', $style.container]">
            <!-- Top Action Navigation -->
            <motion.div
                :class="$style.topBar"
                :initial="{ opacity: 0, y: -15 }"
                :animate="{ opacity: 1, y: 0 }"
                :transition="{ duration: 0.4 }"
            >
                <NuxtLinkLocale to="/" :class="$style.backLink">
                    <Button variant="tertiary" size="md">
                        <TablerArrowLeft :class="$style.buttonIcon" />
                        {{ t('privacy.backHome') }}
                    </Button>
                </NuxtLinkLocale>
            </motion.div>

            <!-- Header & Title -->
            <motion.header
                :class="$style.header"
                :initial="{ opacity: 0, y: 20 }"
                :animate="{ opacity: 1, y: 0 }"
                :transition="{ duration: 0.5, delay: 0.1 }"
            >
                <h1 :class="$style.title">{{ t('privacy.title') }}</h1>
                <p :class="$style.subtitle">{{ t('privacy.subtitle') }}</p>
            </motion.header>

            <!-- Main Legal Content -->
            <motion.article
                :class="$style.documentWrapper"
                :initial="{ opacity: 0, y: 30 }"
                :animate="{ opacity: 1, y: 0 }"
                :transition="{ duration: 0.6, delay: 0.35 }"
            >
                <div :class="$style.documentCard">
                    <Markdown :markdown="privacyMarkdown" />
                </div>
            </motion.article>
        </div>
    </div>
</template>

<style module lang="scss">
@use "~/assets/variables" as *;

.page {
    position: relative;
    min-height: 100vh;
    padding: clamp(160px, 18vw, 220px) 0 100px;
    background-color: var(--color-background-primary);
    color: var(--color-text-primary);
    overflow: hidden;

    .topographyBg {
        position: absolute;
        inset: 0;
        width: 100%;
        height: 700px;
        background-image: radial-gradient(
            circle at 50% 0%,
            hsl(from var(--color-lime-500) h s l / 0.12) 0%,
            transparent 70%
        );
        mask-image: url("/patterns/topography-1.svg");
        mask-repeat: repeat;
        mask-size: auto 100%;
        mask-position: center;
        pointer-events: none;
        z-index: 1;
    }

    .container {
        position: relative;
        z-index: 5;
        max-width: 960px;
        display: flex;
        flex-direction: column;
        gap: 32px;
    }

    .topBar {
        display: flex;
        align-items: center;
        margin-bottom: -8px;

        .backLink {
            text-decoration: none;

            .buttonIcon {
                font-size: var(--font-size-base);
                margin-right: 6px;
            }
        }
    }

    .header {
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        gap: 16px;

        .badgeWrapper {
            display: inline-flex;

            .badge {
                display: inline-flex;
                align-items: center;
                gap: 6px;
                background-color: hsl(from var(--color-primary) h s l / 0.12);
                border: 1px solid var(--color-primary);
                color: var(--color-primary);
                font-size: var(--font-size-xs);
                font-weight: 700;
                text-transform: uppercase;
                letter-spacing: 1.2px;
                padding: 6px 14px;
                border-radius: 9999px;
            }
        }

        .title {
            font-size: var(--font-size-2xl);
            font-weight: 900;
            letter-spacing: -1.5px;
            line-height: 1.1;
            color: var(--color-text-primary);
            margin: 0;
        }

        .subtitle {
            font-size: var(--font-size-lg);
            line-height: 1.6;
            color: var(--color-carbon-100);
            max-width: 760px;
            margin: 0;
        }
    }

    .metaCard {
        background-color: var(--color-carbon-700);
        border: 2px solid var(--color-carbon-400);
        border-radius: 24px;
        padding: 24px 32px;
        display: flex;
        align-items: center;
        justify-content: space-between;
        gap: 24px;
        box-shadow: 0 16px 36px rgba(0, 0, 0, 0.4);

        .metaItem {
            display: flex;
            flex-direction: column;
            gap: 6px;
            flex: 1;

            .metaLabel {
                font-size: var(--font-size-xs);
                font-family: monospace;
                text-transform: uppercase;
                letter-spacing: 1px;
                color: var(--color-carbon-200);
            }

            .metaValue {
                display: flex;
                align-items: center;
                gap: 8px;
                font-size: var(--font-size-sm);
                font-weight: 600;
                color: var(--color-text-primary);

                .metaIcon {
                    font-size: var(--font-size-base);
                    color: var(--color-primary);
                    flex-shrink: 0;
                }

                .contactLink {
                    color: var(--color-primary);
                    text-decoration: none;

                    &:hover {
                        text-decoration: underline;
                    }
                }
            }
        }

        .metaDivider {
            width: 1px;
            height: 44px;
            background-color: var(--color-carbon-400);
        }
    }

    .noticeBox {
        background: linear-gradient(135deg, hsl(from var(--color-primary) h s l / 0.08) 0%, var(--color-carbon-700) 100%);
        border: 1px solid var(--color-primary);
        border-left: 5px solid var(--color-primary);
        border-radius: 16px;
        padding: 20px 24px;
        display: flex;
        align-items: flex-start;
        gap: 16px;

        .noticeIconWrapper {
            flex-shrink: 0;
            padding-top: 2px;

            .noticeIcon {
                font-size: 24px;
                color: var(--color-primary);
            }
        }

        .noticeContent {
            display: flex;
            flex-direction: column;
            gap: 4px;

            .noticeTitle {
                font-size: var(--font-size-base);
                font-weight: 700;
                color: var(--color-text-primary);
                margin: 0;
            }

            .noticeText {
                font-size: var(--font-size-sm);
                line-height: 1.55;
                color: var(--color-carbon-50);
                margin: 0;
            }
        }
    }

    .documentWrapper {
        width: 100%;
    }
}

@media screen and (max-width: $tabletBreakpoint) {
    .page {
        padding-top: 150px;

        .metaCard {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 20px;
            padding: 20px 24px;

            .metaDivider {
                display: none;
            }
        }
    }
}

@media screen and (max-width: $mobileBreakpoint) {
    .page {
        padding-top: 130px;
        padding-bottom: 60px;

        .container {
            gap: 24px;
        }

        .metaCard {
            grid-template-columns: 1fr;
            gap: 16px;
            border-radius: 20px;
            padding: 18px 20px;
        }

        .noticeBox {
            padding: 16px;
            border-radius: 12px;
        }

        .documentWrapper .documentCard {
            padding: 20px 16px;
            border-radius: 20px;
        }
    }
}
</style>
