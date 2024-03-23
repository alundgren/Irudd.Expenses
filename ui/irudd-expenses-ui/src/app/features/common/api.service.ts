import { Injectable, inject } from '@angular/core';
import { environment } from "../../../environments/environment.development";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MessageService } from './message.service';
import { EMPTY, Observable, catchError, from, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ApiService {
    constructor(private messageService: MessageService, private httpClient: HttpClient) {

    }

    postWithoutAccessToken<T>(relativeUrl: string, data: any) {
        return this.httpClient.post<T>(getApiUrl(relativeUrl), data)
        .pipe(
            catchError((x : HttpErrorResponse) => {
                this.messageService.showMessage(x.message, 5000);
                return EMPTY;
            }));
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
