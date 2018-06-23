import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class BeerService {

  constructor(private http: HttpClient) { }

  searchBeerByName(name: string) {
    return this.http.get('http://localhost:64720/search/beers/'+name);
  }

  getBeerDetails(id: string) {
    return this.http.get('http://localhost:64720/beers/'+id);
  }
}
