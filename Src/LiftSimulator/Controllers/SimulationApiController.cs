using System.Collections.Generic;
using System.Web.Http;
using LiftSimulator.Core;
using LiftSimulator.Core.Models;

namespace LiftSimulator.Controllers
{
    [RoutePrefix("api/v1/simulator")]
    public class SimulationApiController : ApiController
    {
        [HttpPost]
        [Route("request-lift")]
        public IHttpActionResult RequestLift([FromBody] CreateLiftRequest request)
        {
            var result = SimulationRunner.Instance.RequestLift(
                request.Tick, 
                request.PeopleCount, 
                request.SourceFloorNumber,
                request.TargetFloorNumber);

            return Ok(result);
        }

        [HttpGet]
        [Route("ticks/{tick:int}")]
        public IHttpActionResult GetSimulationTick([FromUri] int tick)
        {
            var summary = SimulationRunner.Instance.UpdateSimulationTick(tick);

            var result = new UpdateResult
                         {
                             Tick = tick,
                             Context = summary.Context,
                             SummaryItems = summary.Items
                         };

            return Ok(result);
        }

        [HttpGet]
        [Route("reset")]
        public IHttpActionResult Reset()
        {
            var context = SimulationRunner.Instance.Reset();
            var result = new UpdateResult
            {
                Context = context
            };
            return Ok(result);
        }

    }

    public class CreateLiftRequest
    {
        public int Tick { get; set; }
        public int PeopleCount { get; set; }
        public int SourceFloorNumber { get; set; }
        public int TargetFloorNumber { get; set; }
    }

    public class UpdateCommand
    {
        public int Tick { get; set; }
        public IEnumerable<LiftRequest> Requests { get; set; }
    }

    public class UpdateResult
    {
        public int Tick { get; set; }
        public SimulationContext Context { get; set; }
        public IEnumerable<SummaryItem> SummaryItems { get; set; }
    }
}