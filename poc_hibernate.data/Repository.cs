using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_hibernate.data
{
    public class Repository
    {

        public Task<Student> GetById(int id)
        {
            using(var session = DataBaseProvider.SessionFactory.OpenSession())
            {
                return session.GetAsync<Student>(id);
            }
        }


        public async Task<int> Save(Student student)
        {
            using(var session = DataBaseProvider.SessionFactory.OpenSession())
            using(var transaction = session.BeginTransaction())
            {
                int id = default;

                try
                {
                    id = (int) await session.SaveAsync(student);
                    await transaction.CommitAsync();

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }

                return id;
            }
        }

        public async Task<IList<Student>> GetAll()
        {
            using (var session = DataBaseProvider.SessionFactory.OpenSession())
            {
                return await session
                    .CreateCriteria<Student>()
                    .ListAsync<Student>();
            }
        }

        public async Task Update(Student student)
        {
            using (var session = DataBaseProvider.SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    await session.UpdateAsync(student);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
