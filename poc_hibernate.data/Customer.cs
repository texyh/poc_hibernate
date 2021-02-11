using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_hibernate.data
{
    public class Customer
    {

        public Customer()
        {
            MemberSince = DateTime.UtcNow;
            Orders = new List<Order>();
        }

        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual double AverageRating { get; set; }
        public virtual int Points { get; set; }

        public virtual bool HasGoldStatus { get; set; }
        public virtual DateTime MemberSince { get; set; }
        public virtual CustomerCreditRating CreditRating { get; set; }
        public virtual Location Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual void AddOrder(Order order) 
        { 
            Orders.Add(order); 
            order.Customer = this; 
        }
    }

    public class Location
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string Province { get; set; }
        public virtual string Country { get; set; }
    }

    public enum CustomerCreditRating
    {
        Excellent,
        VeryVeryGood,
        VeryGood,
        Good,
        Neutral,
        Poor,
        Terrible
    }
}
