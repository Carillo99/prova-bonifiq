﻿using Microsoft.AspNetCore.Mvc;
using ProvaPub.Domain.Interfaces.IServices;

namespace ProvaPub.Api.Controllers
{

    /// <summary>
    /// O Código abaixo faz uma chmada para a regra de negócio que valida se um consumidor pode fazer uma compra.
    /// Crie o teste unitário para esse Service. Se necessário, faça as alterações no código para que seja possível realizar os testes.
    /// Tente criar a maior cobertura possível nos testes.
    /// 
    /// Utilize o framework de testes que desejar. 
    /// Crie o teste na pasta "Tests" da solution
    /// </summary>
    [ApiController]
	[Route("[controller]")]
	public class Parte4Controller :  ControllerBase
    {
        private readonly ICustomerService _customerService;
        public Parte4Controller(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("CanPurchase")]
		public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
		{
			return await _customerService.CanPurchase(customerId, purchaseValue, DateTime.UtcNow);
		}
	}
}
