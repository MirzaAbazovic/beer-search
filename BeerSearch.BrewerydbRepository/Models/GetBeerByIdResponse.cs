using BeerSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSearch.BrewerydbRepository.Models
{
    public class GetBeerByIdResponse :BaseResponse<Beer>
    {
        public string Message { get; set; }
    }
}
