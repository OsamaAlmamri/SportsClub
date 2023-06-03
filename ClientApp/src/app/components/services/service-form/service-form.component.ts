import { Component,ViewChild, EventEmitter, Input, Output } from '@angular/core';

import { Service } from '../../../models/service';
import { NgForm } from '@angular/forms';

import { ServiceService } from '../../../services/service.service';
import {AddServiceForm} from "../../../models/service-form";
import {ServiceTime} from "../../../models/service-time";
import {ServiceType} from "../../../models/service-type";
import {ServiceTimesService} from "../../../services/service-times.service";
import {ServiceTypesService} from "../../../services/service-types.service";

@Component({
  selector: 'app-service-form',
  templateUrl: './service-form.component.html',
  styleUrls: ['./service-form.component.css']
})
export class ServiceFormComponent {
  @Input() editId=0;
  @Input() service: AddServiceForm;
  @Output() onServiceSaved: EventEmitter<Service> = new EventEmitter();
  @Output() onServiceUpdated: EventEmitter<Service> = new EventEmitter();
  serviceTypes: ServiceType[];
  serviceTimes: ServiceTime[];
  @ViewChild('serviceForm', { static: true }) serviceForm: NgForm;

  constructor(private serviceTimesService: ServiceTimesService,private serviceTypesService: ServiceTypesService,private serviceService: ServiceService) { }

  ngOnInit(): void {

    this.getServiceTypes();
    this.getServiceTimes();
  }

  getServiceTypes(): void {
    this.serviceTypesService.getServiceTypes()
      .subscribe(types => this.serviceTypes = types);
  }
  getServiceTimes(): void {
    this.serviceTimesService.getServiceTimes()
      .subscribe(times => this.serviceTimes = times);
  }
  saveService(): void {
    // Check if the form is valid before saving the service
    if (this.serviceForm.invalid) {
      return;
    }

    if (this.editId>0)
    {
      this.serviceService.updateService(this.service,this.editId)
        .subscribe(
          (result:Service ) => {
            this.onServiceUpdated.emit(result);
            console.log('Service saved successfully.');

          },
          error => {
            console.log('Error saving service:', error);
          }
        );
    }
    else
    {
      this.serviceService.addService(this.service)
        .subscribe(
          (result:Service ) => {
            this.onServiceSaved.emit(result);
            console.log('Service saved successfully.');
          },
          error => {
            console.log('Error saving service:', error);
          }
        );
    }
  }


  // saveService(): void {
  //   if (this.service.id) {
  //     this.serviceService.updateService(this.service)
  //       .subscribe(() => {
  //         // Handle success or error
  //       });
  //   } else {
  //     this.serviceService.addService(this.service)
  //       .subscribe(() => {
  //         // Handle success or error
  //       });
  //   }
  // }
}
