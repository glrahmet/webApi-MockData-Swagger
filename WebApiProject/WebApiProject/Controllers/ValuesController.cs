
using Bogus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApiProject.Model;

namespace WebApiProject.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(GetCustomers());
        }


        //Mock data için eklendi 
        private IEnumerable<Customer> GetCustomers()
        {
            Randomizer.Seed = new Random(123456);
            var ordergenerator = new Faker<Order>()
             .RuleFor(o => o.Id, Guid.NewGuid)
             .RuleFor(o => o.Date, f => f.Date.Past(3))
             .RuleFor(o => o.OrderValue, f => f.Finance.Amount(0, 10000))
             .RuleFor(o => o.Shipped, f => f.Random.Bool(0.9f));
            var customerGenerator = new Faker<Customer>()
             .RuleFor(c => c.Id, Guid.NewGuid())
             .RuleFor(c => c.Name, f => f.Company.CompanyName())
             .RuleFor(c => c.Address, f => f.Address.FullAddress())
             .RuleFor(c => c.City, f => f.Address.City())
             .RuleFor(c => c.Country, f => f.Address.Country())
             .RuleFor(c => c.ZipCode, f => f.Address.ZipCode())
             .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
             .RuleFor(c => c.Email, f => f.Internet.Email())
             .RuleFor(c => c.ContactName, (f, c) => f.Name.FullName());             
            return customerGenerator.Generate(100);
        }
    }
}
