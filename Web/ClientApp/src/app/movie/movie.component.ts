import { Component, Inject, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../models/Movie';
import { MovieApiService } from '../service/movie-api.service';
import { Router } from '@angular/router';

import { CommonService } from '../service/common-service';
import { CartService } from '../service/cart-service';
import { MovieCartComponent } from '../movie-cart/movie-cart.component';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './movie.component.html'
})
export class MovieComponent {
  public movies: GridDataResult;
  public originalMovies: Movie[];
  public skip = 0;
  public pageSize = 10;

  constructor(private movieApiService: MovieApiService, private route: Router, private commonService: CommonService, private cartService: CartService) {
    //this.cartService.clearCart();
    this.loadMovies(movieApiService);
  }

  async loadMovies(movieApiService: MovieApiService): Promise<void> {
    this.originalMovies = await movieApiService.ListMovies();
    this.loadItems();
  }

  gridMovieSelectionChange(selection) {
    const selectedData = selection.selectedRows[0].dataItem;
    this.route.navigate(["movie/edit", selectedData.id]);
  }

  public addClick(item: Movie, titlebar: TemplateRef<any>): void {
    item.uuid = null;
    this.commonService.openItemEditModal(MovieCartComponent, item, titlebar);
  }

  public pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.loadItems();
  }

  private loadItems(): void {
    this.movies = {
      data: this.originalMovies.slice(this.skip, this.skip + this.pageSize),
      total: this.originalMovies.length,
    };
  }
}
