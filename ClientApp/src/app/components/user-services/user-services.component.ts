import { Component, Input } from '@angular/core';
import {UserSubscribtionService} from "../../services/user-subscribtion.service";

@Component({
  selector: 'app-user-services',
  templateUrl: './user-services.component.html',
  styleUrls: ['./user-services.component.css']
})
export class UserServicesComponent {
  @Input() userServices: any[] = [];
  @Input() show: boolean = false;
  constructor(private userSubscriptionService: UserSubscribtionService){}
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


  saveStartTime(dateEvent:any,id: any) {
   if (confirm("do you want save start date "))
   {
     const itemIndex = this.userServices.findIndex(item => item.id === id);
     if (itemIndex !== -1) {
       this.userServices[itemIndex].startAt=dateEvent.target.value;
       this.userSubscriptionService.updateService(this.userServices[itemIndex],id).subscribe(data => {
         this.userServices[itemIndex]=data;
       });


     }
   }
  }


}
