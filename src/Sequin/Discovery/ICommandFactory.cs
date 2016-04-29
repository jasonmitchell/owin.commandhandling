namespace Sequin.Discovery
{
    using System;

    public interface ICommandFactory
    {
        object Create(Type commandType);
    }
}