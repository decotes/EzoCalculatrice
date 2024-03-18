using EzoCalculatrice.API.Operators;
using EzoCalculatrice.API.Services;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace EzoCalculatrice.API.Tests.UtilsTests
{
    public class NotationConverterTests
    {
        [Theory]
        [InlineData("3 + 4", "3 4 +")]
        [InlineData("2 + 3 * 4", "2 3 4 * +")]
        [InlineData("2 * 3 + 4", "2 3 * 4 +")]
        [InlineData("(1 + 2) * 3", "1 2 + 3 *")]
        [InlineData("3 + (4 * 2) - 7", "3 4 2 * + 7 -")]
        [InlineData("(1 + 2) * (3 + 4)", "1 2 + 3 4 + *")]
        [InlineData("12 + 34", "12 34 +")]
        [InlineData("123 * 456", "123 456 *")]
        [InlineData("(12 + 34) * 56", "12 34 + 56 *")]
        [InlineData("3.5 + 4.2 * 2", "3.5 4.2 2 * +")]
        public void ConvertToPostfix_SimpleExpression_ConvertsCorrectly(string input, string expected)
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

            // Act
            var result = converter.ConvertToPostfix(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
