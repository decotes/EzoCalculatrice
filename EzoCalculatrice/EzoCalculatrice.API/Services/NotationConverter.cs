using EzoCalculatrice.API.Operators;
using System.Globalization;
using System.Net;

namespace EzoCalculatrice.API.Services
{
    // Cette classe implémente la conversion d'une expression infixée en postfixée
    public class NotationConverter
    {
        // Dictionnaire pour stocker les opérateurs et leur logique associée
        private readonly Dictionary<string, IOperator> _operators;

        // Constructeur qui initialise le dictionnaire d'opérateurs
        public NotationConverter(IEnumerable<IOperator> operators)
        {
            _operators = new Dictionary<string, IOperator>();
            foreach (var op in operators)
            {
                _operators[op.Symbol] = op;
            }
        }

        // Méthode pour convertir une expression infixe en postfixe
        public string ConvertToPostfix(string infix)
        {
            var outputQueue = new Queue<string>(); // La file pour stocker l'ordre de sortie
            var operatorStack = new Stack<string>(); // La pile pour stocker temporairement les opérateurs

            // Créer une chaîne des symboles d'opérateurs pour une vérification facile
            string operatorSymbols = string.Join("", _operators.Keys);

            // Diviser l'expression en éléments basés sur les opérateurs et les nombres
            string[] expression = ExpressionDivider.DivideExpression(infix, string.Join(@"\", _operators.Keys));

            foreach(string element in expression)
            {
                // Si c'est un opérateur
                if (operatorSymbols.Contains(element)) 
                {
                    if (operatorStack.Count > 0) 
                    {
                        var stackElement = operatorStack.Peek();

                        if (stackElement == "(") 
                            operatorStack.Pop();
                        else
                            // Dépiler les opérateurs de la pile dans la file de sortie jusqu'à trouver un opérateur avec une priorité inférieure ou jusqu'à ce que la pile soit vide
                            while (_operators[stackElement].Precedence >= _operators[element].Precedence && operatorStack.Count > 0)
                            {
                                outputQueue.Enqueue(operatorStack.Pop());
                                if (operatorStack.Count > 0)
                                    stackElement = operatorStack.Peek();
                            }
                    }
                    operatorStack.Push(element);
                }
                else
                {
                    // Si c'est une parenthèse ouvrante, insérer directement dans la file
                    if (element == "(") operatorStack.Push(element);
                    else
                    {
                        // Si c'est une parenthèse fermante, dépiler la pile jusqu'a trouver la parenthèse ouvrante et passer chaque opérateur á la file (sans mettre la parenthèse ouvrante)
                        if (element == ")")
                        {
                            if (operatorStack.Count > 0)
                            {
                                string stackElement = "";
                                do
                                {
                                    stackElement = operatorStack.Pop();
                                    if (stackElement != "(")
                                        outputQueue.Enqueue(stackElement);
                                }
                                while (stackElement != "(" && operatorStack.Count > 0);
                            }
                        }
                        else // Si c'est un numéro, insérer directement dans la file de sortie
                        {
                            outputQueue.Enqueue(element);
                        }
                    }
                }
            }

            // Dépiler tout ce qui reste dans la pile dans la file de sortie.
            while (operatorStack.Count > 0)
                outputQueue.Enqueue(operatorStack.Pop());

            return string.Join(" ", outputQueue);
        }

        // Méthode pour évaluer une opération avec le symbole de l'opérateur et les opérandes donnés.
        public double EvaluateOperation(string operatorSymbol, double leftOperand, double rightOperand)
        {
            if (_operators.TryGetValue(operatorSymbol, out IOperator op))
            {
                return op.Evaluate(leftOperand, rightOperand);
            }

            throw new ArgumentException($"Opérateur non pris en charge : {operatorSymbol}", nameof(operatorSymbol));
        }
    }
}
