<script setup lang="ts">
type ButtonVariant = 'primary' | 'secondary' | 'ghost' | 'danger';
type ButtonSize = 'sm' | 'md' | 'lg' | 'xl';

type ButtonType = 'button' | 'submit' | 'reset';

const props = withDefaults(defineProps<{
    variant?: ButtonVariant;
    size?: ButtonSize;
    type?: ButtonType;
    disabled?: boolean;
    loading?: boolean;
    block?: boolean;
}>(), {
    variant: 'primary',
    size: 'md',
    type: 'button',
    disabled: false,
    loading: false,
    block: false,
});

const css = useCssModule();

const isDisabled = computed<boolean>(() => props.disabled || props.loading);

const classes = computed(() => ([
    css.button,
    css[`variant-${props.variant}`],
    css[`size-${props.size}`],
    props.block ? css.block : null,
    props.loading ? css.loading : null,
]));
</script>

<template>
    <button
        :type="props.type"
        :class="classes"
        :disabled="isDisabled"
        :aria-busy="props.loading || undefined"
    >
        <span v-if="props.loading" :class="$style.spinner" aria-hidden="true"></span>
        <span :class="$style.content">
            <slot />
        </span>
    </button>
</template>

<style module lang="scss">
.button {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    gap: 8px;
    border: 1px solid transparent;
    border-radius: 10000px;
    font-weight: 600;
    line-height: 1;
    cursor: pointer;
    transition: background-color 0.18s ease, border-color 0.18s ease, color 0.18s ease, opacity 0.18s ease, transform 0.08s ease;
    user-select: none;

    //&:active {
    //    transform: translateY(1px);
    //}

    &:focus-visible {
        outline: 2px solid var(--color-lime-300);
        outline-offset: 2px;
    }

    &:disabled {
        cursor: not-allowed;
        opacity: 0.65;
        transform: none;
    }
}

.content {
    display: inline-flex;
    align-items: center;
}

.block {
    width: 100%;
}

.loading {
    pointer-events: none;
}

.size-sm {
    min-height: 32px;
    padding: 0 14px;
    font-size: 13px;
}

.size-md {
    min-height: 40px;
    padding: 0 18px;
    font-size: 15px;
}

.size-lg {
    min-height: 48px;
    padding: 0 24px;
    font-size: 18px;
    font-weight: 700;
}

.size-xl {
    min-height: 48px;
    padding: 16px 32px;
    font-size: 20px;
    font-weight: 700;
}

.variant-primary {
    background-color: var(--color-primary);
    border-color: var(--color-primary);
    color: var(--color-carbon-800);

    &:hover:not(:disabled) {
        background-color: var(--color-lime-300);
        border-color: var(--color-lime-300);
    }
}

.variant-secondary {
    background-color: transparent;
    border-color: var(--color-carbon-100);
    color: var(--color-text-primary);

    &:hover:not(:disabled) {
        background-color: var(--color-carbon-600);
        border-color: var(--color-carbon-50);
    }
}

.variant-ghost {
    background-color: transparent;
    color: var(--color-lime-200);

    &:hover:not(:disabled) {
        background-color: rgba(255, 255, 255, 0.08);
        color: var(--color-lime-100);
    }
}

.variant-danger {
    background-color: #c62828;
    border-color: #c62828;
    color: #fff;

    &:hover:not(:disabled) {
        background-color: #b71c1c;
        border-color: #b71c1c;
    }
}

.spinner {
    width: 0.95em;
    height: 0.95em;
    border-radius: 50%;
    border: 2px solid currentcolor;
    border-right-color: transparent;
    animation: spin 0.65s linear infinite;
}

@keyframes spin {
    to {
        transform: rotate(360deg);
    }
}
</style>
