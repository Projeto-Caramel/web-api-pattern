namespace Caramel.Pattern.Services.Domain.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPetRepository Pets { get; }
        IPartnerRepository Partners { get; }
        int Save();
    }
}
