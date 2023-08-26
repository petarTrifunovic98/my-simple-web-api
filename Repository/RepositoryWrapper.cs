using Contracts;
using MySimpleDatabaseDriver;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly MySimpleDatabaseClient _dbClient;

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbClient);
                return _userRepository;
            }
        }

        public RepositoryWrapper(MySimpleDatabaseClient dbClient)
        {
            _dbClient = dbClient;
        }
    }
}