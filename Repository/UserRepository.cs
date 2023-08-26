using Contracts;
using Entities.Models;
using MySimpleDatabaseDriver;

namespace Repository;
public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(MySimpleDatabaseClient dbClient) : base(dbClient)
    {
    }
}
