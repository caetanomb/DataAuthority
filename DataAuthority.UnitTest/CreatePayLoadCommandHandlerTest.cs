using DataAuthority.ApplicationService.CommandHandlers;
using DataAuthority.ApplicationService.Commands;
using DataAuthority.Domain;
using DataAuthority.Domain.Repository;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DataAuthority.UnitTest
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
                    PayLoad newPayLoad = new PayLoad(payload.Id + 1, payload.ProvidedPayLoadId, "Left", payload.Data);

                    _payLoadList.Add(newPayLoad);
                })
                .Returns(() => {
                    return _payLoadList.OrderBy(a => a.Id).FirstOrDefault();
                });

            CreatePayLoadCommand fakeCommand = new CreatePayLoadCommand(1, "Left", "dGVzdGU=");

            var handler = new CreatePayLoadCommandHandler(repositoryMock.Object);
            var canToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeCommand, canToken);

            Assert.True(result);
        }        
    }
}
