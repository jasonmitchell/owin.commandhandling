﻿namespace Sequin
{
    using System;
    using CommandBus;
    using Core.Infrastructure;
    using Infrastructure;

    public class SequinOptions
    {
        public SequinOptions()
        {
            var appDomainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            CommandEndpointPath = "/commands";
            CommandRegistry = new ReflectionCommandRegistry(appDomainAssemblies);
            CommandBus = new ExclusiveHandlerCommandBus(new ReflectionHandlerFactory(appDomainAssemblies));
            CommandNameResolver = new RequestHeaderCommandNameResolver();
            CommandFactory = new JsonDeserializerCommandFactory();
        }

        public string CommandEndpointPath { get; set; }

        public ICommandRegistry CommandRegistry { get; set; }

        public ICommandBus CommandBus { get; set; }

        public ICommandNameResolver CommandNameResolver { get; set; }

        public ICommandFactory CommandFactory { get; set; }

        public CommandPipelineStage[] CommandPipeline { get; set; }

        public bool HideExceptionDetail { get; set; }

        internal void Validate()
        {
            if (string.IsNullOrWhiteSpace(CommandEndpointPath))
            {
                throw new SequinConfigurationException(nameof(CommandEndpointPath));
            }

            if (CommandRegistry == null)
            {
                throw new SequinConfigurationException(nameof(CommandRegistry));
            }

            if (CommandBus == null)
            {
                throw new SequinConfigurationException(nameof(CommandBus));
            }

            if (CommandNameResolver == null)
            {
                throw new SequinConfigurationException(nameof(CommandNameResolver));
            }

            if (CommandFactory == null)
            {
                throw new SequinConfigurationException(nameof(CommandFactory));
            }
        }
    }
}