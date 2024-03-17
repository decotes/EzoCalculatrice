using EzoCalculatrice.API.Utils;

namespace EzoCalculatrice.API.Tests.UtilsTests
{
    public class ExpressionDividerTests
    {
        [Theory]
        [InlineData("3 + 4", new string[] { "3", "+", "4" })]
        [InlineData("2 * (4 + 5) / (7 - 2)", new string[] { "2", "*", "(", "4", "+", "5", ")", "/", "(", "7", "-", "2", ")" })]
        public void DivideExpression_ConvertsCorrectly(string input, string[] expected)
        {
            // Arrange
            var operators = new List<IOperator>
            {
                new AdditionOperator(),
                new SubtractionOperator(),
                new MultiplicationOperator(),
                new DivisionOperator()
            };

            string operatorSymbols = "";

            foreach(IOperator op in operators)
            {
                operatorSymbols += @"\" + op.Symbol;
            }

            // Act
            string[] actualElements = ExpressionDivider.DivideExpression(input, operatorSymbols);

            // Assert
            Assert.Equal(expected, actualElements);
        }
    }
}
