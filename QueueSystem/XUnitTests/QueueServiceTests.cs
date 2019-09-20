using AutoMapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp;
using WebApp.Mappings;
using WebApp.ServiceLogic;
using Xunit;
using XUnitTests.TestingData;

namespace XUnitTests
{
    public class QueueServiceTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<IServiceScopeFactory> _mockScopeFactory;
        private readonly Mock<IRepositoryWrapper> _mockRepo;

        public QueueServiceTests()
        {
            //initialize
            _mockRepo = new Mock<IRepositoryWrapper>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();

            _mockScopeFactory = new Mock<IServiceScopeFactory>();

            //setup
            _mockRepo.Setup(x => x.Queue.Update(It.IsAny<Entities.Models.Queue>()));
            _mockRepo.Setup(x => x.User.Update(It.IsAny<Entities.Models.User>()));
            _mockRepo.Setup(x => x.SaveAsync());
            _mockRepo.Setup(x => x.Save());
        }

        #region Call Methods to Test, inject necessery dependencies

        private async Task<WebApp.Models.Queue> CallNewQueueNo(int queueNo, List<Entities.Models.Queue> data)
        {
            var mockRepo = new Mock<IRepositoryWrapper>();
            mockRepo.Setup(x => x.Queue.FindByCondition(It.IsAny<Expression<Func<Entities.Models.Queue, bool>>>()))
                .Returns(data);

            mockRepo.Setup(x => x.Queue.Update(It.IsAny<Entities.Models.Queue>()));
            mockRepo.Setup(x => x.SaveAsync());

            var sut = new QueueService(mockRepo.Object, _mapper, null);

            var result = await sut.NewQueueNo("1", queueNo);

            return result;
        }

        private async Task<WebApp.Models.Queue> CallNewAdditioanlInfo(string message, List<Entities.Models.Queue> data)
        { 
            var mockRepo = new Mock<IRepositoryWrapper>();
            mockRepo.Setup(x => x.Queue.FindByCondition(It.IsAny<Expression<Func<Entities.Models.Queue, bool>>>()))
                .Returns(data);

            mockRepo.Setup(x => x.Queue.Update(It.IsAny<Entities.Models.Queue>()));
            mockRepo.Setup(x => x.SaveAsync());

            var sut = new QueueService(mockRepo.Object, _mapper, null);

            var result = await sut.NewAdditionalInfo("1", message);

            return result;
        }

        private WebApp.Models.Queue CallChangeUserRoomNo(int newRoomNo, int oldRoomNo) 
        {
            _mockRepo.Setup(x => x.Queue.FindByCondition(It.IsAny<Expression<Func<Entities.Models.Queue, bool>>>()))
                .Returns(new QueueData().WithRoomNo(oldRoomNo).BuildAsList());

            _mockRepo.Setup(x => x.User.FindByCondition(It.IsAny<Expression<Func<Entities.Models.User, bool>>>()))
                .Returns(new UserData().WithRoomNo(oldRoomNo).BuildAsList());

            var sut = new QueueService(_mockRepo.Object, _mapper, null);

            var result = sut.ChangeUserRoomNo("1", newRoomNo);
            return result;
        }

        #endregion

        //Test methods

        #region NewQueueNo Tests

        [Theory]
        [InlineData(12, false, true)]
        [InlineData(3, true, false)]
        [InlineData(151, true, true)]
        [InlineData(320, false, false)]
        [InlineData(32679, false, false)]
        public async void CheckNewQueueNo_PlusValues(int queueNo, bool isBreak, bool isSpecial)
        {
            //System.Diagnostics.Debugger.Launch();

            var data = new QueueData().WithBreak(isBreak).WithSpecial(isSpecial);
            var preparedData = data.WithQueueNo(queueNo).Build();

            var result = await CallNewQueueNo(queueNo, data.WithQueueNo(15).BuildAsList());

            var queueNoString = queueNo.ToString();

            var expectedQueueMessage = String.Concat(preparedData.OwnerInitials, preparedData.QueueNo);
            Assert.Equal(expectedQueueMessage, result.QueueNoMessage);
            Assert.False(result.IsBreak);
            Assert.False(result.IsSpecial);
        }

