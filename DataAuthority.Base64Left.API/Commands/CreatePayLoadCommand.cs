using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAuthority.Base64Left.API.Commands
{
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
