import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment.development";

@Injectable({
    providedIn: 'root'
})
export class ApiService {
    constructor() {

    }

    private getApiUrl(relativeUrl: string) {
        let baseUrl = environment.apiBaseUrl;
        if(baseUrl.endsWith('/')) {
            baseUrl = baseUrl.substring(0, baseUrl.length - 1);
        }

        const trimmedRelativeUrl = relativeUrl.startsWith('/')
            ? relativeUrl.substring(1)
            : relativeUrl;

        return `${baseUrl}/${trimmedRelativeUrl}`
    }
}
