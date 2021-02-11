using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_hibernate.data
{
    public class DataBaseProvider
    {
        public static ISessionFactory SessionFactory { get; private set; }

        public static void SetSessionFactory(ISessionFactory sessionFactory)
        {
            if(SessionFactory == null)
            {
                SessionFactory = sessionFactory;
            }
        }
    }
}
