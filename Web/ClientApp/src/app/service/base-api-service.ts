import { HttpClient } from "@angular/common/http";
import { Inject } from "@angular/core";

export class BaseApiService {
    baseUrl: string;
    constructor(private http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
      this.baseUrl = baseUrl;
    }

    async get<T>(uri: string) : Promise<T> {
        try {
            var headers = { "Content-Type": "application/json" };
            var result = await this.http.get<T>(this.baseUrl + uri, { headers }).toPromise();
            return result;
        } catch (error) {
            return null;
        }
    }

    async post<TModel, TResponse>(uri:string, model: TModel) : Promise<TResponse> {
        try {
          var headers = { "Content-Type": "application/json" };
            var result = await this.http.post<TResponse>(this.baseUrl + uri, JSON.stringify(model), { headers }).toPromise();
            return result;
        } catch (error) {
            return null;
        }
    }
}
