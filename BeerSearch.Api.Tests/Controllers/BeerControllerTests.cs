using BeerSearch.Api.Controllers;
using BeerSearch.IRepository;
using BeerSearch.Model;
using FizzWare.NBuilder;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace BeerSearch.Api.Tests.Controllers
{
    public class BeerControllerTests
    {
       
        [Fact]
        public void GetBeerById_WhenIdIsEmptyString_ShouldReturnBadRequestWithMessage()
        {
            // Arrange
            var repo = new Mock<IBeerRepository>();
            BeersController beerController = new BeersController(repo.Object);
            var expectedMessge = "Beer ID is mandatory";
         
            // Act
            IHttpActionResult result = beerController.GetBeerById(String.Empty);

            // Assert
            Assert.NotNull(result);
            var badRequestErrorMessageResult = result as BadRequestErrorMessageResult;
            Assert.NotNull(badRequestErrorMessageResult);
            Assert.Equal(expectedMessge, badRequestErrorMessageResult.Message);
        }

        [Fact]
        public void GetBeerById_WhenThereIsNoBeerWithId_ShouldReturnNotFoundMessage()
        {
            // Arrange
            var repo = new Mock<IBeerRepository>();
            repo.Setup(m => m.GetBeerDetails("beer1")).Returns(() => new Beer { Id = "beer1"});
            BeersController beerController = new BeersController(repo.Object);
            var getBeerId = "beer2";
            
            // Act
            IHttpActionResult result = beerController.GetBeerById(getBeerId);

            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void GetBeerById_WhenThereIsBeerWithId_ShouldReturnOKBeerWithCorrectData()
        {
            // Arrange
            var beer = new Beer {
                Id ="beer1"
                //TODO add other properies
            };
            var repo = new Mock<IBeerRepository>();
            repo.Setup(m => m.GetBeerDetails("beer1")).Returns(() => beer);
            BeersController beerController = new BeersController(repo.Object);
            var getBeerId = "beer1";
            // Act
            IHttpActionResult result = beerController.GetBeerById(getBeerId);

            // Assert
            Assert.NotNull(result);
            var okNegotiatedContentResult = result as OkNegotiatedContentResult<Beer>;
            Assert.NotNull(okNegotiatedContentResult);
            Assert.Equal(beer.Id, okNegotiatedContentResult.Content.Id);
            //TODO check other properies
        }

        [Fact]
        public void SearchBeersByName_WhenIdIsLessThen3Char_ShouldReturnBadRequestWithMessage()
        {
            // Arrange
            var repo = new Mock<IBeerRepository>();
            BeersController beerController = new BeersController(repo.Object);
            var expectedMessge = "Search by beer name must have at least three characters";
            var searchName = "Ip";
            // Act
            IHttpActionResult result = beerController.SearchBeersByName(searchName);

            // Assert
            Assert.NotNull(result);
            var badRequestErrorMessageResult = result as BadRequestErrorMessageResult;
            Assert.NotNull(badRequestErrorMessageResult);
            Assert.Equal(expectedMessge, badRequestErrorMessageResult.Message);
        }

        [Fact]
        public void SearchBeersByName_WhenThereAreNoBeersWithNameThatContinsSearchStrgin_ShouldReturnNotFoundMessage()
        {
            // Arrange
            var repo = new Mock<IBeerRepository>();
            var beers = Builder<Beer>.CreateListOfSize(100).Build().ToList();
            for (int i = 0; i < 5; i++)
            {
                beers[i].Name = new Random().ToString() + "ALE";
            }
            repo.Setup(m => m.SearchBeer("ALE")).Returns(() => beers);
            BeersController beerController = new BeersController(repo.Object);
            var searchName = "IPA RED";

            // Act
            IHttpActionResult result = beerController.SearchBeersByName(searchName);

            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void SearchBeersByName_WhenThereAreBeersContainingSearchString_ShouldReturnListOfBeersThatAllHaveSearchStrginInName()
        {
            // Arrange
            var searchName = "ALE";
            var beers = Builder<Beer>.CreateListOfSize(100).Build().ToList();
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                beers[i].Name = "ALE No: "+random.Next().ToString();
            }
            var repo = new Mock<IBeerRepository>();
            repo.Setup(m => m.SearchBeer(searchName))
                .Returns(()=>beers.Where(p=>p.Name.Contains(searchName)).ToList());

            BeersController beerController = new BeersController(repo.Object);
            
            // Act
            IHttpActionResult result = beerController.SearchBeersByName(searchName);

            // Assert
            Assert.NotNull(result);
            var okNegotiatedContentResult = result as OkNegotiatedContentResult<List<Beer>>;
            Assert.NotNull(okNegotiatedContentResult);
            Assert.Equal(5, okNegotiatedContentResult.Content.Count());
            foreach (var beer in okNegotiatedContentResult.Content)
            {
                Assert.Contains(searchName, beer.Name);
            }
            //TODO check other properies
        }



    }
}
