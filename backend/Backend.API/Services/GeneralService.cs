using System.Data;
using Backend.API.Entities;
using Backend.API.Queries;
using Dapper;

namespace Backend.API.Services
{
    public class GeneralService
	{
		private readonly IDbConnection _dbConnection;

		public GeneralService(
            IDbConnection dbConnection
		)
		{
			_dbConnection = dbConnection;
		}

		public async Task<IEnumerable<Customer>> GetCustomers()
		{
			return await _dbConnection.QueryAsync<Customer>(GeneralQueries.GetCustomers());
		}

        public async Task<IEnumerable<Service>> GetServices(int customerId)
        {
            return await _dbConnection.QueryAsync<Service>(GeneralQueries.GetServices(), new
			{
                customerId
            });
        }

		public async Task<bool> PayInvoice(Invoice invoice)
		{
			var rows = await _dbConnection.ExecuteAsync(GeneralQueries.PayInvoice(), invoice);

			return rows > 0;
		}
    }
}

