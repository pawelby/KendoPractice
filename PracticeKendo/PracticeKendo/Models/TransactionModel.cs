using System;

namespace PracticeKendo.Models
{
    public class TransactionModel
    {
        public string Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransTypeName { get; set; }
        public string CategoryName { get; set; }
        public decimal Ammount { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Details { get; set; }
    }
}