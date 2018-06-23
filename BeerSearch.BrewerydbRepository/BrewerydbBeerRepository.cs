using BeerSearch.IRepository;
using BeerSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSearch.BrewerydbRepository
{
    public class BrewerydbBeerRepository : IBeerRepository
    {
        public Beer GetBeerDetails(string id)
        {
            throw new NotImplementedException();
        }

        public List<Beer> SearchBeer(string beerName)
        {
            throw new NotImplementedException();
        }
    }
}
