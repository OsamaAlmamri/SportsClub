import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ServiceType} from "../models/service-type";

@Injectable({
  providedIn: 'root'
})
export class ServiceTypesService {

  private apiUrl = '/api/servicetype';

  constructor(private http: HttpClient) { }

  getServiceTypes(): Observable<ServiceType[]> {
    return this.http.get<ServiceType[]>(this.apiUrl+'/all');
  }
}
