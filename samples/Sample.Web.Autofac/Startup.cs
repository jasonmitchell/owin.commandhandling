namespace Sample.Web.Autofac
{
    using System;
    using global::Autofac;
    using Owin;
    using Sequin;
    using Sequin.Autofac;
    using Sequin.CommandBus;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureAutofac();
            app.UseSequin(new SequinOptions
            {
                CommandBus = new ExclusiveHandlerCommandBus(new AutofacHandlerFactory(container))
            });
        }

        private static IContainer ConfigureAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("Handler"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            var container = builder.Build();
            return container;
        }
    }
}