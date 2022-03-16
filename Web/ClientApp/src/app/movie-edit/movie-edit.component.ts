import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';

import { item, Movie } from '../models/Movie';
import { MovieApiService } from '../service/movie-api.service';
import { CommonService } from '../service/common-service';

@Component({
  selector: 'movie-edit',
  templateUrl: './movie-edit.component.html'
})
export class MovieEditComponent implements OnInit {
  public movieModel: Movie;
  movieForm: FormGroup;
  id: number;

  errorMessages: string[];
  isLoading: boolean = false;
  changePassword: boolean = false;
  genres: Array<item>;

  private propertiesToUpdate: string[] = ["name", "cost", "salePrice", "genreId"];

  constructor(private router: Router, private route: ActivatedRoute, private movieApiService: MovieApiService,
    private formBuilder: FormBuilder, private commonService: CommonService  ) { }

  ngOnInit() {
    this.movieModel = new Movie();
    this.movieForm = this.formBuilder.group(this.movieModel);
    this.route.params.subscribe(params => {
      this.id = params["id"] || 0;
      this.commonService.updateEditModeFlag(true);
      this.movieApiService.GetMovie(this.id).then(result => {
        this.movieModel = result;
        this.movieForm = this.formBuilder.group(
          {
            name: result.name,
            cost: result.cost,
            salePrice: result.salePrice,
            genreId: result.genreId == 0 ? null : result.genreId
          });
        this.genres = result.listValues.genre;
      });
    });
    this.commonService.updateEditModeFlag(false);
  }

  async submitForm(): Promise<void> {
    this.commonService.updateEditModeFlag(true);
    this.movieForm.value.id = this.id;
    var result = await this.movieApiService.UpdateMovie({ ...this.movieForm.value, propertiesToUpdate: this.propertiesToUpdate });

    this.commonService.updateEditModeFlag(false);
    if (result.success) {
      this.goBack();
    }
    else {
      this.errorMessages = result.errorMessages;
    }
  }

  goBack(): void {
    this.router.navigate(["movie"]);
  }
}
