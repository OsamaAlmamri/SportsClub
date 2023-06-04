import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {RegisterModal} from "../models/register-modal";
import {LoginModal} from "../models/login-modal";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = '/api/auth'; // Replace with your API endpoint

  constructor(private http: HttpClient) { }

  login(loginModal: LoginModal): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, loginModal);
  }

  register(registerData: RegisterModal): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, registerData);
  }
  isAuthenticated(): boolean {
    const jwtToken = localStorage.getItem("jwtToken");
    return !!jwtToken;
  }
  getAuthHeader(): HttpHeaders {
    const jwtToken = localStorage.getItem("jwtToken");
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${jwtToken}`
    });

    return headers ;

  }

  getUser(): any {
    const userData = localStorage.getItem("userData");

    if (userData) {
      return JSON.parse(userData);
    } else {
      return null;
    }
  }



  logout(): void {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('userData');
    // Handle logout logic (e.g., navigate to the login page)
  }
}
