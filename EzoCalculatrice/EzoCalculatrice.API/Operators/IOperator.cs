﻿namespace EzoCalculatrice.API.Operators
{
    public interface IOperator
    {
        int Precedence { get; }

        string Symbol { get; }

        double Evaluate(double leftOperand, double rightOperand);
    }
}
