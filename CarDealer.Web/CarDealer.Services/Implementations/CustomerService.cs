
namespace CarDealer.Services.Implementations
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> Ordered(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDay)
                        .ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDay)
                        .ThenBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid Order direction {order}");

            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Name= c.Name,
                    BitrhDay = c.BirthDay,
                    IsYoungDriver =c.IsYoungDriver
                })
                .ToList();

        }
    }
}
