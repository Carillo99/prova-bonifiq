using Azure;
using Microsoft.AspNetCore.Mvc;
using ProvaPub.Domain.DTO;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Models;

namespace ProvaPub.Api.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante é em relação à data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso horário do Brasil. 
    /// Demonstre como você faria isso.
    /// </summary>
    [ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
    {
        private readonly IOrderService _orderService;

        public Parte3Controller(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("orders")]
        public async Task<IActionResult> PlaceOrder(OrderDTO mode)
        {
            var response = await _orderService.PayOrder(mode);
            if (response.IsSucceed) return Ok(response);
            return BadRequest(response);
        }
	}
}
