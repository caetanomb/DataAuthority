using DataAuthority.ApplicationService.Commands;
using DataAuthority.Base64Left.API.Controllers;
using DataAuthority.Base64Left.API.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DataAuthority.UnitTest
{
    public class DataControllerTest
    {
        private Mock<IMediator> _mediatorMock;

        public DataControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Post_PayLoad_Success()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreatePayLoadCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(true));


            DataController dataController = new DataController(_mediatorMock.Object);
            IActionResult actionResult = await dataController.Post(1, "{\"offset\": 3,\"lenght\": 2}");

            Assert.Equal(((CreatedAtActionResult)actionResult).StatusCode, (int)System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task Post_PayLoad_BadRequest()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreatePayLoadCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(false));


            DataController dataController = new DataController(_mediatorMock.Object);
            IActionResult actionResult = await dataController.Post(1, "{\"offset\": 3,\"lenght\": 2}");

            Assert.Equal(((BadRequestResult)actionResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }
    }
}
