import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-user-services',
  templateUrl: './user-services.component.html',
  styleUrls: ['./user-services.component.css']
})
export class UserServicesComponent {
  @Input() userServices: any[] = [];
  @Input() show: boolean = false;

  isEndDate(date: string) {
    var varDate = new Date(date); //dd-mm-YYYY
    var today = new Date();

    return varDate < today
  }
  
  isHaveTime() {
    var haveTime = false;
    for (var i = 0; i < this.userServices.length; i++) {
      if (!this.isEndDate(this.userServices[i].endAt)) {
        haveTime = true;
      }

    }
  

    return haveTime
  }



}
