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
        public IQueryable<Recipe> Recipes => _session.Query<Recipe>();
        public IQueryable<RecipeMedicament> RecipeMedicaments => _session.Query<RecipeMedicament>();
        public IQueryable<Department> Departments => _session.Query<Department>();
        public IQueryable<Employee> Employees => _session.Query<Employee>();
        public IQueryable<Visit> Visits => _session.Query<Visit>();
        public IQueryable<Doctor> Doctors => _session.Query<Doctor>();
        public IQueryable<Nurse> Nurses => _session.Query<Nurse>();
        public IQueryable<TechnicalWorker> TechnicalWorkers => _session.Query<TechnicalWorker>();
        public IQueryable<Patient> Patients => _session.Query<Patient>();

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task AddAsync<T>(T entity)
        {
            await _session.SaveAsync(entity);
        }

        public void Add<T>(T entity)
        {
            _session.Save(entity);
        }

        public async Task AddRangeAsync<T>(List<T> entity)
        {
            foreach (var item in entity)
            {
                await _session.SaveOrUpdateAsync(item);
            }
        }

        public void AddRange<T>(List<T> entity)
        {
            foreach (var item in entity)
            {
                _session.Save(item);
            }
        }

        public void SaveOrUpdateRange<T>(List<T> entity)
        {
            foreach (var item in entity)
            {
                _session.SaveOrUpdate(item);
            }
        }

        public async Task UpdateRangeAsync<T>(List<T> entity)
        {
            foreach (var item in entity)
            {
                await _session.UpdateAsync(item);
            }
        }

        public void UpdateRange<T>(List<T> entity)
        {
            foreach (var item in entity)
            {
                _session.Update(item);
            }
        }

        public async Task AddMedicamentToRecipeAsync(Recipe recipe)
        {
            await _session.SaveOrUpdateAsync(recipe);
        }

        public async Task SaveAsync(Medicament entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task DeleteAsync(Medicament entity)
        {
            await _session.DeleteAsync(entity);
        }
    }

}
