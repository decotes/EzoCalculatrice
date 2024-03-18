using EzoCalculatrice.API.Operators;
using EzoCalculatrice.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzoCalculatrice.API.Tests.UtilsTests
{
    public class ExpressionEvaluatorTests
    {
        [Theory]
        [InlineData("3 + 4", 7)]
        [InlineData("30 + 44", 74)]
        [InlineData("2 + 3 * 6", 20)]
        [InlineData("2 * 3 + 6", 12)]
        [InlineData("(2 + 3) * 6", 30)]
        [InlineData("3 + (4 * 2) - 7", 4)]
        [InlineData("(1 + 2) * (3 + 4)", 21)]
        [InlineData("12 + 34", 46)]
        [InlineData("123 * 456", 56088)]
        [InlineData("(12 + 34) * 56", 2576)]
        [InlineData("3.5 + 4.2 * 2", 11.9)]
        public void Evaluate_ComputeCorrectly(string input, double expected)
        {

            // Arrange

            var operators = new List<IOperator>
            {
                new AdditionOperator(),
                new SubtractionOperator(),
                new MultiplicationOperator(),
                new DivisionOperator()
            };
            var converter = new NotationConverter(operators);

            var evaluator = new ExpressionEvaluator(converter);

            // Act
            var result = evaluator.Evaluate(input);

            // Aserción
            Assert.Equal(expected, result);
        }
    }
}
