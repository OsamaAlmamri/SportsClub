import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PaymentService } from '../services/payment.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent {
  approvalUrl: string="";
  paymentId: string="";
  type: string | null;
  paymentComplete = false;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private paymentService: PaymentService, private router: Router, private route: ActivatedRoute) {
    this.type = route.snapshot.paramMap.get('type');
    this.approvalUrl = baseUrl;
  }




  initiatePayment() {
    this.paymentService.createPayment().subscribe(result => {
      this.paymentId = result.paymentId;
      window.open(result.approvalUrl, '_self');

      this.checkPaymentStatus();
    }, (error) => {                              //Error callback
      console.error(error)
      console.error('error caught in component')

    });
  }

  checkPaymentStatus() {
    this.paymentService.checkPaymentStatus(this.paymentId).subscribe(result => {
      if (['approved', 'refunded'].includes(result.paymentStatus) ) {
        this.paymentComplete = true;
        console.log("Perform additional actions or display a success message")
        // Perform additional actions or display a success message
      } else if (['created', 'pending','in_progress'].includes(result.paymentStatus) ) {
        // Payment is still pending, continue checking
        console.log("Payment is still pending, continue checking")
        setTimeout(() => {
          this.checkPaymentStatus();
        }, 5000); // Check every 5 seconds
      } else {
        console.log("Payment failed or unknown status")
        // Payment failed or unknown status
        // Handle error case or display appropriate message
      }
    });
  }
 /* initiatePayment() {
    console.log("url");
    this.http.post<any>('/api/payment', {}).subscribe(url => {
      this.approvalUrl = url.href;
      console.log(url);
     window.open(this.approvalUrl, '_blank');
    }, (error) => {                              //Error callback
      console.error(error)
      console.error('error caught in component')
     
    });

    this.paymentService.checkPaymentStatus(this.paymentId).subscribe(status => {
      if (status === 'approved') {
        // Payment is complete
        this.paymentComplete = true;
        console.log("sssssssssssssssssss")
        // Perform additional actions or display a success message
      }
    })
  }*/

}

