using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Hubs;
using Xunit;

namespace XUnitTests
{
    public class QueueHubTests
    {
        [Fact]
        public async void SimpleHubTest()
        {
            bool sendCallend = false;
            var hub = new QueueHub(null, null, null);
            var mockClients = new Mock<IHubCallerClients>();
            var mockGroups = new Mock<IGroupManager>();
            hub.Groups = mockGroups.Object;
            hub.Clients = mockClients.Object;
        }
    }
}
