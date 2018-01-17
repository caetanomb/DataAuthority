using MediatR;

namespace DataAuthority.ApplicationService.Commands
{
    /// <summary>
    /// This DTO structure transports data to CreatePayLoadCommandHandler
    /// </summary>
    public class CreatePayLoadCommand : IRequest<bool>
    {
        public int ProvidedPayLoadId { get; set; }
        public string Data { get; set; }
        public string Origin { get; set; }

        public CreatePayLoadCommand(int providedPayLoadId, string origin, string data)
        {
            ProvidedPayLoadId = providedPayLoadId;
            Origin = origin;
            Data = data;
        }

        public CreatePayLoadCommand(string origin, string data)
        {
            Origin = origin;
            Data = data;
        }
    }
}
