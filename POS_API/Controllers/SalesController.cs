using Clean_Architecture_Soufiane.Application.Sale.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace POS_API.Controllers
{
    public class SalesController : ApiControllerBase
    {
      

        [HttpGet]
        public async Task<ActionResult<int>> Create()
        {
            return await Mediator.Send(new CreateSaleCommand());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AddItemToSaleCommand command)
        {
            if (id != command.idSale)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(int id, ValidateVenteAsCacheCommand command)
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
