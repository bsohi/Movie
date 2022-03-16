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

  paymentMethods: Array<string> = ["Cash", "Cheque", "Credit Card"];
  amount: number = 0;
  public card = "9999 9000 9900 0000";
  public expiryDate = "99/99";
  showCardText = false;
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

  get balance() {
    return this.total - this.amount;
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

  onAmountChange(value: number) {
    var strAmount = value.toFixed(2);
    this.amount = Number(strAmount);
    this.commonService.setLocalStorageItems("amount", this.amount);
  }

  public selectionChange(value: any): void {
    this.showCardText = false;
    if (value == "Credit Card") {
      this.showCardText = true;
    }
  }

  print() {
    window.print();
  }
  // and don't forget to unsubscribe
  ngOnDestroy() {
    //this.commonService.subscriptionForLocation.unsubscribe();
  }
}
