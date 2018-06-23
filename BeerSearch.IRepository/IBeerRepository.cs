using BeerSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSearch.IRepository
{
    public interface IBeerRepository
    {
        Beer GetBeerDetails(string id);
        //Maybe later for server side paging and ordering
        //PageableList<Beer> SearchBeer(string beerName, int page = 1, string orderBy="Name");
        List<Beer> SearchBeer(string beerName);
    }
}
