import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { interval, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {



  private apiUrl = '/api/payment';
  constructor(private http: HttpClient) { }

 
  createPayment(): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/create`, {});
  }

  checkPaymentStatus(paymentId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/status/${paymentId}`);
  }
}
