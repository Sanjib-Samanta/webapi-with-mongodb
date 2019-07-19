using Autofac;
using Autofac.Integration.WebApi;
using MyStore.Mongo.Repository;
using MyStore.MongoDB;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace WebAPIDemo.App_Start
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;
        public static IMongoDBSettings Settings;
        /// <summary>
        /// Initializes the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Initialize(HttpConfiguration config)
        {
            var mongoDbHost = ConfigurationManager.AppSettings["MongoDBHost"];
            var mongoDbName = ConfigurationManager.AppSettings["MongoDBName"];
            if (!string.IsNullOrWhiteSpace(mongoDbHost)
                && !string.IsNullOrWhiteSpace(mongoDbName))
            {
                Settings = new MongoDBSettings
                {
                    MongoDBConnectionString = mongoDbHost,
                    MongoDBName = mongoDbName
                };
            }

            Initialize(config, RegisterServices(new ContainerBuilder()));

        }

        /// <summary>
        /// Initializes the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="container">The container.</param>
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register(ctx =>
            {
                return Settings;
            }).As<IMongoDBSettings>().InstancePerRequest();

            builder.RegisterType<MongoDBContext>()
                   .As<IMongoDBContext>()
                   .InstancePerRequest();

            builder.RegisterType<ProductMongoRepository>()
                   .As<IProductMongoRepository>()
                   .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}
