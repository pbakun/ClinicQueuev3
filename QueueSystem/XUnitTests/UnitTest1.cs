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

namespace XUnitTests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            //System.Diagnostics.Debugger.Launch();
            _factory = factory;
            var appFactory = new WebApplicationFactory<Startup>().CreateClient();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Patient/12")]
        //[InlineData("/Admin/Rooms/Index")]
        public async Task Test1(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData(12)]
        [InlineData(3)]
        [InlineData(151)]
        [InlineData(320)]
        [InlineData(-775)]
        [InlineData(-3)]
        public async void CheckNewQueueNo(int queueNo)
        {
            //System.Diagnostics.Debugger.Launch();

            //prepare correct data
            int dataQueueNo = queueNo;
            if (queueNo < 0)
            {
                dataQueueNo = queueNo * -1;
            }
            var preparedData = PrepareData(dataQueueNo).FirstOrDefault();
            
            var mockRepo = new Mock<IRepositoryWrapper>();
            mockRepo.Setup(x => x.Queue.FindByCondition(It.IsAny<Expression<Func<Entities.Models.Queue, bool>>>()))
                .Returns(PrepareData(dataQueueNo));

            mockRepo.Setup(x => x.Queue.Update(It.IsAny<Entities.Models.Queue>()));
            mockRepo.Setup(x => x.SaveAsync());

            var mockScopeFactory = new Mock<IServiceScopeFactory>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();

            var sut = new QueueService(mockRepo.Object, mapper, mockScopeFactory.Object);

            var result = await sut.NewQueueNo("1", queueNo);

            var queueNoString = queueNo.ToString();


            var expectedQueueMessage = String.Concat(preparedData.OwnerInitials, preparedData.QueueNo);

            Assert.Equal(expectedQueueMessage, result.QueueNoMessage);
            Assert.True(!preparedData.IsBreak);
            Assert.True(!preparedData.IsSpecial);

        }

        private List<Entities.Models.Queue> PrepareData(int queueNo)
        {
            List<Entities.Models.Queue> output = new List<Entities.Models.Queue>();
            var data = new Entities.Models.Queue()
            {
                QueueNo = queueNo,
                IsBreak = false,
                IsSpecial = false,
                OwnerInitials = "PB",
                RoomNo = 12
            };

            output.Add(data);

            return output;
        }
    }
}
