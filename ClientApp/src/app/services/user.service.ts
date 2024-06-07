import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from '../model/user';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {

  headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

  private userSubject: BehaviorSubject<User | null>;
  public user: Observable<User | null>;

  constructor(private http: HttpClient) {
    const userJson = localStorage.getItem('user');
    this.userSubject = new BehaviorSubject<User | null>(userJson ? JSON.parse(userJson) : null);
    this.user = this.userSubject.asObservable();
    }


  public get currentUserValue(): User | null {
    return this.userSubject.value;
  }

  login(email: string, password: string) {
    return this.http.post<any>(`${environment.baseUrl}/login`, { email, password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
      }));
  }

  register(email: string, password: string) {
    return this.http.post<any>(`${environment.baseUrl}/register`, { email, password });
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
    this.userSubject.next(null);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.baseUrl}/users/`);
  }

  getUserById(id: number) {
    return this.http.get<User>(`${environment.baseUrl}/users/${id}`);
  }

  getUserByUserName(userName: string) {
    return this.http.get<User>(`${environment.baseUrl}/users/by-user-name/${userName}`);
  }

  deleteUser(id: number) {
    return this.http.delete(`${environment.baseUrl}/users/${id}`);
  }

}