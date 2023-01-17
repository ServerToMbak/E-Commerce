import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IDeliveryMethod } from '../shared/models/deliveryMethods';
import { IOrderToCreate } from '../shared/models/Order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }

  createOrder(order:IOrderToCreate){
    return this.http.post(this.baseUrl +'orders', order);
  }

  getDeliveryMethod(){
    return this.http.get(this.baseUrl + 'orders/deliveryMethods').pipe(
      map((dm: IDeliveryMethod[]) =>{
        return dm.sort((a,b) => b.price - a.price);
      })
    );
  }
}
