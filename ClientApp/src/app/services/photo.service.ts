import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Photo } from '../model/photo';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

  constructor(private http: HttpClient)
  {
  }

  getPhotos(albumId: number): Observable<Photo[]> {
    return this.http.get<Photo[]>(`${environment.baseUrl}/albums/${albumId}/photos`);
  }

  getPhotosByTags(tags: string[]): Observable<Photo[]> {
    let params = new HttpParams();
    for (let tag of tags) {
      params = params.append('tags', tag);
    }

    return this.http.get<Photo[]>(`${environment.baseUrl}/photosByTags`, {params: params});
  }

  getPhoto(id: number): Observable<Photo> {
    return this.http.get<Photo>(`${environment.baseUrl}/photos/${id}`)
  }

  createPhoto(albumId: number, name: string, path: string): Observable<Photo> {
    return this.http.post<Photo>(`${environment.baseUrl}/albums/${albumId}/photos`, { name, path })
  }

  updatePhoto(id: number, name: string): Observable<Photo> {
    return this.http.put<Photo>(`${environment.baseUrl}/photos/${id}`, { name })
  }

  deletePhoto(id: number) {
    return this.http.delete(`${environment.baseUrl}/photos/${id}`)
  }

  likePhoto(id: number) {
    return this.http.post(`${environment.baseUrl}/photos/${id}/like`, {})
  }

}