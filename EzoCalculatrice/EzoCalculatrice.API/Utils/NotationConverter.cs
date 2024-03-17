namespace EzoCalculatrice.API.Utils
{
    public class NotationConverter
    {
        private readonly Dictionary<string, IOperator> _operators;

        public NotationConverter(IEnumerable<IOperator> operators)
        {
            _operators = new Dictionary<string, IOperator>();
            foreach (var op in operators)
            {
                _operators[op.Symbol] = op;
            }
        }

        public string ConvertToPostfix(string infix)
        {
            var outputQueue = new Queue<string>();
            var operatorStack = new Stack<string>();

            string operatorSymbols = string.Join("", _operators.Keys);

            string[] expression = ExpressionDivider.DivideExpression(infix, string.Join(@"\", _operators.Keys));

            foreach(string element in expression)
            {
                if (operatorSymbols.Contains(element)) // Es un operador
                {
                    if (operatorStack.Count > 0) // Si hay operadores en la pila
                    {
                        var stackElement = operatorStack.Peek();

                        if (stackElement == "(")
                            operatorStack.Pop();
                        else
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
                    if (element == "(") operatorStack.Push(element);
                    else
                    {
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
                        else
                        {
                            outputQueue.Enqueue(element);
                        }
                    }
                }
            }

            while (operatorStack.Count > 0)
                outputQueue.Enqueue(operatorStack.Pop());

            return string.Join(" ", outputQueue);
        }

        public double EvaluateOperation(string operatorSymbol, double leftOperand, double rightOperand)
        {
            if (_operators.TryGetValue(operatorSymbol, out IOperator op))
            {
                return op.Evaluate(leftOperand, rightOperand);
            }

            throw new ArgumentException($"Operador no soportado: {operatorSymbol}", nameof(operatorSymbol));
        }
    }
}
