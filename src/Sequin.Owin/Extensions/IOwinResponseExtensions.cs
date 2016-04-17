﻿namespace Sequin.Extensions
{
    using System;
    using System.Net;
    using Microsoft.Owin;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class IOwinResponseExtensions
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static void BadRequest(this IOwinResponse response, string message)
        {
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.ReasonPhrase = message;
        }

        public static void NotFound(this IOwinResponse response, string message)
        {
            response.StatusCode = (int) HttpStatusCode.NotFound;
            response.ReasonPhrase = message;
        }

        public static void Json(this IOwinResponse response, object body)
        {
            response.ContentType = "application/json";
            response.Write(JsonConvert.SerializeObject(body, SerializerSettings));
        }
    }
}
