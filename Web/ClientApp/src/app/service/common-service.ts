import { Injectable,  ElementRef, TemplateRef, ViewContainerRef } from "@angular/core";
import { Subject, Subscription, BehaviorSubject, interval } from "rxjs";
import { WindowService, WindowRef, WindowCloseResult } from '@progress/kendo-angular-dialog';
import { Router } from "@angular/router";
import { PopupService, PopupRef, Align } from '@progress/kendo-angular-popup';
import { Movie } from "../models/Movie";

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  public movieEditMode: Subject<boolean> = new Subject();
  subscriptionForItemEditModeOnCart: Subscription;
  public utilityName: Subject<string> = new BehaviorSubject("");
  subscriptionForUtilityName: Subscription;
  public location: Subject<any> = new BehaviorSubject(null);
  subscriptionForLocation: Subscription;
  refreshNextPickupTime = interval(60000);
  popupRef: PopupRef;

  constructor(private windowService: WindowService, private router: Router, private popUpService: PopupService) { }
    
  updateEditModeFlag(enabledOverlay): void {
    this.movieEditMode.next(enabledOverlay);
  }

  setLocalStorageItems(key, value): void {
    sessionStorage.setItem(key, value);
  }

  getLocalStorageItems(key): string {
    if (sessionStorage.getItem(key) !== null) {
      return sessionStorage.getItem(key);
    }
    return null;
  }

  removeLocalStorageItems(key): void {
    sessionStorage.removeItem(key);
  }

  setUtilityName(): string {
    var utility = this.getLocalStorageItems("utilityName");
    if (utility != null) {
      this.utilityName.next(utility.toLowerCase());
    }
    return "";
  }

  getUtilityName(): string {
    var utilityName = this.getLocalStorageItems("utilityName");
    if (utilityName != null) {
      return utilityName;//.replace(/[&']/g, "").toLowerCase();
    }
    return "";
  }
  
  openItemEditModal(component, item: Movie, titlebar): void {
    var height = 615;

    var header = `${item.name} - ($${item.salePrice})`;
    var windowRef = this.setWindowSettings(titlebar, header, component, height, () => {
      item.opened = false;
      this.updateEditModeFlag(item.opened);
    });

    item.opened = true;
    const windowDataObject = windowRef.content.instance;
    windowDataObject.item = item;
    this.updateEditModeFlag(item.opened);
  }
    
  setWindowSettings(titlebar, header, component, height, closeCallback: () => void): WindowRef {
    var windowSettings = {
      title: header,
      content: component,
      draggable: false,
      resizable: false,
      titleBarContent: titlebar,
      width: 625,
      height: height
    };

    if (window.innerWidth < 768) {
      windowSettings.width = window.innerWidth;
      delete windowSettings.height;
    }

    const windowRef = this.windowService.open(windowSettings);

    windowRef.result.subscribe((result) => {
      if (result instanceof WindowCloseResult) {
        closeCallback();
      }
    });

    return windowRef;
  }

  openPopUp(anchor: ElementRef, template: TemplateRef<any>, containerRef: ViewContainerRef, anchorAlign?: Align, popupAlign?: Align): void {
    anchorAlign = anchorAlign || { horizontal: 'right', vertical: 'bottom' };
    popupAlign = popupAlign || { horizontal: 'right', vertical: 'top' };
    this.closePopUp();
    this.popupRef = this.popUpService.open({
      anchor: anchor,
      appendTo: containerRef,
      content: template,
      anchorAlign: anchorAlign,
      popupAlign: popupAlign
    });
  }

  closePopUp(): void {
    if (this.popupRef) {
      this.popupRef.close();
      this.popupRef = null;
    }
  }
}
