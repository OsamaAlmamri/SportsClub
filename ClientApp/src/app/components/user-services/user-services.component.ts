import {Component, Input} from '@angular/core';
import {UserSubscribtionService} from "../../services/user-subscribtion.service";

@Component({
  selector: 'app-user-services',
  templateUrl: './user-services.component.html',
  styleUrls: ['./user-services.component.css']
})
export class UserServicesComponent {
  @Input() userServices: any[] = [];
  @Input() show: boolean = false;

  constructor(private userSubscriptionService: UserSubscribtionService) {
  }

  isEndDate(date: string) {
    var varDate = new Date(date); //dd-mm-YYYY
    var today = new Date();

    return varDate < today
  }

  isInFutureDate(date: string) {
    var varDate = new Date(date); //dd-mm-YYYY
    var today = new Date();

    return varDate > today
  }

  isInTime(fromTime: string, toTime: string) {


    const currentTime = new Date(); // Get the current time

    const startTime = new Date();
    startTime.setHours(parseInt(fromTime.split(":")[0]));
    startTime.setMinutes(parseInt(fromTime.split(":")[1]));
     // Set Before start time by 30 m
    const newCurrentTime = new Date(currentTime.getTime()+ (30 * 60000));
    const endTime = new Date();
    endTime.setHours(parseInt(toTime.split(":")[0]));
    endTime.setMinutes(parseInt(toTime.split(":")[1]));

    return (
      // currentTime is less than end time
      currentTime < endTime &&
      // to allow enter before time by 30 m
      newCurrentTime > startTime );
  }

  isHaveTime() {
    var haveTime = false;
    for (var i = 0; i < this.userServices.length; i++) {

      if (!this.isInFutureDate(this.userServices[i].startAt) &&
        !this.isEndDate(this.userServices[i].endAt) && this.userServices[i].endAt != null) {

        if (this.isInTime(this.userServices[i].service.fromTime ,this.userServices[i].service.toTime) )
          haveTime = true;
        break;
      }
    }
    return haveTime
  }

  isHaveDate() {
    var haveDate = false;
    for (var i = 0; i < this.userServices.length; i++) {

      if (!this.isInFutureDate(this.userServices[i].startAt) &&
        !this.isEndDate(this.userServices[i].endAt) && this.userServices[i].endAt != null) {
        haveDate = true;
        break;
      }
    }
    return haveDate
  }


  saveStartTime(dateEvent: any, id: any) {
    if (confirm("do you want save start date ")) {
      const itemIndex = this.userServices.findIndex(item => item.id === id);
      if (itemIndex !== -1) {
        this.userServices[itemIndex].startAt = dateEvent.target.value;
        this.userSubscriptionService.updateService(this.userServices[itemIndex], id).subscribe(data => {
          this.userServices[itemIndex] = data;
        });


      }
    }
  }


}
