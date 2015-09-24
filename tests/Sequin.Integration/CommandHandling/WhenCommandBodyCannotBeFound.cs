﻿namespace Sequin.Integration.CommandHandling
{
    using System.Net;
    using Xunit;

    public class WhenCommandBodyCannotBeFound : SequinSpecification
    {
        [Fact]
        public void ReturnsBadRequest()
        {
            var response = IssueCommand("TrackedCommand");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void ReturnsReasonPhrase()
        {
            var response = IssueCommand("TrackedCommand");
            Assert.Equal("Command body was not provided", response.ReasonPhrase);
        }
    }
}
