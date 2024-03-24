import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService, getApiUrl } from '../../common/api.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [ReactiveFormsModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})
export class LoginComponent {
    constructor(private authService: AuthService, private router: Router) {

    }
    loginForm = new FormGroup({
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required])
    });

    login(evt ?: Event) {
        evt?.preventDefault();
        this.authService.login(this.loginForm.value.email ?? '', this.loginForm.value.password ?? '', false).subscribe(wasLoggedIn => {
            if(wasLoggedIn) {
                this.router.navigate(['secure/overview']);
            }
        });
    }
}
