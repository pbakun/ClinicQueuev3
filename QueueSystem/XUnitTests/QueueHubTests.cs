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
using System.Threading;
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
        private readonly Mock<IHubCallerClients> _mockClients;
        private readonly Mock<Microsoft.AspNetCore.SignalR.IClientProxy> _mockClientProxy;
        private readonly Mock<IHubContext<QueueHub>> _mockHubContext;
        private readonly Mock<IQueueService> _mockQueueService;
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly Mock<IGroupManager> _mockGroupManager;
        private readonly Mock<Microsoft.AspNetCore.SignalR.HubCallerContext> _mockHubCallerContext;

        public QueueHubTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();

            _mockClientProxy = new Mock<Microsoft.AspNetCore.SignalR.IClientProxy>();

            _mockClients = new Mock<IHubCallerClients>();
            _mockHubContext = new Mock<IHubContext<QueueHub>>();
            _mockQueueService = new Mock<IQueueService>();
            _mockRepo = new Mock<IRepositoryWrapper>();
            _mockGroupManager = new Mock<IGroupManager>();
            _mockHubCallerContext = new Mock<Microsoft.AspNetCore.SignalR.HubCallerContext>();

        }

        public async Task<Queue> CallRegisterDoctor(string id, int roomNo, 
            Mock<IHubCallerClients> mockClients,
            Mock<Microsoft.AspNetCore.SignalR.IClientProxy> mockClientProxy,
            Mock<IGroupManager> mockGroupManager)
        {
            if(mockClients == null)
            {
                mockClients = _mockClients;
            }
            if(mockClientProxy == null)
            {
                mockClientProxy = _mockClientProxy;
            }
            if(mockGroupManager == null)
            {
                mockGroupManager = _mockGroupManager;
            }


            var prepareQueue = new QueueData().WithQueueNo(12).WithRoomNo(roomNo).Build();
            var queue = _mapper.Map<Queue>(prepareQueue);

            var prepareUser = new UserData().WithRoomNo(roomNo).BuildAsList();

            _mockRepo.Setup(r => r.User.FindByCondition(It.IsAny<Expression<Func<Entities.Models.User, bool>>>()))
                .Returns(prepareUser);
            _mockQueueService.Setup(q => q.FindByUserId(It.IsAny<string>())).Returns(queue);
            _mockQueueService.Setup(q => q.NewQueueNo(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(queue));


            _mockHubCallerContext.Setup(c => c.ConnectionId).Returns(It.IsAny<string>());

            mockClients.Setup(c => c.Group(queue.RoomNo.ToString())).Returns(() => mockClientProxy.Object);
            //System.Diagnostics.Debugger.Launch();
            
            
            var hub = new QueueHub(_mockRepo.Object, _mockQueueService.Object, _mockHubContext.Object)
            {
                Clients = mockClients.Object,
                Context = _mockHubCallerContext.Object,
                Groups = mockGroupManager.Object
            };

            await hub.RegisterDoctor(id, queue.RoomNo);

            return queue;

        }


        [Theory]
        [InlineData("1", 12)]
        [InlineData("2", 12)]
        public async Task RegisterDoctor_TestOneOwner(string id, int roomNo)
        {
            var mockClientProxy = new Mock<Microsoft.AspNetCore.SignalR.IClientProxy>();
            var mockClients = new Mock<IHubCallerClients>();
            var mockGroupManager = new Mock<IGroupManager>();

            //System.Diagnostics.Debugger.Launch();
            var queue = await CallRegisterDoctor(id, roomNo, mockClients, mockClientProxy, mockGroupManager);

            mockClients.Verify(c => c.Group("12"), Times.AtLeastOnce);

            mockClientProxy.Verify(p => p.SendCoreAsync("ReceiveDoctorFullName",
                It.Is<object[]>(o => o != null && o.Length == 2),
                default(CancellationToken)), Times.Once);

            mockClientProxy.Verify(p => p.SendCoreAsync("ReceiveQueueNo",
                It.Is<object[]>(o => o != null && o.Length == 2 && ((string)o[1]) == queue.QueueNoMessage),
                default(CancellationToken)), Times.Once);

            mockClientProxy.Verify(p => p.SendCoreAsync("ReceiveAdditionalInfo",
                It.Is<object[]>(o => o != null && o.Length == 2 && ((string)o[1]) == queue.AdditionalMessage),
                default(CancellationToken)), Times.Once);

            QueueHub._connectedUsers.Clear();
            //await hub.NewQueueNo("1", 12, 12);

            //_mockClientProxy.Verify(p => p.SendCoreAsync("ReceiveQueueNo", 
            //    It.Is<object[]>(o => o != null && o.Length==2 && ((string)o[1]) == "PB12"),
            //    default(CancellationToken)));
        }


        [Theory]
        [InlineData(12, 2)]
        [InlineData(12, 3)]
        [InlineData(13, 5)]
        [InlineData(10, 30)]
        public async Task RegisterDoctor_TestMoreThanOneOwner(int roomNo, int numberOfCalls)
        {
            var mockClientProxy = new Mock<Microsoft.AspNetCore.SignalR.IClientProxy>();
            var mockClients = new Mock<IHubCallerClients>();
            var mockGroupManager = new Mock<IGroupManager>();

            mockClients.Setup(c => c.Caller).Returns(() => mockClientProxy.Object);

            //System.Diagnostics.Debugger.Launch();
            for (int i=1; i < numberOfCalls + 1; i++)
            {
                var queue = await CallRegisterDoctor(i.ToString(), roomNo, mockClients, mockClientProxy, mockGroupManager);
            }

            mockClients.Verify(c => c.Group(roomNo.ToString()), Times.AtLeastOnce);

            mockClientProxy.Verify(p => p.SendCoreAsync("NotifyQueueOccupied",
                It.Is<object[]>(o => o != null && o.Length == 1),
                default(CancellationToken)), Times.Exactly(numberOfCalls - 1));


            QueueHub._connectedUsers.Clear();
            QueueHub._waitingUsers.Clear();
        }

        [Theory]
        [InlineData("1", 12)]
        [InlineData("2", 12)]
        public async Task NewQueueNoTest(string id, int roomNo)
        {
            var mockClientProxy = new Mock<Microsoft.AspNetCore.SignalR.IClientProxy>();
            var mockClients = new Mock<IHubCallerClients>();
            var mockGroupManager = new Mock<IGroupManager>();

            var prepareQueue = new QueueData().WithRoomNo(roomNo).WithQueueNo(15).WithOwnerInitials("PB").Build();
            var preparedQueue = _mapper.Map<Queue>(prepareQueue);
            
            var queue = await CallRegisterDoctor(id, roomNo, mockClients, mockClientProxy, mockGroupManager);

            _mockQueueService.Setup(q => q.NewQueueNo(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(preparedQueue));

            var hub = new QueueHub(_mockRepo.Object, _mockQueueService.Object, _mockHubContext.Object)
            {
                Clients = mockClients.Object,
                Context = _mockHubCallerContext.Object,
                Groups = mockGroupManager.Object
            };
            //System.Diagnostics.Debugger.Launch();
            await hub.NewQueueNo("1", preparedQueue.QueueNo, roomNo);

            _mockClientProxy.Verify(p => p.SendCoreAsync("ReceiveQueueNo",
                It.Is<object[]>(o => o != null && o.Length == 2 && ((string)o[1]) == preparedQueue.QueueNoMessage),
                default(CancellationToken)), Times.AtLeastOnce);

            QueueHub._connectedUsers.Clear();
        }
    }
}
