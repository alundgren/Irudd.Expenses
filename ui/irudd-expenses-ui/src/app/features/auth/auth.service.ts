import { Injectable, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    //TODO: ExpirationDate, refreshToken: string
    private readonly auth  = signal<{ isAuthenticated: boolean, accessToken ?: string }>({ isAuthenticated: false });

    isAuthenticated() {
        return this.auth().isAuthenticated;
    }

    accessToken() {
        return this.auth().accessToken;
    }

    loginUrl() {
        return inject(Router).createUrlTree(['/login']);
    }
}
