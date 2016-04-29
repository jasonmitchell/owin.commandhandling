namespace Sequin.Owin
{
    using Discovery;
    using Infrastructure;
    using Sequin.Discovery;

    public class OwinSequinOptions : SequinOptions
    {
        public OwinSequinOptions()
        {
            CommandNameResolver = new RequestHeaderCommandNameResolver();
            CommandFactory = new JsonDeserializerCommandFactory(new OwinEnvironmentBodyProvider());
        }
    }
}