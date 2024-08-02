namespace HomeyBackend.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}