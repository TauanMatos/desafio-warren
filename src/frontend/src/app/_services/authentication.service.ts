import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { AuthResponse } from '../_models/authResponse';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<AuthResponse>;
    public currentUser: Observable<AuthResponse>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<AuthResponse>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): AuthResponse {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<AuthResponse>(`${environment.apiUrl}/api/Login/Authenticate`, { "userName": username, "password": password })
            .pipe(map(data => {
                if(data.authenticated){
                    localStorage.setItem('currentUser', JSON.stringify(data));
                }
                this.currentUserSubject.next(data);
                return data;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}