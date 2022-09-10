using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class CashFlow
    {
        public CashFlow()
        {
        }
        public int TransactionId { get; set; }

        // BASIC DATA FIELD
        public string TransactionType { get; set; }
        public int TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionStatus { get; set; }

        // ADVANCED
        public Users User { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}
