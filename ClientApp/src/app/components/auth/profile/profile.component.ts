import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../../services/auth-service.service";
import {UserSubscribtionService} from "../../../services/user-subscribtion.service";


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: any; // Placeholder for user data
  showServices: true; // Placeholder for user data
  services: any[] = [];
  constructor(private userSubscriptionService: UserSubscribtionService,private authService: AuthService) {
    this.user = this.authService.getUser(); // Fetch the authenticated user data
  }
  ngOnInit(): void {
    this.SearchServices();
  }

  SearchServices(): void {


    this.userSubscriptionService.getUserServices(this.user.id).subscribe(data => {
      this.services = data;
    });
  }
}
