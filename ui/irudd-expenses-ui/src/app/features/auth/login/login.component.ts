import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../common/api.service';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [ReactiveFormsModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})
export class LoginComponent {
    constructor(private api: ApiService) {

    }

    loginForm = new FormGroup({
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required])
    });

    login(evt ?: Event) {
        evt?.preventDefault();
    }
}
