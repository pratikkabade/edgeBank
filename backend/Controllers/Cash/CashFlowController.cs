using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CashFlowController : Controller
    {
        private DataBaseContext cash_context;
        public CashFlowController(DataBaseContext cash_context)
        {
            this.cash_context = cash_context;
        }

        // CREATE
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public string Post([FromBody] CashFlow transaction)
        {
            this.cash_context.CashFlows.Add(transaction);
            this.cash_context.SaveChanges();
            return "Money sent successfully!";
        }

        // GET BY ID
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IEnumerable<UserCashFlow> Get(int id)
        {
            var deptEmployees = from c in cash_context.Set<CashFlow>()
                                join u in cash_context.Set<Users>()
                                on c.UserId equals u.UserId
                                where c.TransactionId == id
                                select new UserCashFlow
                                {
                                    TransactionId = c.TransactionId,
                                    TransactionType = c.TransactionType,
                                    UserId = u.UserId,
                                    FirstName = u.FirstName,
                                    Email = u.Email,
                                    TransactionAmount = c.TransactionAmount,
                                    TransactionDate = c.TransactionDate,
                                    TransactionStatus = c.TransactionStatus
                                };
            return deptEmployees.ToList();
        }


        // GET BY ID
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<UserCashFlow> Get()
        {
            var deptEmployees = from c in cash_context.Set<CashFlow>()
                                join u in cash_context.Set<Users>()
                                on c.UserId equals u.UserId
                                select new UserCashFlow
                                {
                                    TransactionId = c.TransactionId,
                                    TransactionType = c.TransactionType,
                                    UserId = u.UserId,
                                    FirstName = u.FirstName,
                                    Email = u.Email,
                                    TransactionAmount = c.TransactionAmount,
                                    TransactionDate = c.TransactionDate,
                                    TransactionStatus = c.TransactionStatus
                                };
            return deptEmployees.ToList();
        }
    }
}
