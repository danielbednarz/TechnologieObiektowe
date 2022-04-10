using NHibernate;
using NHibernateProject.Model;

namespace NHibernateProject
{
    public class MainDatabaseContext
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public MainDatabaseContext(ISession session)
        {
            _session = session;
        }

        public IQueryable<Medicament> Medicaments => _session.Query<Medicament>();

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task Save(Medicament entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(Medicament entity)
        {
            await _session.DeleteAsync(entity);
        }
    }

}
