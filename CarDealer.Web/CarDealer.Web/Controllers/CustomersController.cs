namespace CarDealer.Web.Controllers
{
    using Services;
    using Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Customers;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("customers/all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "descending"
                ? OrderDirection.Descending
                : OrderDirection.Ascending; 

            var customers = this.customers.Ordered(orderDirection);

            return View(new AllCustomersModel

            {
                Customers = customers,
                OrderDirection = orderDirection

            });
        }

    }
}
