import { Component, OnInit } from '@angular/core';
import { CartService } from '../service/cart-service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  constructor(private cartService: CartService) {
  }
    ngOnInit(): void {
      this.cartService.clearCart();
    }
}
