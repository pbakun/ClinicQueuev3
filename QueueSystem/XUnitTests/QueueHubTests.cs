using AutoMapper;
using Entities;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApp.Hubs;
using WebApp.Mappings;
using WebApp.Models;
using WebApp.ServiceLogic;
using Xunit;
using XUnitTests.TestingData;

namespace XUnitTests
{
    public class QueueHubTests
    {
        private readonly IMapper _mapper;

        public QueueHubTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }


        [Fact]
        public async Task SimpleHubTest()
        {
            var prepareQueue = new QueueData().WithQueueNo(5).Build();
            var queue = _mapper.Map<Queue>(prepareQueue);

            bool sendCallend = false;
            var mockClients = new Mock<IHubCallerClients>();
            //var mockGroups = new Mock<IGroupManager>();
            var mockHubContext = new Mock<IHubContext<QueueHub>>();
            var mockQueueService = new Mock<IQueueService>();

            mockQueueService.Setup(q => q.FindByUserId(It.IsAny<string>())).Returns(queue);

            var mockRepo = new Mock<IRepositoryWrapper>();
            var mockGroupManager = new Mock<IGroupManager>();
            System.Diagnostics.Debugger.Launch();
            //setup Repo
            mockRepo.Setup(r => r.User.FindByCondition(It.IsAny<Expression<Func<Entities.Models.User, bool>>>()))
                .Returns(PrepareUser());

            var mockHubCalletContext = new Mock<Microsoft.AspNetCore.SignalR.HubCallerContext>();
            mockHubCalletContext.Setup(c => c.ConnectionId).Returns("2");


            var mockClientProxy = new Mock<Microsoft.AspNetCore.SignalR.IClientProxy>();

            mockClients.Setup(c => c.Group("12")).Returns(mockClientProxy.Object);


            var hub = new QueueHub(mockRepo.Object, mockQueueService.Object, mockHubContext.Object);
            hub.Clients = mockClients.Object;
            hub.Context = mockHubCalletContext.Object;
            hub.Groups = mockGroupManager.Object;
            //prepare context to add user to hubUsers

            await hub.RegisterDoctor("1", 12);

            //await hub.NewQueueNo("1", 12, 12);

            mockClients.Verify(c => c.Group("12"), Times.AtLeastOnce);
        }

        public List<Entities.Models.User> PrepareUser()
        {
            var list = new List<Entities.Models.User>();
            list.Add(new Entities.Models.User()
            {
                Id = "1",

            });
            return list;
        }
    }
}
