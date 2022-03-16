import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Movie } from '../models/Movie';
import { CartService } from '../service/cart-service';
import { CommonService } from '../service/common-service';
import { MovieCartComponent } from '../movie-cart/movie-cart.component';
import { IntlService } from '@progress/kendo-angular-intl';
import { Router } from '@angular/router';

import * as $ from 'jquery';

@Component({
  selector: 'cart',
  templateUrl: './cart.component.html'
})
export class CartComponent {
  @Input() public items: Movie[];
  
  constructor(private cartService: CartService, private commonService: CommonService, private intl: IntlService, private router: Router) {
    this.items = this.cartService.getItems();
  }

  show() {
    return this.items.length > 0;
  }

  get subtotal() {
    return this.cartService.getSubTotal();
  }

  get hst() {
    return this.cartService.getHST();
  }

  get total() {
    return this.cartService.getTotal();
  }

  isAddOnOptionNumber(option) {
    return !isNaN(option);
  }
    
  updateQuantity(value: number, item) {
    item.quantity = value;
    this.cartService.addToCart(item);
  }

  editItem(item, titlebar: TemplateRef<any>) {
    this.commonService.openItemEditModal(MovieCartComponent, item, titlebar);
  }

  removeItem(item) {
    this.items = this.cartService.removeItem(item);
    if (this.items.length == 0) {
      this.router.navigate(['/movie']);
    }
  }
  
  // and don't forget to unsubscribe
  ngOnDestroy() {
    //this.commonService.subscriptionForLocation.unsubscribe();
  }
}
