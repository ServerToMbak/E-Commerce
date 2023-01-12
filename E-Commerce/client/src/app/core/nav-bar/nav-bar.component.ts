import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccontService } from 'src/app/account/accont.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>
  currentUser$:Observable<IUser>;

  constructor(private basketService:BasketService, private accountService:AccontService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.accountService.currentUser$;
  }
  logout(){
    this.accountService.logout();
  }
}
