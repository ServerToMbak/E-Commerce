import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss']
})
export class CheckoutReviewComponent implements OnInit {
  @Input() appStepper?: CdkStepper;
  basket$: Observable<IBasket>;

  constructor(private basketSsrvice:BasketService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.basket$ = this.basketSsrvice.basket$;
  }
  createPayentIntent(){
    return this.basketSsrvice.createPaymentIntent().subscribe((response:any) =>{
      this.toastr.success('Paymen Intent Created');
      this.appStepper?.next();
    }, error =>{
      console.log(error);
      this.toastr.error(error.message);
    })
  }

}
