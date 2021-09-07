using Clean_Architecture_Soufiane.Application.Sale.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace POS_API.Controllers
{
    public class SalesController : ApiControllerBase
    {
      

        [HttpPost("CreateSale")]
        public async Task<ActionResult<int>> Create()
        {
            return await Mediator.Send(new CreateSaleCommand());
        }

        [HttpPut("[action] {id}")]
        public async Task<ActionResult> AddItemToSale(int id, AddItemToSaleCommand command)
        {
            if (id != command.idSale)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action] {id}")]
        public async Task<ActionResult> ValidateVenteAsCache(int id, ValidateVenteAsCacheCommand command)
        {
            if (id != command.idSale)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

     
    }
}
