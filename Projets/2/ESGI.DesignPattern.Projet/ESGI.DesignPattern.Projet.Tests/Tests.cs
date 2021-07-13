using System;
using Xunit;

namespace ESGI.DesignPattern.Projet.Tests
{
    public class Tests
    {
        private readonly int LOW_RISK_TAKING = 2;
        private readonly int HIGH_RISK_TAKING = 5;
        private readonly double LOAN_AMOUNT = 10000.00;
        private readonly double TWO_DIGIT_PRECISION = 0.001;

        [Fact()]
        public void payment_is_constructed_correctly()
        {
            var christmasDay = new DateTime(2010, 12, 25);
            var payment = new Payment(1000.0, christmasDay);

            Assert.Equal(1000, payment.Amount);
            Assert.Equal(christmasDay, payment.Date);
        }

        [Fact()]
        public void test_term_loan_same_payments()
        {
            DateTime start = November(20, 2003);
            DateTime maturity = November(20, 2006);

            Loan termLoan = Loan.NewTermLoan(LOAN_AMOUNT, start, maturity);
            termLoan.Payment(1000.00, November(20, 2004));
            termLoan.Payment(1000.00, November(20, 2005));
            termLoan.Payment(1000.00, November(20, 2006));

            Assert.Equal(20027, termLoan.Duration(), (int)TWO_DIGIT_PRECISION);
            Assert.Equal(6008100, termLoan.Capital(), (int)TWO_DIGIT_PRECISION);
        }

        [Fact()]
        public void test_revolver_loan_same_payments()
        {
            DateTime start = November(20, 2003);
            DateTime expiry = November(20, 2007);

            Loan revolverLoan = Loan.NewRevolver(LOAN_AMOUNT, start, expiry);
            revolverLoan.Payment(1000.00, November(20, 2004));
            revolverLoan.Payment(1000.00, November(20, 2005));
            revolverLoan.Payment(1000.00, November(20, 2006));

            Assert.Equal(40027, revolverLoan.Duration(), (int)TWO_DIGIT_PRECISION);
            Assert.Equal(4002700, revolverLoan.Capital(), (int)TWO_DIGIT_PRECISION);
        }

        [Fact()]
        public void test_advised_line_loan_same_payments()
        {
            DateTime start = November(20, 2003);
            DateTime maturity = November(20, 2006);
            DateTime expiry = November(20, 2007);

            Loan advisedLineLoan = Loan.NewAdvisedLine(LOAN_AMOUNT, start, expiry, LOW_RISK_TAKING);
            advisedLineLoan.Payment(1000.00, November(20, 2004));
            advisedLineLoan.Payment(1000.00, November(20, 2005));
            advisedLineLoan.Payment(1000.00, November(20, 2006));

            Assert.Equal(40027, advisedLineLoan.Duration(), (int)TWO_DIGIT_PRECISION);
            Assert.Equal(1200810, advisedLineLoan.Capital(), (int)TWO_DIGIT_PRECISION);
        }

        private static DateTime November(int dayOfMonth, int year)
        {
            return new DateTime(year, 11, dayOfMonth);
        }
    }
}

