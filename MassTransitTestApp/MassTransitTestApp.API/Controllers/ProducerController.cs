using MassTransientTestApp.Shared;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MassTransitTestApp.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public ProducerController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public Task Send(int messageCount)
        {
            var id = Guid.NewGuid();

            for (int i = 0; i < messageCount; i++)
            {
                Message message = new() { Text = i.ToString() };

                if (i % 2 == 0) message.Id = Guid.NewGuid();
                else message.Id = id;

                _publishEndpoint.Publish(message);
            }

            return Task.CompletedTask;
        }
    }
}