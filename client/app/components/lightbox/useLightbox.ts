import {type InjectionKey, inject } from 'vue';

export type LightboxItemData = {
    src: string;
    width: number;
    height?: number;
    alt?: string;
};

export interface LightboxContextType {
    register: (item: LightboxItemData) => void;
    openAt: (index: number) => void;
    openBySrc: (src: string) => void;
}

export const LightboxKey = Symbol('Lightbox') as InjectionKey<LightboxContextType>;

export function useLightbox() {
    const context = inject(LightboxKey);
    if (!context) throw new Error('useLightbox must be used within a Lightbox provider');
    return context;
}