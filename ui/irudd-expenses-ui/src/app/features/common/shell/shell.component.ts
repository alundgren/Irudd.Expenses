import { Component, signal } from '@angular/core';
import { MessageService } from '../message.service';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.scss'
})
export class ShellComponent {
    constructor(messageService: MessageService) {
        this.messageSub = messageService.message$.subscribe(x => {
            if(!x) {
                return;
            }
            let m = { shownTime: Date.now(), text: x.message };
            this.message.set(m);
            setTimeout(() => {
                //Hide the message after timeShownInMilliseconds unless it's been replaced by a newer message.
                let currentM = this.message();
                if(currentM?.shownTime === m.shownTime) {
                    this.message.set(null);
                }
            }, x.timeShownInMilliseconds)
        });
    }

    message = signal<{ shownTime: number, text: string} | null>(null);

    private messageSub : Subscription;

    ngOnDestroy() {
        this.messageSub.unsubscribe();
    }
}
