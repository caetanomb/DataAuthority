using DataAuthority.Base64Left.API.CommandHandlers;
using DataAuthority.Base64Left.API.Commands;
using DataAuthority.Domain.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataAuthority.Domain.UnitTest
{    
    public class CreatePayLoadCommandHandlerTest
    {
        private List<PayLoad> _payLoadList = new List<PayLoad>();

        [Fact]
        public async Task Handler_Return_True_Create_Success()
        {
            Mock<IDataAuthorityRepository> repositoryMock
                = new Mock<IDataAuthorityRepository>();
            repositoryMock.Setup(a => a.AddPayLoad(It.IsAny<PayLoad>()))
                .Callback((PayLoad payload) =>
                {
                    PayLoad newPayLoad = new PayLoad(payload.Id + 1, payload.ProvidedPayLoadId, payload.Data);

                    _payLoadList.Add(newPayLoad);
                })
                .Returns(() => {
                    return _payLoadList.OrderBy(a => a.Id).FirstOrDefault();
                });

            CreatePayLoadCommand fakeCommand = new CreatePayLoadCommand(1, "dGVzdGU=");

            var handler = new CreatePayLoadCommandHandler(repositoryMock.Object);
            var canToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeCommand, canToken);

            Assert.True(result);
        }        
    }
}
