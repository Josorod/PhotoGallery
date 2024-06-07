import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserService } from './user.service';
import { Album } from '../model/album';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  user: User;

  constructor(
    private http: HttpClient,
    private userService: UserService
  ) {
    this.userService.user.subscribe(x => this.user = x);
  }

  getAlbums(id: number = this.user.id): Observable<Album[]> {
    return this.http.get<Album[]>(`${environment.baseUrl}/users/${id}/albums`);
  }

  getAlbum(id: number = this.user.id): Observable<Album> {
    return this.http.get<Album>(`${environment.baseUrl}/albums/${id}`)
  }

  createAlbum(name: string, description: string): Observable<Album> {
    return this.http.post<Album>(`${environment.baseUrl}/albums`, { name, description })
  }

  updateAlbum(id: number, name: string = null, description: string = null): Observable<Album> {
    return this.http.put<Album>(`${environment.baseUrl}/albums/${id}`, { name, description })
  }

  deleteAlbum(id: number) {
    return this.http.delete(`${environment.baseUrl}/albums/${id}`)
  }
}
