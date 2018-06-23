import { Component, OnInit } from '@angular/core';
import { BeerService } from '../services/beer.service';
import {Message} from 'primeng/api';

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
  constructor(private beerService : BeerService) { }

  ngOnInit() {  
  }
  search(){
    if(this.nameOfBeer.length<3)
    {

    }
    this.beerService.searchBeerByName(this.nameOfBeer).subscribe(p => 
      {
        //this.msgs.push({severity:'success', summary:'Success', detail:'Search successful'});
        this.beers = p;
      },
    error=>{
      //debugger;
      this.msgs.push({severity:'error', summary:'Error Message',  detail:'There was error calling API: '+error.error.Message});
    });
  }

  onDialogHide() {
    this.selectedBeer = null;
}
  selectBeer(event, beer){
    this.selectedBeer = beer;
    this.displayDialog = true;
  }

}
