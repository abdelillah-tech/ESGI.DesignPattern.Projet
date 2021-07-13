using System;
using System.Collections.Generic;

namespace ESGI.DesignPattern.Projet
{
    public class Loan
    {
        private double _commitment;
        private DateTime? _expiry;
        private DateTime? _maturity;
        IList<Payment> _payments = new List<Payment>();
        private DateTime _start = DateTime.Now;
        private const long MILLIS_PER_DAY = 86400000;
        private const long DAYS_PER_YEAR = 365;
        private const double RISK_FACTOR = 0.03;
        private const double UNUSED_RISK_FACTOR = 0.01;
        private double _unusedPercentage = 1.0;

        public Loan(
            double commitment, 
            DateTime start, 
            DateTime? expiry, 
            DateTime? maturity)
        {
            this._expiry = expiry;
            this._commitment = commitment;
            this._start = start;
            this._maturity = maturity;
        }

        public static Loan NewTermLoan(
            double commitment, 
            DateTime start, 
            DateTime maturity)
        {
            return new Loan(commitment,
                start, 
                null,
                maturity);
        }

        public static Loan NewRevolver(
            double commitment, 
            DateTime start, 
            DateTime expiry)
        {
            return new Loan(commitment,
                start, 
                expiry,
                null);
        }

        public static Loan NewAdvisedLine(
            double commitment, 
            DateTime start, 
            DateTime expiry, 
            int riskRating)
        {
            if (riskRating > 3) return null;
            Loan advisedLine = new Loan(
                commitment, 
                start, 
                expiry,
                null);
            advisedLine.SetUnusedPercentage(0.1);
            return advisedLine;
        }

        public void Payment(
            double amount, 
            DateTime paymentDate)
        {
            _payments.Add(new Payment(amount, paymentDate));
        }

        public double Capital()
        {
            if (_expiry == null && _maturity != null)
                return _commitment * Duration() * RISK_FACTOR;
            if (_expiry != null && _maturity == null)
            {
                if (GetUnusedPercentage() != 1.0)
                {
                    return _commitment * GetUnusedPercentage() * Duration() * RISK_FACTOR;
                }
                return _commitment * Duration() * UNUSED_RISK_FACTOR;
            }
            return 0.0;
        }

        public double Duration()
        {
            if (_expiry == null && _maturity != null)
            {
                return WeightedAverageDuration();
            }
            
            if (_expiry != null && _maturity == null)
            {
                return YearsTo(_expiry);
            }
            
            return 0.0;
        }

        private double WeightedAverageDuration()
        {
            double weightedAverage = 0.0;
            double sumOfPayments = 0.0;

            if (_commitment == 0.0) return 0.0;
            
            foreach (var payment in _payments)
            {
                sumOfPayments += payment.Amount;
                weightedAverage += YearsTo(payment.Date) * payment.Amount;
            }
            
            return weightedAverage / sumOfPayments;
        }

        private double YearsTo(DateTime? endDate)
        {
            return (double)((endDate?.Ticks - _start.Ticks) / MILLIS_PER_DAY / DAYS_PER_YEAR);
        }

        private double GetUnusedPercentage()
        {
            return _unusedPercentage;
        }

        public void SetUnusedPercentage(double unusedPercentage)
        {
            _unusedPercentage = unusedPercentage;
        }
    }

    public interface ILoan
    {
        double Duration();
        double Capital();
    }

    public class NewTermLoan: Loan, ILoan
    {
        public double Duration()
        {
            throw new NotImplementedException();
        }

        public double Capital()
        {
            throw new NotImplementedException();
        }

        public NewTermLoan(double commitment, DateTime start, DateTime? expiry, DateTime? maturity) : base(commitment, start, expiry, maturity)
        {
        }
    }
    
    public class NewRevolver: Loan, ILoan
    {
        public double Duration()
        {
            throw new NotImplementedException();
        }

        public double Capital()
        {
            throw new NotImplementedException();
        }

        public NewRevolver(double commitment, DateTime start, DateTime? expiry, DateTime? maturity) : base(commitment, start, expiry, maturity)
        {
        }
    }
    
    public class NewAdvisedLine: Loan, ILoan
    {
        public double Duration()
        {
            throw new NotImplementedException();
        }

        public double Capital()
        {
            throw new NotImplementedException();
        }

        public NewAdvisedLine(double commitment, DateTime start, DateTime? expiry, DateTime? maturity) : base(commitment, start, expiry, maturity)
        {
        }
    }
}