import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { UserForLogin } from '../model/user';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {

  headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

  private userSubject: BehaviorSubject<UserForLogin | null>;
  public user: Observable<UserForLogin | null>;

  constructor(private http: HttpClient) {
    const userJson = localStorage.getItem('user');
    this.userSubject = new BehaviorSubject<UserForLogin | null>(userJson ? JSON.parse(userJson) : null);
    this.user = this.userSubject.asObservable();
    }


  public get currentUserValue(): UserForLogin | null {
    return this.userSubject.value;
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
    this.userSubject.next(null);
  }

  getUsers(): Observable<UserForLogin[]> {
    return this.http.get<UserForLogin[]>(`${environment.baseUrl}/users/`);
  }

  getUserById(id: number) {
    return this.http.get<UserForLogin>(`${environment.baseUrl}/users/${id}`);
  }

  getUserByUserName(userName: string) {
    return this.http.get<UserForLogin>(`${environment.baseUrl}/users/by-user-name/${userName}`);
  }

  deleteUser(id: number) {
    return this.http.delete(`${environment.baseUrl}/users/${id}`);
  }

}