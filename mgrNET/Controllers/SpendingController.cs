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
            return Ok(storeSpendings);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Domain.Spending), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var spending = await spendingStore.GetById(id);
            if (spending == null)
            {
                return NotFound();
            }

            var spending2 = new Domain.Spending(spending);
            var json = System.Text.Json.JsonSerializer.Serialize(spending2);
            Console.WriteLine(json);  // Log the serialized JSON

            return Ok(spending2);
        }
        
        // POST api/movies
        [HttpPost]
//        [Consumes(typeof(CreateSpending), "application/json")]
        [Consumes("application/json")]
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
        public async Task<IActionResult> Put(int id, [FromBody] UpdateSpending request)
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
        public async Task<IActionResult> Delete(int id)
        {
            await spendingStore.Delete(id);
            return Ok();
        }
    }

}
