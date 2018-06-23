using BeerSearch.BrewerydbRepository.Models;
using BeerSearch.IRepository;
using BeerSearch.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BeerSearch.BrewerydbRepository
{
    public class BrewerydbBeerRepository : IBeerRepository
    {
        const string API_URL = "http://api.brewerydb.com/v2";
        string _key;
        HttpClient _client;
        public BrewerydbBeerRepository()
        {
            _key = Environment.GetEnvironmentVariable("BREWERYDB_KEY");
            _client = new HttpClient();
        }
        public Beer GetBeerDetails(string id)
        {
            Beer beer = null;
            var response = _client.GetAsync(String.Format("{0}/{1}/{2}/?key={3}", API_URL, "beer", id, _key)).Result;
            if (response.IsSuccessStatusCode)
            {
                GetBeerByIdResponse getBeerByIdResponse = JsonConvert.DeserializeObject<GetBeerByIdResponse>(response.Content.ReadAsStringAsync().Result);
                if (getBeerByIdResponse.Status.ToLower().Equals("success"))
                {
                    beer = getBeerByIdResponse.Data;
                }
            }
            return beer;
        }
        public List<Beer> SearchBeer(string beerName)
        {
            List<Beer> beers = new List<Beer>();
            beers = GetAllPages(beerName, beers);
            return beers;
        }

        private List<Beer> GetAllPages(string beerName, List<Beer> beers, int page =1)
        {
            var response = _client.GetAsync(String.Format("{0}/{1}/?key={2}&type=beer&q={3}&p={4}", API_URL, "search", _key, beerName,page)).Result;
            if (response.IsSuccessStatusCode)
            {
                var searchBeerResponse = JsonConvert.DeserializeObject<GetBeerByISearchBeerByNameResponse>(response.Content.ReadAsStringAsync().Result);
                if (searchBeerResponse.Status.ToLower().Equals("success") && searchBeerResponse.NumberOfPages > 0)
                {
                    beers.AddRange(searchBeerResponse.Data);
                }
                if(page < searchBeerResponse.NumberOfPages && page < 200)//For some reason search api is not working after page 200
                {
                    GetAllPages(beerName, beers, ++page);
                }
            }
            return beers;
        }
    }
}
