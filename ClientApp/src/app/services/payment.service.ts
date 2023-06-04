import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { interval, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import {Service} from "../models/service";
import {AuthService} from "./auth-service.service";

@Injectable({
  providedIn: 'root'
})
export class PaymentService {



  private apiUrl = '/api/payment';
  constructor(private http: HttpClient,public authService: AuthService) { }


  createPayment(cartItems :Service[]): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/create`, {Services:cartItems},{headers:this.authService.getAuthHeader()});
  }

  checkPaymentStatus(paymentId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/status/${paymentId}`,{headers:this.authService.getAuthHeader()});
  }
}
