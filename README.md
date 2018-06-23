# Build a website about beer :) 
You're free in choosing languages and frameworks and how much you want to use from the following API: http://www.brewerydb.com/developers/docs

## Requirements:
1. The application should run without any additional (manual) tweaks
2. You should be able to search for beer, sort and filter the results and show detail information about the beer.
3. Display the results in an organized way and make it look good ;)
4. Nice to have: using a source control system like GitHub with multiple check-ins and of course, tests!

## Step 1
Analyze API  http://www.brewerydb.com/developers/docs more specific 
http://www.brewerydb.com/developers/docs-endpoint/beer_index and http://www.brewerydb.com/developers/docs-endpoint/beer_index and json structure of Beer entity.
See also form for adding beer http://www.brewerydb.com/add/beer-details/breweryId/zf0Of4 to get better understanding about beer entity(model).  

## Step 2
 
Use https://app.quicktype.io/to convert json into C# classes Beer.cs (and dependent classes like Style.cs) in order to create model.

## Step 3
 
Add projects for Beer Repositories (IRepository and is implementations: In memory, Json file, and Rest wrapper for brewerydb.com/api/v2 ) 
 
Add Rest API project BeerSearch.Api and demonstrate DI with injecting different Repositories in Global.asax.cs using AutoFac as IoC Container
Make Tests for BeerController and implement BeerController in SearchBeer.Api
Api has search by name and get by Id functionality.

## Step 3
 Implemented BrewerydbBeerRepository by using HttpClient - wraper (adapter) around brewerydb.com/api/v2 (save secret API key in ENV variable BREWERYDB_KEY)
 ```
 set BREWERYDB_KEY=SOMETHING_SECRET
 ``` 

## Step 4 
Added GUI project ASP.NET core Angular template. Used https://primefaces.org/primeng/#/ in order to "make it look good". Added simple angular service and page for searching beers by name.
Also enabled CORS on BeerSearch.Api   

## Step 5 
Display of results in grid, on click display more details about beer in modal dialog.


# TODO

## Step 6 
Implement paging, sort and filter of the search results on client side.