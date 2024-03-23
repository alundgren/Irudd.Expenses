import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService, getApiUrl } from '../../common/api.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [ReactiveFormsModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})
export class LoginComponent {
    constructor(private apiService: ApiService) {

    }
    loginForm = new FormGroup({
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required])
    });

    login(evt ?: Event) {
        evt?.preventDefault();
        let request = {
            email: this.loginForm.value.email,
            password: this.loginForm.value.password
        };
        this.apiService.postWithoutAccessToken<{
            tokenType: string,
            accessToken: string,
            expiresIn: number,
            refreshToken: string }>('v1/identity/login', request).subscribe(({accessToken}) => {
                console.log(accessToken);
            });
    }
}
