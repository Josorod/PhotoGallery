export interface Photo {
    id: number;
    albumId: number;
    name: string;
    path: string;
    created: Date;
    likes: number;
    isLiked: boolean;
  }