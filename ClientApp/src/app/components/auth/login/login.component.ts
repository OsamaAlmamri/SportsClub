import {Component, ViewChild} from '@angular/core';
import { AuthService } from '../../../services/auth-service.service';
import {Router} from "@angular/router";
import {LoginModal} from "../../../models/login-modal";
import {NgForm} from "@angular/forms";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  @ViewChild('registerForm', { static: true }) registerForm: NgForm;

  loginModal: LoginModal={

    Email:"",

    Password:"",

  };
  constructor(private authService: AuthService,private router: Router) { }

  login(): void {
    if (this.registerForm.invalid) {
      return;
    }


    this.authService.login(this.loginModal).subscribe(
      response => {
        const token = response.token;
        if (token) {
          localStorage.setItem('jwtToken', token);
          window.localStorage.setItem("userData", JSON.stringify(response));
          // Handle successful login (e.g., navigate to a different page)
          this.router.navigate(['/profile']);
        }
      },
      error => {
        // Handle login error
      }
    );
  }
}
