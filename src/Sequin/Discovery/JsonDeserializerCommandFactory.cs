namespace Sequin.Discovery
{
    using System;
    using Newtonsoft.Json;

    public class JsonDeserializerCommandFactory : ICommandFactory
    {
        private readonly ICommandBodyProvider commandBodyProvider;
        private readonly JsonSerializerSettings serializerSettings;

        public JsonDeserializerCommandFactory(ICommandBodyProvider commandBodyProvider) : this(commandBodyProvider, new JsonSerializerSettings()) { }

        public JsonDeserializerCommandFactory(ICommandBodyProvider commandBodyProvider, JsonSerializerSettings serializerSettings)
        {
            this.commandBodyProvider = commandBodyProvider;
            this.serializerSettings = serializerSettings;
        }

        public object Create(Type commandType)
        {
            var comandBody = commandBodyProvider.Get();

            try
            {
                var command = Convert.ChangeType(JsonConvert.DeserializeObject(comandBody, commandType, serializerSettings), commandType);
                return command;
            }
            catch (JsonSerializationException ex)
            {
                throw new CommandConstructionException(ex.Message, commandType, comandBody, ex);
            }
            catch (JsonReaderException ex)
            {
                throw new CommandConstructionException("JSON command body could not be read; it may be malformed.", commandType, comandBody, ex);
            }
        }
    }
}