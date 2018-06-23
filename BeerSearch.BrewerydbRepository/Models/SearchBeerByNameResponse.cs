using BeerSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSearch.BrewerydbRepository.Models
{
    public class GetBeerByISearchBeerByNameResponse : BaseResponse<List<Beer>>
    {
        public int CurrentPage { get; set; }
        public int NumberOfPages{ get; set; }
        public int TotalResults { get; set; }

    }
}
