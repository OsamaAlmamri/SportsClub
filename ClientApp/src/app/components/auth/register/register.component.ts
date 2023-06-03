import { Component ,ViewChild} from '@angular/core';
import { AuthService } from '../../../services/auth-service.service';
import {NgForm} from "@angular/forms";
import {AddServiceForm} from "../../../models/service-form";
import {RegisterModal} from "../../../models/register-modal";
import { Router } from '@angular/router';
import {LoginModal} from "../../../models/login-modal";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  @ViewChild('registerForm', { static: true }) registerForm: NgForm;

  registerData: RegisterModal={
    UserName:"",
    Email:"",
    PhoneNumber:"",
    Password:"",
    ConfirmPassword:"",
    FullName:"",
  };

  constructor(private authService: AuthService,private router: Router) { }

  register(): void {
    if (this.registerForm.invalid) {
      return;
    }



    this.authService.register(this.registerData).subscribe(
      response => {
        const token = response.token;
        if (token) {
          localStorage.setItem('jwtToken', token);
          window.localStorage.setItem("userData", JSON.stringify(response));
          this.router.navigate(['/profile']);
          // Handle successful login (e.g., navigate to a different page)
        }
      },
      error => {
        // Handle registration error
      }
    );
  }
}
