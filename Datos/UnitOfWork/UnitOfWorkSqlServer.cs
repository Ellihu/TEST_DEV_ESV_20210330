using Datos.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Datos.UnitOfWork
{
    public class UnitOfWorkForSqlServer : IUnitOfWork
    {
        private SqlConnection _context { get; set; }
        private SqlTransaction _transaction { get; set; }
        public IUnitOfWorkRepository Repositories { get; set; }

        public UnitOfWorkForSqlServer(IConfiguration configuration)
        {
            _context = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _context.Open();
            _transaction = _context.BeginTransaction();
            Repositories = new UnitOfWorkSqlServerRepository(_context, _transaction);
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            if (_context != null)
            {
                _context.Close();
                _context.Dispose();
            }

            Repositories = null;
        }

        public void SaveChanges()
        {
            _transaction.Commit();
        }
    }
}