        [Theory]
        [InlineData(-15)]
        [InlineData(-3)]
        [InlineData(-126)]
        public async void CheckNewQueueNo_MinusValues(int queueNo)
        {
            var data = new QueueData().WithQueueNo(15);
            var preparedData = data.Build();

            var result = await CallNewQueueNo(queueNo, data.BuildAsList());

            var expectedQueueMessage = String.Concat(preparedData.OwnerInitials, preparedData.QueueNo);
            Assert.Equal(expectedQueueMessage, result.QueueNoMessage);
            Assert.False(result.IsBreak);
            Assert.False(result.IsSpecial);
        }

        [Fact]
        public async void CheckNewQueueNo_IsBreakOff()
        {
            var data = new QueueData().WithQueueNo(15);

            var result = await CallNewQueueNo(-1, data.BuildAsList());

            var expectedQueueMessage = "Przerwa";
            Assert.Equal(expectedQueueMessage, result.QueueNoMessage);
            Assert.True(result.IsBreak);
            Assert.False(result.IsSpecial);
        }

        [Fact]
        public async void CheckNewQueueNo_IsBreakOn()
        {
            var data = new QueueData().WithQueueNo(15).WithBreak(true);

            var result = await CallNewQueueNo(-1, data.BuildAsList());

            var expectedQueueMessage = "PB15";
            Assert.Equal(expectedQueueMessage, result.QueueNoMessage);
            Assert.False(result.IsBreak);
            Assert.False(result.IsSpecial);
        }

        [Fact]
        public async void CheckNewQueueNo_IsSpecialOn()
        {
            var data = new QueueData().WithQueueNo(15);
            var preparedData = data.Build();

            var result = await CallNewQueueNo(-2, data.BuildAsList());

            var expectedQueueMessage = String.Concat(preparedData.OwnerInitials, preparedData.QueueNo, "A");
            Assert.Equal(expectedQueueMessage, result.QueueNoMessage);
            Assert.False(result.IsBreak);
            Assert.True(result.IsSpecial);
        }

        [Fact]
        public async void CheckNewQueueNo_IsSpecialOff()
        {
            var data = new QueueData().WithQueueNo(15).WithSpecial(true);
            var preparedData = data.Build();

            var result = await CallNewQueueNo(-2, data.BuildAsList());

            var expectedQueueMessage = String.Concat(preparedData.OwnerInitials, preparedData.QueueNo);
            Assert.Equal(expectedQueueMessage, result.QueueNoMessage);
            Assert.False(result.IsBreak);
            Assert.False(result.IsSpecial);
        }

        [Fact]
        public async void CheckNewQueueNo_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => CallNewQueueNo(15, null));
        }

        #endregion

        #region NewAdditionalInfo Tests

        [Theory]
        [InlineData("")]
        [InlineData("Jest sobie swietnym miko³ajem")]
        [InlineData("Lorem Ipsum jest tekstem stosowanym jako przyk³adowy wype³niacz w przemyœle poligraficznym. Zosta³ po raz pierwszy u¿yty w XV w. przez nieznanego drukarza do wype³nienia tekstem próbnej ksi¹¿ki. Piêæ wieków póŸniej zacz¹³ byæ u¿ywany przemyœle elektronicznym, pozostaj¹c praktycznie niezmienionym. Spopularyzowa³ siê w latach 60. XX w. wraz z publikacj¹ arkuszy Letrasetu, zawieraj¹cych fragmenty Lorem Ipsum, a ostatnio z zawieraj¹cym ró¿ne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker")]
        public async void CheckNewAdditionalInfo(string message)
        {
            var data = new QueueData().WithMessage("blablabla");
            var preparedData = data.Build();
            var result = await CallNewAdditioanlInfo(message, data.BuildAsList());

            Assert.Equal(message, result.AdditionalMessage);
            Assert.Equal(preparedData.QueueNo, result.QueueNo);
            Assert.Equal(preparedData.OwnerInitials, result.OwnerInitials);
            Assert.Equal(preparedData.IsBreak, result.IsBreak);
            Assert.Equal(preparedData.IsSpecial, result.IsSpecial);
            Assert.Equal(preparedData.RoomNo, result.RoomNo);
        }
        #endregion

        #region ChangeUserRoomNo Tests

        [Theory]
        [InlineData(12, 15)]
        [InlineData(13, 13)]
        public void ChangeUserRoomNo_Test(int newRoomNo, int oldRoomNo)
        {
            var data = new QueueData();

            var result = CallChangeUserRoomNo(newRoomNo, oldRoomNo);

            Assert.Equal(newRoomNo, result.RoomNo);
        }

        #endregion
    }
}
