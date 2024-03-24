import { Injectable, inject } from '@angular/core';
import { environment } from "../../../environments/environment.development";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { MessageService } from './message.service';
import { EMPTY, Observable, catchError, from, of, throwError } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ApiService {
    constructor(private messageService: MessageService, private httpClient: HttpClient) {

    }

    post<TResponse>(relativeUrl: string, data: any, options ?: {
        accessToken ?: string,
        handleError ?: (err: HttpErrorResponse) => Observable<TResponse>
    }) {
        const headers  = new HttpHeaders();
        if(options?.accessToken) {
            headers.set('Authorization', `Bearer ${options?.accessToken}`);
        }
        return this
            .httpClient
            .post<TResponse>(getApiUrl(relativeUrl), data, { headers: headers })
            .pipe(
                catchError((x : HttpErrorResponse) => {
                    if(options?.handleError) {
                        return options.handleError(x);
                    } else {
                        return throwError(() => x);
                    }
                }),
                catchError((x : HttpErrorResponse) => {
                    this.messageService.showMessage(x.message, 5000);
                    return EMPTY;
                })
            );
    }
}

export function getApiUrl(relativeUrl: string) {
    let baseUrl = environment.apiBaseUrl;
    if(baseUrl.endsWith('/')) {
        baseUrl = baseUrl.substring(0, baseUrl.length - 1);
    }

    const trimmedRelativeUrl = relativeUrl.startsWith('/')
        ? relativeUrl.substring(1)
        : relativeUrl;

    return `${baseUrl}/${trimmedRelativeUrl}`
}
