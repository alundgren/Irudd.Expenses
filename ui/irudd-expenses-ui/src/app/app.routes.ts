import { Routes } from '@angular/router';
import { AuthGuard } from './features/auth/auth.guard';
import { LoginComponent } from './features/auth/login/login.component';
import { OverviewComponent } from './features/overview/overview.component';

export const routes: Routes = [
    { path: '', redirectTo: 'secure/overview', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'secure', canActivate: [AuthGuard], children: [
        { path: 'overview', component: OverviewComponent }
    ]}
];
