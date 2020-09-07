using Autofac;
using AzureFunctions.Autofac.Configuration;
using DAL;

namespace FunctionAppProductsGet.Infrastructure
{
    public class DIConfiguration
    {
        public DIConfiguration(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterType<FakeDB>().As<IDB>();

            }, functionName);
        }
    }
}
