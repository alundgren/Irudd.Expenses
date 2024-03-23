import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class MessageService {
    private readonly messageSub = new Subject<{ message: string, timeShownInMilliseconds: number }>();

    constructor() {

    }

    message$ = this.messageSub.asObservable();

    showMessage(message: string, timeShownInMilliseconds: number) {
        this.messageSub.next({ message, timeShownInMilliseconds: timeShownInMilliseconds });
    }
}
