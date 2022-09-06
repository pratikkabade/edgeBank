using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public class UserCashFlow
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }

        public string FirstName { get; set; }
        public int TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionStatus { get; set; }

    }
}
