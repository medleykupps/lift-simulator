using System;
using System.Configuration;
using System.Web.Http;
using LiftSimulator.Core;
using LiftSimulator.Models;

namespace LiftSimulator.Controllers
{
    [RoutePrefix("api/v1/simulator")]
    public class SimulationApiController : ApiController
    {
        private IRepository _repository;

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
            if (summary != null)
            {
                CommitSummaryToDatabase(summary);
            }

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
            Repository.ResetSummaryItems();
            var result = new UpdateResult
            {
                Context = context
            };
            return Ok(result);
        }

        private void CommitSummaryToDatabase(TickSummary summary)
        {
            if (summary.Items != null)
            {
                foreach (var item in summary.Items)
                {
                    Repository.AddSummaryItem(item);
                }
            }
        }

        protected IRepository Repository
        {
            get
            {
                if (_repository != null)
                {
                    return _repository;
                }

                var connectionString = ConfigurationManager.ConnectionStrings["LiftSimulator"];
                if (connectionString == null)
                {
                    throw new ApplicationException("Missing connection string for LiftSimulator db");
                }

                _repository = new Repository(connectionString.ConnectionString);

                return _repository;
            }
        }
    }
}