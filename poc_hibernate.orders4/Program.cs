using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using poc_hibernate.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_hibernate.orders
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=.;Initial Catalog=NhibernateDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var config = new Configuration();

            config.DataBaseIntegration(x =>
            {
                x.ConnectionString = connectionString;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
            });

            config.AddAssembly(typeof(DataBaseProvider).Assembly.GetName().Name);

            var sessionFactory = config.BuildSessionFactory();

            Guid id = default;

            using(var session = sessionFactory.OpenSession())
            using(var txn = session.BeginTransaction())
            {
                var customer = CreateCustomer();
                try
                {
                    session.Save(customer);

                    id = customer.Id;
                    txn.Commit();
                }
                catch (Exception ex)
                {
                    txn.Rollback();
                    throw;
                }
            }

            using(var session  = sessionFactory.OpenSession())
            {
                var customer = session.Get<Customer>(id);
                var customer2 = session.Load<Customer>(id);
            }
       }

        private static Customer CreateCustomer()
        {
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Points = 100,
                HasGoldStatus = true,
                MemberSince = new DateTime(2012, 1, 1),
                CreditRating = CustomerCreditRating.Good,
                AverageRating = 42.42424242,
                Address = CreateLocation()
            };

            var order1 = new Order
            {
                Ordered = DateTime.Now
            };

            customer.AddOrder(order1);

            var order2 = new Order
            {
                Ordered = DateTime.Now.AddDays(-1),
                Shipped = DateTime.Now,
                ShipTo = CreateLocation()
            };

            customer.AddOrder(order2);
            return customer;
        }

        private static Location CreateLocation()
        {

            return new Location
            {
                Street = "123 Somewhere Avenue",
                City = "Nowhere",
                Province = "Alberta",
                Country = "Canada"
            };
        }
    }
}
