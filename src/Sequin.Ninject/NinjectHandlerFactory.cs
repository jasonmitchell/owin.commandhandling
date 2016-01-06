namespace Sequin.Ninject
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Infrastructure;

    using global::Ninject;

    public class NinjectHandlerFactory : IHandlerFactory
    {
        private readonly IKernel kernel;

        public NinjectHandlerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public ICollection<IHandler<T>> GetForCommand<T>()
        {
            return kernel.GetAll<T>().OfType<IHandler<T>>().ToList();
        }
    }
}
