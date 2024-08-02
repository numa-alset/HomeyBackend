using HomeyBackend.Core;

namespace HomeyBackend.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HomeyBackendDbContext context;

        public UnitOfWork(HomeyBackendDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
