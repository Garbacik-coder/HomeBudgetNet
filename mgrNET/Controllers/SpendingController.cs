using mgrNET.Requests;
using mgrNET.Store;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mgrNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpendingController : ControllerBase
    {
        private readonly ISpendingStore spendingStore;

        public SpendingController(ISpendingStore spendingStore)
        {
            this.spendingStore = spendingStore;
        }

        // GET: api/spendings
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Domain.Spending>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var storeSpendings = await spendingStore.GetAll();
            var spendings = storeSpendings.Select(x => new Domain.Spending(x));
            return Ok(spendings);
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Domain.Spending), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Domain.Spending), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var spending = await spendingStore.GetById(id);

            return Ok(new Domain.Spending(spending));
        }

        // POST api/movies
        [HttpPost]
        [Consumes(typeof(CreateSpending), "application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] CreateSpending request)
        {
            try
            {
                await spendingStore.Create(new CreateSpendingParams(
                    request.id,
                    request.name,
                    request.value,
                    request.category,
                    request.date
                    ));
            }
            catch (DuplicateKeyException)
            {
                return Conflict();
            }

            return Ok();
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        [Consumes(typeof(UpdateSpending), "application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateSpending request)
        {
            await spendingStore.Update(id, new UpdateSpendingParams(
                request.name,
                request.value,
                request.category,
                request.date
                ));

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await spendingStore.Delete(id);
            return Ok();
        }
    }

}
