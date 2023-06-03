import { Component, EventEmitter, Input, Output } from '@angular/core';
import {User} from "../../models/user";


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent {
  @Input() user: User;
  subscriptionServices: any[] = [];
  @Output() onSearchServices: EventEmitter<string> = new EventEmitter();
  constructor() { }

  getUserSubscriptionServices(userId: string): void {
    console.log(userId);
    this.onSearchServices.emit(userId);
  }
}
