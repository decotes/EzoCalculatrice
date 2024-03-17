using EzoCalculatrice.API.Utils;

var operators = new List<IOperator>
{
    new AdditionOperator(),
    new SubtractionOperator(),
    new MultiplicationOperator(),
    new DivisionOperator()
};
var converter = new NotationConverter(operators);

string expression = "3 * 4 + 5";

string resultado = converter.ConvertToPostfix(expression);

Console.WriteLine($"Expresión: {expression}");
Console.WriteLine($"Resultado: {resultado}");