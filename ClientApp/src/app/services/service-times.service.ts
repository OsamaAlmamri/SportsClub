import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ServiceTime} from "../models/service-time";

@Injectable({
  providedIn: 'root'
})
export class ServiceTimesService {

  private apiUrl = '/api/servicetime';

  constructor(private http: HttpClient) { }

  getServiceTimes(): Observable<ServiceTime[]> {
    return this.http.get<ServiceTime[]>(this.apiUrl+'/all');
  }
}
