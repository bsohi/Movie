import { Component, OnInit, TemplateRef } from '@angular/core';
import { CartService } from "../service/cart-service";
import { CommonService } from "../service/common-service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  totalCount = 0;
  totalCost = 0;

  constructor(private cartService: CartService, private commonService: CommonService, private router: Router) { }

  updateCount() {
    this.totalCount = this.cartService.getItems().length;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  gotoMyCart(titlebar: TemplateRef<any>) {
    if (this.totalCount > 0) {
      this.router.navigate(['/cart']);
    }
  }

  async ngOnInit() {
    if (this.cartService.subscriptionForItemCount == undefined) {
      this.cartService.subscriptionForItemCount = this.cartService.itemCountChange.subscribe(
        newProdCount => {
          this.totalCount = newProdCount;
        }
      );
    }
    if (this.cartService.subscriptionForTotalCost == undefined) {
      this.cartService.subscriptionForTotalCost = this.cartService.totalCostChange.subscribe(
        newTotalCost => {
          this.totalCost = newTotalCost;
        }
      );
    }
  }
  // and don't forget to unsubscribe
  ngOnDestroy() {
    this.cartService.itemCountChange.unsubscribe();
    this.cartService.totalCostChange.unsubscribe();
  }
}
