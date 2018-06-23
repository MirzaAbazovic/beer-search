import { Component, OnInit } from '@angular/core';
import { BeerService } from '../services/beer.service';

@Component({
  selector: 'app-search-beer',
  templateUrl: './search-beer.component.html',
  styleUrls: ['./search-beer.component.css']
})
export class SearchBeerComponent implements OnInit {
  beers;
  nameOfBeer;
  constructor(private beerService : BeerService) { }

  ngOnInit() {  
  }
  search(){
    this.beerService.searchBeerByName(this.nameOfBeer).subscribe(p => 
      {
        this.beers = p;
      });
  }

}
