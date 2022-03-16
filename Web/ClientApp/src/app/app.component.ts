import { Component, OnInit } from '@angular/core';
import { CommonService } from "./service/common-service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';
  isEnabled: boolean = false;

  constructor(private commonService: CommonService) {
  }

  async ngOnInit() {
    if (this.commonService.subscriptionForItemEditModeOnCart == undefined) {
      this.commonService.subscriptionForItemEditModeOnCart = this.commonService.movieEditMode.subscribe(
        (editModeEnabled) => (this.isEnabled = editModeEnabled)
      );
    }
  }
}
