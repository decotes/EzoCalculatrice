using System;

namespace EzoCalculatrice.API.Services
{
    public class ExpressionEvaluator
    {
        private NotationConverter converter;

        public ExpressionEvaluator(NotationConverter converter)
        {
            this.converter = converter;
        }

        public double Evaluate(string infixExpression)
        {
            var postfixExpression = converter.ConvertToPostfix(infixExpression);

            var stack = new Stack<double>();

            var elements = postfixExpression.Replace('.', ',').Split(' '); 
            foreach(var element in elements)
            {
                if (double.TryParse(element, out double token))
                {
                    stack.Push(token);
                }
                else
                {
                    var rightOperand = stack.Pop(); 
                    var leftOperand = stack.Pop();

                    var result = converter.EvaluateOperation(element, leftOperand, rightOperand);
                    stack.Push(result);
                }
            }

            return stack.Pop();
        }
    }
}