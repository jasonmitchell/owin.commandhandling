namespace Sequin.Discovery
{
    using System;

    public abstract class CommandFactory
    {
        public abstract object Create(Type commandType);
    }
}
