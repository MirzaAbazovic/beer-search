using BeerSearch.IRepository;
using BeerSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BeerSearch.Api.Controllers
{

    [RoutePrefix("beers")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BeersController : ApiController
    {
        private IBeerRepository _beerRepository;
        public BeersController(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        // GET beers/:id
        [HttpGet()]
        [Route("{id}")]
        public IHttpActionResult GetBeerById(string id) {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Beer ID is mandatory");
            }
            var beer = _beerRepository.GetBeerDetails(id);
            if (beer == null)
            {
                return NotFound();
            }
            return Ok(beer);
        }

        // GET /search/beers/:beerName
        [HttpGet()]
        [Route("~/search/beers/{beerName}")]
        public IHttpActionResult SearchBeersByName(string beerName)
        {
            if (string.IsNullOrEmpty(beerName) || beerName.Length < 3)
            {
                return BadRequest("Search by beer name must have at least three characters");
            }
            var beer = _beerRepository.SearchBeer(beerName);
            if (beer == null)
            {
                return NotFound();
            }
            return Ok(beer);
        }
    }
}