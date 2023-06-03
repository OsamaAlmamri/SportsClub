import {Component, OnInit} from '@angular/core';
import {Service} from '../../../models/service';
import {ServiceService} from '../../../services/service.service';
import {AddServiceForm} from "../../../models/service-form";
import {ServiceTime} from "../../../models/service-time";
import {ServiceType} from "../../../models/service-type";
import {ServiceTypesService} from "../../../services/service-types.service";
import {ServiceTimesService} from "../../../services/service-times.service";

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.css']
})
export class ServiceListComponent implements OnInit {
  services: Service[];

  editableService: AddServiceForm={

    Name:"",
    Description:"",
    Period:30,
    Price:100,
    ServiceTimeId:null,
    ServiceTypeId:null,
  };
  showModal = false;
  editId = 0;
  constructor(private serviceService: ServiceService) {
  }

  ngOnInit(): void {
    this.getServices();
  }

  getServices(): void {
    this.serviceService.getServices()
      .subscribe(services => this.services = services);
  }

  openModal(): void {
    this.editId=0;

    this.editableService={

      Name:"",
      Description:"",
      Period:30,
      Price:100,
      ServiceTimeId:null,
      ServiceTypeId:null,
    };

    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  editService(service: Service): void {
    this.editId=service.id;
    this.showModal=true;
    this.editableService.Name=service.name;
    this.editableService.ServiceTimeId=service.serviceTimeId;
    this.editableService.ServiceTypeId=service.serviceTypeId;
    this.editableService.Period=service.period;
    this.editableService.Price=service.price;
    this.editableService.Description=service.description;
    // Assuming you have a method to handle the edit functionality in a separate component
    // Pass the service object to the edit method or navigate to the edit component with the service ID
    // Example: this.router.navigate(['/services/edit', service.id]);
  }

  addServiceToList(service: Service): void {
   this.services.unshift(service);
   this.showModal=false;
  }

  updateServiceInList(service: Service): void {

    for (let i = 0; i < this.services.length; i++) {
      if (this.services[i].id === service.id) {
        this.services[i] = service;
        break;
      }
    }

   this.showModal=false;
  }

  deleteService(service: Service): void {
    if (confirm('Are you sure you want to delete this service?')) {
      this.serviceService.deleteService(service.id)
        .subscribe(() => {
          this.services = this.services.filter(s => s !== service);
        });
    }
  }
}
