import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Service } from '../models/service';
import {AddServiceForm} from "../models/service-form";

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  private apiUrl = '/api/service';

  constructor(private http: HttpClient) { }

  getServices(): Observable<Service[]> {
    return this.http.get<Service[]>(this.apiUrl+'/all');
  }

  getService(id: number): Observable<Service> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Service>(url);
  }

  addService(service: AddServiceForm): Observable<Service> {
    return this.http.post<Service>(this.apiUrl, service);
  }

  updateService(service: AddServiceForm,id:number): Observable<Service> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.patch<Service>(url, service);
  }

  deleteService(id: number): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete(url);
  }



  removeServiceFromList(service: Service): void {
    //this.services = this.services.filter(s => s.id !== service.id);
  }
}
