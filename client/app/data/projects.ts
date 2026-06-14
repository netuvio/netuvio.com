// TODO: switch to database

interface Project {
    name: string;
    description: string;
    imageUrl: string;
    isFeatured: boolean;
    type: "website" | "graphics";
}

const projects: Project[] = [
    {
        name: "Project 1",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",
        imageUrl: "https://images.squarespace-cdn.com/content/v1/607f89e638219e13eee71b1e/1684821560422-SD5V37BAG28BURTLIXUQ/michael-sum-LEpfefQf4rU-unsplash.jpg",
        isFeatured: true,
        type: "website"
    },
    {
        name: "Project 2",
        description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        imageUrl: "https://pawspace.in/wp-content/uploads/2021/09/siamese-cat-1.jpg",
        isFeatured: true,
        type: "graphics"
    }
];

export default projects;