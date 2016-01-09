﻿namespace Sequin
{
    using System;
    using CommandBus;
    using Owin;
    using Microsoft.Owin;
    using Middleware;

    public static class SequinAppBuilderExtensions
    {
        public static void UseSequin(this IAppBuilder app)
        {
            app.UseSequin(new SequinOptions());
        }

        public static void UseSequin(this IAppBuilder app, SequinOptions options)
        {
            options.Validate();

            app.MapWhen(x => ShouldExecuteCommandPipeline(x, options.CommandEndpointPath), x =>
            {
                x.Use((ctx, next) =>
                {
                    ctx.Set("CommandEndpointPath", new PathString(options.CommandEndpointPath));
                    return next();
                });

                x.Use<JsonExceptionHandler>(options.HideExceptionDetail);
                x.Use<DiscoverCommand>(options.CommandNameResolver, options.CommandRegistry, options.CommandFactory);

                if (options.CommandPipeline != null)
                {
                    foreach (var pipelineStage in options.CommandPipeline)
                    {
                        x.Use(pipelineStage.MiddlewareType, pipelineStage.Arguments);
                    }
                }

                x.Use<IssueCommand>(options.CommandBus);
            });
        }

        private static bool ShouldExecuteCommandPipeline(IOwinContext context, string commandEndpointPath)
        {
            return context.Request.Method.Equals("PUT", StringComparison.InvariantCultureIgnoreCase) &&
                   context.Request.Path.StartsWithSegments(new PathString(commandEndpointPath));
        }
    }
}