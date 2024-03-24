import { Injectable, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, EMPTY, Observable, Subject, map, of, throwError } from 'rxjs';
import { ApiService } from '../common/api.service';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private apiService: ApiService) {
        const storedAuthRaw = sessionStorage.getItem(AccessTokenKey);
        const auth : AuthData = storedAuthRaw ? JSON.parse(storedAuthRaw) : null;
        this.authState = new BehaviorSubject<{ isAuthenticated: boolean, accessToken ?: string }>(auth 
            ? { isAuthenticated: true, accessToken: auth.accessToken } 
            : { isAuthenticated: false });
    }

    private readonly authState:  BehaviorSubject<{ isAuthenticated: boolean, accessToken ?: string }>;

    isAuthenticated() {
        return this.authState.value.isAuthenticated;
    }

    accessToken() {
        return this.authState.value.accessToken;
    }

    loginUrl() {
        return inject(Router).createUrlTree(['/login']);
    }

    login(email: string, password: string, persistRefreshToken: boolean) {
        return this.apiService.post<AuthData>('v1/identity/login', { email, password }, {
                handleError: err => {
                    if(err.status === 401) {
                        (err as any).message = 'Invalid email or password.';
                        return throwError(() => err);
                    } else {
                        return throwError(() => err);
                    }
                }
        })
        .pipe(map(x => {
            sessionStorage.setItem(AccessTokenKey, JSON.stringify(x));
            if(persistRefreshToken) {
                localStorage.setItem(RefreshTokenKey, JSON.stringify(x));
            }
            this.authState.next({ isAuthenticated: true, accessToken: x.accessToken });
            return true;
        }));
    }
}

const RefreshTokenKey = 'irudd_expenses_refresh_token_2024032401'
const AccessTokenKey = 'irudd_expenses_access_token_2024032401';

interface AuthData {
    tokenType: string,
    expiresIn: number,
    refreshToken: string,
    accessToken : string
}