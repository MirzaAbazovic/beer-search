# Description of app

Video of app can be seen [here](https://www.youtube.com/watch?v=VnBDtj2hxFI)

In order to Run/Debug app locally 

1. Clone repo ``` git clone https://mirza_abazovic@bitbucket.org/mirza_abazovic/beersearch.git```

2. If planing to use http://api.brewerydb.com and have api key set env variable ```set BREWERYDB_KEY=YOUR_API_KEY```

3. Open solution BeerSearch.sln set up to start multiple startup projects BeerSearch.Api and BeerSearch.Gui.Web

4. Hit F5

# Solution
Solution has next projects
![Solution](https://bytebucket.org/mirza_abazovic/beersearch/raw/8adc8a8ef8eea4ed8b16f6596e95a5afcfda9ef5/solution.JPG)

| Project						| Description	|
| ------------------------------| --------------|
|BeerSearch.Model				|	Domain classes Models Beer Entity for app (used from http://www.brewerydb.com)
|BeerSearch.Api					|	Backend REST API with methods for search and get details about beer
|BeerSearch.Api.Tests			|	Tests for Backend REST API
|BeerSearch.IRepository			|	Interface for repository that REST API uses to get data
|BeerSearch.InMemoryRepository	|	Implementation of IBeerReposotory in memory data
|BeerSearch.FileRepository		|	Implementation of IBeerReposotory in where data is in json file (file can be found in BeerSearch.Api\App_Data\beers.json)
|BeerSearch.BrewerydbRepository	|	Implementation of IBeerReposotory where data is retrieved from REST API from http://api.brewerydb.com/v2. You need key and has to be set as env variable BREWERYDB_KEY
|BeerSearch.Gui.Web				|	User interface written in Angular (ASP.NET Core template) uses REST API from BeerSearch.Api as backend.

To change repository in Global.asax.cs select repository to inject by registering wanted repo. implementation.

```csharp 
  protected void Application_Start()
        {
            //IoC AutoFac container
            var builder = new ContainerBuilder();
            
            // Register application dependencies.
            //In memory Repository
            //builder.RegisterType<InMemoryBeerRepository>().As<IBeerRepository>();
            
            //Repository from json file in ~/App_Data/beers.json
            var pathToJsonFile  = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data");
            builder.RegisterType<JsonFileBeerRepository>().As<IBeerRepository>().WithParameter("jsonFile", Path.Combine(pathToJsonFile ,"beers.json"));

            //Repository from api http://api.brewerydb.com/v2/
            //builder.RegisterType<BrewerydbBeerRepository>().As<IBeerRepository>();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
```

# What would I do differently

### At the moment GUI uses only search api and does filtering, sorting and paging on client side. I would make model for search api smaller only 3-5 fields (short info about beer with icon image) and implement server side paging, sorting and filtering.

BeerSearch.BrewerydbRepository.BrewerydbBeerRepository Has method that is bottleneck
```charp 
private List<Beer> GetAllPages(string beerName, List<Beer> beers, int page =1)
```
It uses recursion to get all pages of search result beer with name from api.brewerydb.com/v2 (hits brewerydb api N times where N is number of pager in initial result)
For example search for term red GET http://api.brewerydb.com/v2/search?key=XXXXX&type=beer&q=red  
```json
{
    "currentPage": 1,
    "numberOfPages": 42,
    "totalResults": 2092,
    "data": [...],
    "status": "success"
}
```
Will hit this api 42 times to get all 2092 items.

I would "propagate" pagination from my API to brewerydb api

### Now GUI has all data about beer from search api in and uses it (when showing details) instead of going to backend to get details. Model for Beer for API GET beer/:id would leave same (whole rich graph) and use it on GUI  when users selects to see beer details.

