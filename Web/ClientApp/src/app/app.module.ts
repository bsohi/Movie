import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { MovieComponent } from './movie/movie.component';
import { MovieEditComponent } from './movie-edit/movie-edit.component';
import { MovieCartComponent } from './movie-cart/movie-cart.component';
import { CartComponent } from './cart/cart.component';

import { BaseApiService } from './service/base-api-service';
import { MovieApiService } from './service/movie-api.service';

import { GridModule } from '@progress/kendo-angular-grid';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { WindowModule } from '@progress/kendo-angular-dialog';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    MovieComponent,
    MovieEditComponent,
    MovieCartComponent,
    CartComponent
  ],
  entryComponents: [MovieCartComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    GridModule,
    ReactiveFormsModule,
    LayoutModule,
    LabelModule,
    InputsModule,
    DropDownsModule,
    ButtonsModule,
    BrowserAnimationsModule, WindowModule,
    RouterModule.forRoot([
      /*{ path: '', component: HomeComponent, pathMatch: 'full' },*/
      { path: 'cart', component: CartComponent },
      {
        path: 'movie', children: [
          { path: '', component: MovieComponent },
          { path: 'create', component: MovieEditComponent },
          { path: 'edit/:id', component: MovieEditComponent },
        ]
      }
    ])
  ],
  providers: [MovieApiService, BaseApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
