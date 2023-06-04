import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ServiceTime} from "../models/service-time";
import {User} from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class UserSubscribtionService {


  private apiUrl =  '/api/UserSubscriptionsServices';

  constructor(private http: HttpClient) { }

  getUserServices(id:string): Observable<any[]> {

  return   this.http.get<User[]>(this.apiUrl, { params: { id: id } });
  }
}
