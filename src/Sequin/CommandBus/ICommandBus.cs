namespace Sequin.CommandBus
{
    public interface ICommandBus
    {
        void Issue<T>(T command);
    }
}