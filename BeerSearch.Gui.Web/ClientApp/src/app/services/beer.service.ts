import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
@Injectable()
export class BeerService {
  apiUrl : string;
  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiUrl;
   }

  searchBeerByName(name: string) {
    return this.http.get(this.apiUrl+'search/beers/'+name);
  }

  getBeerDetails(id: string) {
    return this.http.get(this.apiUrl+'beers/'+id);
  }
}
