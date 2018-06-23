using Autofac;
using Autofac.Integration.WebApi;
using BeerSearch.BrewerydbRepository;
using BeerSearch.FileRepository;
using BeerSearch.InMemoryRepository;
using BeerSearch.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BeerSearch.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //IoC AutoFac container
            var builder = new ContainerBuilder();

            // Register application dependencies.
            //In memory Repository
            //builder.RegisterType<InMemoryBeerRepository>().As<IBeerRepository>();
            
            //Repository from json file in ~/App_Data/beers.json
            //var pathToJsonFile  = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data");
            //builder.RegisterType<JsonFileBeerRepository>().As<IBeerRepository>().WithParameter("jsonFile", Path.Combine(pathToJsonFile ,"beers.json"));

            //Repository from api http://api.brewerydb.com/v2/
            builder.RegisterType<BrewerydbBeerRepository>().As<IBeerRepository>();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}