export type WebTheme = "dark" | "light";

export interface HeaderLink {
    name: string;
    to: string;
}

export type ProjectType = "Website" | "Graphics";

export interface Project {
    id: string;
    slug: string;
    title: string;
    description?: string;
    body: string;
    isFeatured: boolean;
    imageUrls: string[];
    type: ProjectType;
    technologies: string[];
    startedAt: string;
    finishedAt: string;
}