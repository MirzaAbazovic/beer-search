using BeerSearch.IRepository;
using BeerSearch.Model;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSearch.InMemoryRepository
{
    public class InMemoryBeerRepository : IBeerRepository
    {
        List<Beer> _beers;
        public InMemoryBeerRepository()
        {
            _beers = Builder<Beer>.CreateListOfSize(500).Build().ToList();
        }

        public Beer GetBeerDetails(string id)
        {
            return _beers.SingleOrDefault(p => p.Id == id);
        }

        public List<Beer> SearchBeer(string beerName)
        {
            return _beers.Where(p => p.Name.Contains(beerName)).OrderBy(p => p.Name).ToList();
        }
    }
}
