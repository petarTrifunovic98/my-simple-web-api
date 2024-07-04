namespace Contracts
{
    public interface IAdminWrapper
    {
        IUserRepository UserRepository { get; }
    }
}