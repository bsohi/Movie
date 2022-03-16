import { Component, Input, OnInit } from '@angular/core';
import { CartService } from '../service/cart-service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { WindowRef } from '@progress/kendo-angular-dialog';

import { Movie } from '../models/Movie';

import * as $ from 'jquery';

@Component({
  selector: 'movie-cart',
  templateUrl: './movie-cart.component.html'
})
export class MovieCartComponent implements OnInit {
  @Input() public item: Movie;
  quantity: number;
  
  constructor(private cartService: CartService, public window: WindowRef, private formBuilder: FormBuilder) {
    this.quantity = 1;//this.item.Quantity;
  }

  addItemToCart() {
    var output = this.cartService.addItemToCart(this.item, this.quantity);
    if (output.success) {
      this.window.close();
    } else {
      $('div.required-field-message').attr("tabindex", -1).focus();
    }
  }

  updateQuantity(value: number, quantityInput: any) {
    if (value > 0 && value <= 50) {
      this.quantity = value;
    } else {
      this.quantity = 1;
      quantityInput.value = 1;
    }
  }

  increaseDecreaseQuantity(value: number) {
    if (this.quantity + value > 0 && this.quantity + value <= 50) {
      this.quantity += value;
    }
  }

  ngOnInit() {
    if (this.item.quantity) {
      this.quantity = this.item.quantity;
    }
  }
}
