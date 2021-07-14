using System;

namespace ESGI.DesignPattern.Projet
{
    public enum LoanType
    {
        NewTermLoan,
        NewRevolver,
        NewAdvisedLine
    }
    public class LoanFactory
    {
        public static ILoan create(LoanType loanType, double commitment, DateTime start, DateTime end, int riskRating)
        {
            switch (loanType)
            {
                    case LoanType.NewTermLoan:
                        return new NewTermLoan(commitment, start, end);
                    case LoanType.NewRevolver:
                        return new NewRevolver(commitment, start, end);
                    case LoanType.NewAdvisedLine:
                    {
                        if (riskRating < 3)
                            return new NewAdvisedLine(commitment, start, end);
                        return null;
                    }
                    default:
                        return null;
            }
        }

    }
}