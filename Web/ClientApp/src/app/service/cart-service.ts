import { Injectable } from '@angular/core';
import { Movie } from '../models/Movie';
import { Subject, Subscription, BehaviorSubject } from 'rxjs';
import { CommonService } from '../service/common-service';
import { v1 as uuid } from 'uuid';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  items: Movie[] = [];
  itemQuantity: number;
  subTotal: number;
  
  public itemCountChange: Subject<number> = new Subject();
  public totalCostChange: Subject<number> = new Subject();
  public itemsSubscription: Subject<any> = new BehaviorSubject(this.items);

  subscriptionForItems: Subscription;
  subscriptionForItemCount: Subscription;
  subscriptionForTotalCost: Subscription;
    
  constructor(private commonService: CommonService) {
  }

  addItemToCart(item: Movie, quantity): { success } {
    item.finalPrice = (item.salePrice * quantity) * 1.13;
    item.quantity = quantity;
    this.addToCart(item);
    return { success: true };    
  }
  
  addToCart(item: Movie) {
    this.getLocalStorageItems();
    const itemExistInCart = this.items.find(({ uuid, name, id }) =>
      (item.uuid === uuid) || ((name === item.name) && (id == item.id))
    ); // find product by name and addons
    if (!itemExistInCart) {
      item.uuid = uuid();
      this.items.push(item);
    }
    else {
      itemExistInCart.quantity = item.quantity;
      itemExistInCart.finalPrice = item.finalPrice;
    }    
    this.setLocalStorageItems();
    this.updateCount();
  }

  removeItem(item) {
    this.getLocalStorageItems();
    this.items = this.items.filter(({ uuid }) => uuid !== item.uuid);
    this.setLocalStorageItems();
    this.updateCount();
    return this.items;
  }

  getItems() {
    this.getLocalStorageItems();
    this.updateCount();
    return this.items;
  }

  clearCart() {
    this.removeLocalStorageItems();
    this.items = [];
    this.updateCount();
    return this.items;
  }

  updateItemsSubscription(): void {
    this.itemsSubscription.next(this.items);
  }

  updateCount(): void {
    this.getLocalStorageItems();
    if (this.isArrayEmpty()) {
      this.itemCountChange.next(0);
      this.totalCostChange.next(0);
      this.updateItemsSubscription();
      return;
    }
    this.itemQuantity = this.items.reduce((acc, item) => acc += item.quantity, 0)
    this.itemCountChange.next(this.itemQuantity);
    this.getSubTotal();
    this.totalCostChange.next(this.getTotal());
    //refresh itesm wherever it is subscribed
    this.updateItemsSubscription();
  }

  getSubTotal() {
    if (this.isArrayEmpty()) { this.subTotal = 0; return 0; }
    var internalSubTotal = this.items.map(a => a.salePrice * a.quantity).reduce(function (a, b) {
      return a + b;
    });
    this.subTotal = parseFloat(internalSubTotal.toFixed(2))
    return this.subTotal;
  }

  getHST() {
    var hst = this.subTotal * .13;
    return parseFloat(hst.toFixed(2));
  }

  getTotal() {
    var tipAmount = 0;
    if (this.commonService.getLocalStorageItems("tipAmount")) {
      tipAmount = Number(this.commonService.getLocalStorageItems("tipAmount"));
    }
    return this.subTotal + this.getHST() + tipAmount;
  }

  private isArrayEmpty() {
    return this.items.length == 0;
  }

  private setLocalStorageItems() {
    this.commonService.setLocalStorageItems('cartItems', JSON.stringify(this.items));
  }

  private getLocalStorageItems() {
    var value = this.commonService.getLocalStorageItems('cartItems');
    if (value !== null) {
      this.items = JSON.parse(value);
    }
  }

  private removeLocalStorageItems() {
    this.commonService.removeLocalStorageItems('cartItems');
  }
}
