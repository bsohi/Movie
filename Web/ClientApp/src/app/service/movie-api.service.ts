import { Injectable, Inject } from '@angular/core';
import { Movie } from '../models/Movie';
import { ApiResponse } from '../models/ApiResponse';
import { BaseApiService } from './base-api-service';

@Injectable({
  providedIn: 'root'
})
export class MovieApiService {
  constructor(private baseApiService: BaseApiService) { }

  async ListMovies(): Promise<Movie[]> {
    var result = await this.baseApiService.get<ApiResponse<Movie[]>>('Movie/ListMovies/' || "");
    return result.content;
  }

  async GetMovie(movieId: number): Promise<Movie> {
    var result = await this.baseApiService.get<ApiResponse<Movie>>('Movie/Get/' + movieId || "");
    return result.content;
  }

  async UpdateMovie(model: Movie): Promise<ApiResponse<boolean>> {
    var result = await this.baseApiService.post<Movie, ApiResponse<boolean>>("Movie/Post", model);
    return result;
  }
}
