namespace EzoCalculatrice.API.Utils
{
    public class MultiplicationOperator : IOperator
    {
        public int Precedence => 2;
     
        public string Symbol => "*";
        
        public double Evaluate(double leftOperand, double rightOperand) => leftOperand * rightOperand;
    }
}
