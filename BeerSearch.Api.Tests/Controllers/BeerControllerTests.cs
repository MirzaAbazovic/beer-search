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
        private BeersController _beersController;
        private Mock<IBeerRepository> _beerRepositoryMock = new Mock<IBeerRepository>();

        public BeerControllerTests()
        {
            _beersController = new BeersController(_beerRepositoryMock.Object);
        }
        [Fact]
        public void GetBeerById_WhenIdIsEmptyString_ShouldReturnBadRequestWithMessage()
        {
            // Arrange
            var expectedMessge = "Beer ID is mandatory";
            // Act
            IHttpActionResult result = _beersController.GetBeerById(String.Empty);

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
            _beerRepositoryMock.Setup(m => m.GetBeerDetails("beer1")).Returns(() => new Beer { Id = "beer1"});
            var getBeerId = "beer2";
            
            // Act
            IHttpActionResult result = _beersController.GetBeerById(getBeerId);

            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public void GetBeerById_WhenThereIsBeerWithId_ShouldReturnOKBeerWithCorrectData()
        {
            // Arrange
            /*
             "id": "B2lc7P",
            "name": "5th Year Beer",
            "nameDisplay": "5th Year Beer",
            "description": "Our 5th Year Beer is an extremely drinkable IPA brewed with Pilsner, Carafoam and Aromatic malts to yield a pale golden color and clean malt profile.\r\n\r\nThe real stars of the show are the new German varieties of hops that were hand selected by our friend Nunzino while in the Fatherland. The bales of whole hops were shipped back to Hop Head Farms production facility in Hickory Corners, Michigan, to be pelletized under his strict specifications. We utilized the hop variety Apollo to achieve a crisp, aggressive, yet clean bitterness.\r\n\r\nFinally we blended the German varieties Hallertau Blanc, Melon and Mandarina in multiple additions during the brewkettle, whirlpool and in the dry-hop.\r\n\r\nThis program of hop additions infuses this unique IPA with aromas and flavors of green grapes, freshly cut melons and citrus zest.  Enjoy it while you can -- it will not be around very long!\r\n\r\n75 IBU",
            "abv": "6.3",
            "styleId": 30,
            "isOrganic": "N",
            "status": "verified",
            "statusDisplay": "Verified",
            "createDate": "2018-05-10 20:13:31",
            "updateDate": "2018-06-11 20:08:51",
            "style": {
                "id": 30,
                "categoryId": 3,
                "category": {
                    "id": 3,
                    "name": "North American Origin Ales",
                    "createDate": "2012-03-21 20:06:45"
                },
                "name": "American-Style India Pale Ale",
                "shortName": "American IPA",
                "description": "American-style India pale ales are perceived to have medium-high to intense hop bitterness, flavor and aroma with medium-high alcohol content. The style is further characterized by floral, fruity, citrus-like, piney, resinous, or sulfur-like American-variety hop character. Note that one or more of these American-variety hop characters is the perceived end, but the hop characters may be a result of the skillful use of hops of other national origins. The use of water with high mineral content results in a crisp, dry beer. This pale gold to deep copper-colored ale has a full, flowery hop aroma and may have a strong hop flavor (in addition to the perception of hop bitterness). India pale ales possess medium maltiness which contributes to a medium body. Fruity-ester flavors and aromas are moderate to very strong. Diacetyl can be absent or may be perceived at very low levels. Chill and/or hop haze is allowable at cold temperatures. (English and citrus-like American hops are considered enough of a distinction justifying separate American-style IPA and English-style IPA categories or subcategories. Hops of other origins may be used for bitterness or approximating traditional American or English character. See English-style India Pale Ale",
                "ibuMin": "50",
                "ibuMax": "70",
                "abvMin": "6.3",
                "abvMax": "7.5",
                "srmMin": "6",
                "srmMax": "14",
                "ogMin": "1.06",
                "fgMin": "1.012",
                "fgMax": "1.018",
                "createDate": "2012-03-21 20:06:45",
                "updateDate": "2015-04-07 15:26:37"
            }*/
            var beer = new Beer {
                Id = "B2lc7P",
                Name = "5th Year Beer",
                NameDisplay = "5th Year Beer",
                Description ="Our 5th Year Beer is an extremely drinkable IPA brewed with Pilsner, Carafoam and Aromatic malts to yield a pale golden color and clean malt profile.\r\n\r\nThe real stars of the show are the new German varieties of hops that were hand selected by our friend Nunzino while in the Fatherland. The bales of whole hops were shipped back to Hop Head Farms production facility in Hickory Corners, Michigan, to be pelletized under his strict specifications. We utilized the hop variety Apollo to achieve a crisp, aggressive, yet clean bitterness.\r\n\r\nFinally we blended the German varieties Hallertau Blanc, Melon and Mandarina in multiple additions during the brewkettle, whirlpool and in the dry-hop.\r\n\r\nThis program of hop additions infuses this unique IPA with aromas and flavors of green grapes, freshly cut melons and citrus zest.  Enjoy it while you can -- it will not be around very long!\r\n\r\n75 IBU",
                Abv = "6.3",
                StyleId = 30,
                IsOrganic = "N",
                Status = "verified",
                StatusDisplay = "Verified",
                InsertDate = DateTime.Parse("2018-05-10 20:13:31"),
                UpdateDate = DateTime.Parse("2018-06-11 20:08:51")
            };
            
            _beerRepositoryMock.Setup(m => m.GetBeerDetails("beer1")).Returns(() => beer);
            var getBeerId = "beer1";
            // Act
            IHttpActionResult result = _beersController.GetBeerById(getBeerId);

            // Assert
            Assert.NotNull(result);
            var okNegotiatedContentResult = result as OkNegotiatedContentResult<Beer>;
            Assert.NotNull(okNegotiatedContentResult);
            var actualBeer = okNegotiatedContentResult.Content;
            Assert.Equal(beer.Id, actualBeer.Id);
            Assert.Equal(beer.Name, actualBeer.Name);
            Assert.Equal(beer.NameDisplay, actualBeer.NameDisplay);
            Assert.Equal(beer.Abv, actualBeer.Abv);
            Assert.Equal(beer.StyleId, actualBeer.StyleId);
            Assert.Equal(beer.IsOrganic, actualBeer.IsOrganic);
            Assert.Equal(beer.Status, actualBeer.Status);
            Assert.Equal(beer.StatusDisplay, actualBeer.StatusDisplay);
            Assert.Equal(beer.InsertDate, actualBeer.InsertDate);
            Assert.Equal(beer.UpdateDate, actualBeer.UpdateDate);
        }

        [Fact]
        public void SearchBeersByName_WhenIdIsLessThen3Char_ShouldReturnBadRequestWithMessage()
        {
            // Arrange
            var expectedMessge = "Search by beer name must have at least three characters";
            var searchName = "Ip";
            // Act
            IHttpActionResult result = _beersController.SearchBeersByName(searchName);

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
            _beerRepositoryMock.Setup(m => m.SearchBeer("ALE")).Returns(() => beers);
            var searchName = "IPA RED";

            // Act
            IHttpActionResult result = _beersController.SearchBeersByName(searchName);

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
            _beerRepositoryMock.Setup(m => m.SearchBeer(searchName))
                .Returns(()=>beers.Where(p=>p.Name.Contains(searchName)).ToList());

            // Act
            IHttpActionResult result = _beersController.SearchBeersByName(searchName);

            // Assert
            Assert.NotNull(result);
            var okNegotiatedContentResult = result as OkNegotiatedContentResult<List<Beer>>;
            Assert.NotNull(okNegotiatedContentResult);
            Assert.Equal(5, okNegotiatedContentResult.Content.Count());
            foreach (var beer in okNegotiatedContentResult.Content)
            {
                Assert.Contains(searchName, beer.Name);
            }
        }

    }
}
