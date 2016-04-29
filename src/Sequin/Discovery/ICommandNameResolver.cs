namespace Sequin.Discovery
{
    using System.Collections.Generic;

    public interface ICommandNameResolver
    {
        string GetCommandName(IDictionary<string, object> environment);
    }
}