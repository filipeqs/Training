using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Calculator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {

        [HttpGet]
        public decimal Calculate(string operatorValue, decimal firstOperand, decimal secondOperand)
        {
            decimal result;
            switch (operatorValue)
            {
                case "devide":
                    result = firstOperand / secondOperand;
                    break;
                case "*":
                    result = firstOperand * secondOperand;
                    break;
                case "+":
                    result = firstOperand + secondOperand;
                    break;
                case "-":
                    result = firstOperand - secondOperand;
                    break;
                case "=":
                    result = secondOperand;
                    break;
                default:
                    result = secondOperand;
                    break;
            }

            return result;
        }
    }
}
