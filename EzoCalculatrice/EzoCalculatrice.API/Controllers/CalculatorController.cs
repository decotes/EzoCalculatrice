using Microsoft.AspNetCore.Mvc;
using EzoCalculatrice.API.Services;

namespace EzoCalculatrice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ExpressionEvaluator _expressionEvaluator;

        public CalculatorController(ExpressionEvaluator expressionEvaluator)
        {
            _expressionEvaluator = expressionEvaluator;
        }

        [HttpGet("evaluate")]
        public IActionResult EvaluateExpression([FromQuery] string expression)
        {
            try
            {
                var result = _expressionEvaluator.Evaluate(expression);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}