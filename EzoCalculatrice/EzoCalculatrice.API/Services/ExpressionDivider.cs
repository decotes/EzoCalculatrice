using System.Text.RegularExpressions;

namespace EzoCalculatrice.API.Services
{
    public class ExpressionDivider
    {
        public static string[] DivideExpression(string input, string operatorSymbols)
        {
            string regularExpression = @$"([-+]?\d*\.?\d+|\b\w+\b|[{operatorSymbols}\(\)])";

            MatchCollection matches = Regex.Matches(input, regularExpression);

            List<string> elements = new();
            foreach (Match match in matches.Cast<Match>())
            {
                elements.Add(match.Value);
            }

            elements = elements.Where(element => !string.IsNullOrWhiteSpace(element)).ToList();

            return elements.ToArray();
        }
    }
}
