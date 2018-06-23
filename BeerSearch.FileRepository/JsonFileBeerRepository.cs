using BeerSearch.IRepository;
using BeerSearch.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSearch.FileRepository
{
    public class JsonFileBeerRepository : IBeerRepository
    {
        List<Beer> _beers;

        public JsonFileBeerRepository(string jsonFile)
        {
            using (StreamReader file = File.OpenText(jsonFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                //_beers = JsonConvert.DeserializeObject<List<Beer>>(jsonFile);
                _beers= (List<Beer>)serializer.Deserialize(file, typeof(List<Beer>));
            }
        }
        public Beer GetBeerDetails(string id)
        {
            return _beers.SingleOrDefault(p => p.Id == id);
        }
        public List<Beer> SearchBeer(string beerName)
        {
            return _beers.Where(p => p.Name.ToLower().Contains(beerName.ToLower())).OrderBy(p => p.Name).ToList();
        }
        /*
        public PageableList<Beer> SearchBeer(string beerName)
        {
            var beers = _beers.Where(p => p.Name == beerName).OrderBy(p=>p.Name);
            var totalResults = beers.Count();
            int pages = totalResults % 50;
            return new PageableList<Beer>
            {
                Data = beers.ToList(),
                TotalResults = beers.Count(),
                NumberOfPages = pages,
            };
        }
        */
    }
}
