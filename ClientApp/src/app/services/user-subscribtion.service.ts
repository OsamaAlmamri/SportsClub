import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ServiceTime} from "../models/service-time";
import {User} from "../models/user";
import {AddServiceForm} from "../models/service-form";
import {Service} from "../models/service";

@Injectable({
  providedIn: 'root'
})
export class UserSubscribtionService {


  private apiUrl =  '/api/UserSubscriptionsServices';

  constructor(private http: HttpClient) { }

  getUserServices(id:string): Observable<any[]> {

  return   this.http.get<User[]>(this.apiUrl, { params: { id: id } });
  }

  updateService(service: any,id:number): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.patch<Service>(url, service);
  }
}
