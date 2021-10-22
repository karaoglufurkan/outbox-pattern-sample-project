using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OutboxService.Business;
using OutboxService.Models;
using Shared.Models;

namespace OutboxService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OutboxController : ControllerBase
    {
        private IOutboxBusiness _outboxBusiness;
        public OutboxController(IOutboxBusiness outboxBusiness)
        {
            _outboxBusiness = outboxBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnpublishedEvents()
        {
            var events = await _outboxBusiness.GetUnpublishedEvents();

            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(OutboxEventRequest request)
        {
            var newEvent = new OutboxEvent(request.Message, request.Type, Guid.NewGuid(), DateTime.Now);
            await _outboxBusiness.AddEvent(newEvent);

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEvents(List<UpdateEventStateRequest> request)
        {
            await _outboxBusiness.UpdateEvents(request);

            return Ok();
        }
    }

}