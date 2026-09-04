<script setup lang="ts">
type ButtonVariant = 'primary' | 'secondary' | 'tertiary' | 'ghost' | 'danger';
type ButtonSize = 'sm' | 'md' | 'lg' | 'xl';

// Native button types forwarded to the underlying <button> element.
type ButtonType = 'button' | 'submit' | 'reset';

const props = withDefaults(defineProps<{
    /** Visual style variant for the button */
    variant?: ButtonVariant;
    
    /** Predefined size for the button */
    size?: ButtonSize;
    
    /** Native button type attribute */
    type?: ButtonType;
    
    disabled?: boolean;
    
    /** Shows a loading spinner and disables the button when true */
    loading?: boolean;
    
    /** Makes the button take the full width of its container when true */
    block?: boolean;
    
    arrow?: boolean;

    /** Enables 3D layered hover effect (defaults to true for primary and danger variants) */
    threeD?: boolean;
}>(), {
    variant: 'primary',
    size: 'md',
    type: 'button',
    disabled: false,
    loading: false,
    block: false,
    arrow: true,
    threeD: undefined,
});

// CSS module classes generated for this component.
const css = useCssModule();

const isDisabled = computed<boolean>(() => props.disabled || props.loading);

// const is3D = computed<boolean>(() => props.threeD ?? (props.variant === 'primary' || props.variant === 'tertiary' || props.variant === 'danger'));
const is3D = computed<boolean>(() => props.threeD ?? (props.variant !== 'ghost'));

// Resolved CSS module class list for the button element.
const classes = computed(() => ([
    css.button,
    css[`variant-${props.variant}`],
    css[`size-${props.size}`],
    props.block ? css.block : null,
    is3D.value ? css.is3d : null,
    props.loading ? css.loading : null,
    props.arrow ? css.arrow : null,
]));
</script>

<template>
    <button
        :type="props.type"
        :class="classes"
        :disabled="isDisabled"
        :aria-busy="props.loading || undefined"
    >
        <span v-if="is3D" :class="$style.bottomLayer" aria-hidden="true"></span>
        <span :class="$style.topLayer">
            <span v-if="props.loading" :class="$style.spinner" aria-hidden="true"></span>
            <span :class="$style.content">
                <slot />
            </span>
        </span>
    </button>
</template>

<style module lang="scss">
.button {
    position: relative;
    display: inline-flex;
    padding: 0 !important;
    margin: 0;
    border: none;
    background: transparent;
    border-radius: 10000px;
    font-family: inherit;
    font-weight: 600;
    line-height: 1;
    cursor: pointer;
    user-select: none;
    vertical-align: middle;
    text-decoration: none;
    outline: none;
    transition: opacity 0.18s ease;

    &:disabled {
        cursor: not-allowed;
        opacity: 0.65;

        .topLayer {
            transform: none !important;
        }
    }
}

.bottomLayer {
    position: absolute;
    inset: 0;
    border-radius: 10000px;
    background-color: var(--color-background-primary);
    pointer-events: none;
    z-index: 0;
    transform: scale(.99);
}

.topLayer {
    position: relative;
    z-index: 1;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    gap: 8px;
    border: 2px solid var(--color-background-secondary);
    border-radius: 10000px;
    font-weight: 600;
    line-height: 1;
    width: 100%;
    transition: transform 0.15s cubic-bezier(0.2, 0, 0, 1),
                background-color 0.18s ease,
                border-color 0.18s ease,
                color 0.18s ease;
    will-change: transform;
}

.is3d {
    &:hover:not(:disabled) {
        .topLayer {
            transform: translate(-4px, -4px);
        }
    }

    &:active:not(:disabled) {
        .topLayer {
            transform: translate(0, 0);
            transition-duration: 0.06s;
        }
    }
}

.content {
    display: inline-flex;
    align-items: center;
}

.block {
    width: 100%;

    .topLayer {
        width: 100%;
    }
}

.loading {
    pointer-events: none;
}

.size-sm {
    .topLayer {
        min-height: 32px;
        padding: 0 14px;
        font-size: 13px;
    }
}

.size-md {
    .topLayer {
        min-height: 40px;
        padding: 0 18px;
        font-size: 15px;
    }
}

.size-lg {
    .topLayer {
        min-height: 48px;
        padding: 0 24px;
        font-size: 18px;
        font-weight: 700;
    }
}

.size-xl {
    .topLayer {
        min-height: 48px;
        padding: 16px 32px;
        font-size: 20px;
        font-weight: 700;
    }
}

.variant-primary {
    .topLayer {
        background-color: var(--color-primary);
        color: var(--color-text-secondary);
        border-color: var(--color-background-primary);

        &::after {
            background-color: var(--color-text-secondary);
        }
    }

    &:hover:not(:disabled) {
        .topLayer {
            //background-color: var(--color-lime-300);
        }
    }
    
    .bottomLayer {
        background-color: var(--color-background-primary);
    }
}

.variant-secondary {
    .topLayer {
        background-color: var(--color-background-secondary);
        color: var(--color-text-secondary);
        border-color: var(--color-background-primary);

        &::after {
            background-color: var(--color-text-secondary);
        }
    }

    &:hover:not(:disabled) {
        .topLayer {
            //background-color: var(--color-carbon-600);
        }
    }
}

.variant-tertiary {
    .topLayer {
        background-color: var(--color-background-primary);
        color: var(--color-text-primary);

        &::after {
            background-color: var(--color-text-primary);
        }
    }

    &:hover:not(:disabled) {
        .topLayer {
            //background-color: var(--color-background-primary-hover);
        }
    }

    .bottomLayer {
        background-color: var(--color-background-secondary);
    }
}

.variant-ghost {
    .topLayer {
        background-color: transparent;
        color: var(--color-lime-200);
        border: none;
    }

    &:hover:not(:disabled) {
        .topLayer {
            background-color: rgba(255, 255, 255, 0.08);
            color: var(--color-lime-100);
        }
    }
}

.variant-danger {
    .topLayer {
        background-color: #c62828;
        color: #fff;

        &::after {
            background-color: #fff;
        }
    }

    &:hover:not(:disabled) {
        .topLayer {
            //background-color: #b71c1c;
        }
    }

    .bottomLayer {
        background-color: var(--color-background-secondary);
    }
}

.spinner {
    width: 0.95em;
    height: 0.95em;
    border-radius: 50%;
    border: 2px solid currentcolor;
    border-right-color: transparent;
    animation: spin 0.65s linear infinite;
    flex-shrink: 0;
}

@keyframes spin {
    to {
        transform: rotate(360deg);
    }
}

.arrow {
    .topLayer::after {
        content: "";
        mask-image: url("/images/arrow.svg");
        mask-size: contain;
        mask-repeat: no-repeat;
        mask-position: center;
        width: 15px;
        height: 15px;
        display: inline-block;
        flex-shrink: 0;
    }
}

@media (prefers-reduced-motion: reduce) {
    .topLayer {
        transition: none !important;
    }
}
</style>