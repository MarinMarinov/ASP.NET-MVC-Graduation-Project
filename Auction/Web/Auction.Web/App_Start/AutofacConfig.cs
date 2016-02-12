namespace Auction.Web
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Data;
    using Infrastructure;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;
    using Auction.Models;
    using Data.Repositories;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new AuctionDbContext()).As<DbContext>().InstancePerRequest();

            //builder.Register(x => new EfGenericRepository<Auction>()).As

            builder.Register(x => new AuctionService()).As<IAuctionService>().InstancePerRequest();

        }
    }
}