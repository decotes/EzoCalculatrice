namespace EzoCalculatrice.API.Operators
{
    public class AdditionOperator : IOperator
    {
        public int Precedence => 1;

        public string Symbol => "+";

        public double Evaluate(double leftOperand, double rightOperand) => leftOperand + rightOperand;
    }
}

