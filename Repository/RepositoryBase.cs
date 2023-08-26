using Contracts;
using MySimpleDatabaseDriver;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly MySimpleDatabaseClient _dbClient;

        public RepositoryBase(MySimpleDatabaseClient dbClient)
        {
            _dbClient = dbClient;
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            return await _dbClient.IssueSelectCommand<T>();
        }

        public async Task<T> FindByID(object id)
        {
            return await _dbClient.IssueSelectOneCommand<T>(id);
        }

        public async Task Create(T entity)
        {
            await _dbClient.IssueInsertCommand(entity);
        }
    }
}
