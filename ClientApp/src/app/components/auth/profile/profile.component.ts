import { Component } from '@angular/core';
import {AuthService} from "../../../services/auth-service.service";


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  user: any; // Placeholder for user data

  constructor(private authService: AuthService) {
    this.user = this.authService.getUser(); // Fetch the authenticated user data
  }
}
