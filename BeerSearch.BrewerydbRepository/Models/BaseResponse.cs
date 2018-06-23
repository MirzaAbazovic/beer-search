using BeerSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerSearch.BrewerydbRepository.Models
{
    public class BaseResponse<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
    }
}
