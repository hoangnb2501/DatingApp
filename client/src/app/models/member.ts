import { Photo } from './photo';

export interface Member {
  id: number;
  username: string;
  photoUrl: string;
  age: number;
  knownAs: any;
  createdAt: string;
  lastActiveAt: string;
  gender: string;
  introduction: string;
  lookingFor: string;
  interests: string;
  city: string;
  country: string;
  photos: Photo[];
}
