import { Component, OnInit } from '@angular/core';
import { BeerService } from '../services/beer.service';
import { Message, SelectItem } from 'primeng/api';

@Component({
  selector: 'app-search-beer',
  templateUrl: './search-beer.component.html',
  styleUrls: ['./search-beer.component.css']
})
export class SearchBeerComponent implements OnInit {
  beers;
  nameOfBeer: string;
  selectedBeer;
  displayDialog: boolean;
  msgs: Message[] = [];
  sortOptions: SelectItem[];
  sortKey: string;
  sortField: string;
  sortOrder: number;
  fetchingData = false;
  constructor(private beerService: BeerService) { }

  ngOnInit() {
    this.sortOptions = [
      { label: 'Name', value: 'Name' },
      { label: 'Is organic', value: 'IsOrganic' }
    ];
  }
  search() {
    this.fetchingData = true;
    this.beerService.searchBeerByName(this.nameOfBeer).subscribe(p => {

      this.fetchingData = false;
      this.beers = p;

    },
      error => {
        this.fetchingData = false;
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: 'There was error calling API: ' + error.error.Message });
      });
  }

  onSortChange(event) {
    let value = event.value;

    if (value.indexOf('!') === 0) {
      this.sortOrder = -1;
      this.sortField = value.substring(1, value.length);
    }
    else {
      this.sortOrder = 1;
      this.sortField = value;
    }
  }

  onDialogHide() {
    this.selectedBeer = null;
  }
  selectBeer(event, beer) {
    this.selectedBeer = beer;
    this.displayDialog = true;
  }

}
