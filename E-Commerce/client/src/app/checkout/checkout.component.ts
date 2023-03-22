import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, subscribeOn } from 'rxjs';
import { AccontService } from '../account/accont.service';
import { BasketService } from '../basket/basket.service';
import { IBasketTotal } from '../shared/models/basket';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  basketTotal$: Observable<IBasketTotal>;
  checkoutForm: FormGroup;

  constructor(private fb: FormBuilder, private accountService:AccontService,private basketService:BasketService) { }

  ngOnInit(): void {
    this.createCheckoutForm();
    this.getAddressFormValues();
    this.getDeliveryMethodValue();
    this.basketTotal$= this.basketService.basketTotal$;
  }
  createCheckoutForm(){
    this.checkoutForm=this.fb.group({
      addressForm: this.fb.group({
        firstName: [null,Validators.required],
        lastName: [null,Validators.required],
        street: [null,Validators.required],
        city: [null,Validators.required],
        state: [null,Validators.required],
        zipCode: [null,Validators.required],

      }),
      deliveryForm : this.fb.group({
        deliveryMethod: [null,Validators.required]
      }),
      paymentForm: this.fb.group({
        nameOnCard : [null, Validators.required]
      }),
    })
  }

  getAddressFormValues(){
    this.accountService.getUserAddress().subscribe(address=> {
      if(address)
      {
        this.checkoutForm.get('addressForm').patchValue(address);
      }
    }, error=>{
      console.log(error)
    }
    );
  }
  getDeliveryMethodValue(){
    const basket = this.basketService.getCurrentBasketValue();

    if(basket.deliveryMethodId !== null){
      this.checkoutForm.get('deliveryForm').get('deliveryMethod').patchValue(basket.deliveryMethodId.toString())

    }
  }

}
