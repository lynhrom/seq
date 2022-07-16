using Application.Common;
using Application.Services;
using Infrastructure.Handlers.Markets;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class NotificationHub: Hub
    {
        private IMediator _mediator;

        public NotificationHub(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task SendData(string ticker, string source)
        {
            long tickerId, sourceId = 0;
            long.TryParse(ticker, out tickerId);
            long.TryParse(source, out sourceId);
            var items = await _mediator.Send(new GetPagedPriceQuery
            {
                Request = new ListPagedPriceRequest(5, 0, sourceId, tickerId)
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, string.Format("{0};#{1}", tickerId, sourceId));
            await Clients.Caller.SendAsync(SignalRContants.ReceiveData, items);
        }
    }
}
