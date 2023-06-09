import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User} from "../../models/user";
import {ServiceTimesService} from "../../services/service-times.service";
import {UserSubscribtionService} from "../../services/user-subscribtion.service";


@Component({
  selector: 'app-user-search',
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.css']
})
export class UserSearchComponent {
  searchTerm: string = '';
  users: User[] = [];
  showServices: boolean = false;
  userServices: any[] = [];
  searchPerformed: boolean = false;

  constructor(private userSubscriptionService: UserSubscribtionService,private http: HttpClient) { }

  searchUsers(): void {
    this.searchPerformed = true;
    this.showServices = false;

    if (this.searchTerm.trim() === '') {
      this.users = [];
      return;
    }

    const apiUrl = '/api/users/search'; // Replace with your ASP.NET endpoint URL

    this.http.get<User[]>(apiUrl, { params: { searchTerm: this.searchTerm } })
      .subscribe(data => {
        this.users = data;
      });
  }
  SearchServices(UserID: any): void {
    this.searchPerformed = true;
    this.showServices = false;
    this.userServices=[]
    console.log(UserID);
    if (this.searchTerm.trim() === '') {
      this.users = [];
      return;
    }

    this.userSubscriptionService.getUserServices(UserID).subscribe(data => {
        this.userServices = data;
        this.showServices = true;
      });
  }
}
