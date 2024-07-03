using System.Data;
using Backend.API.Entities;
using Backend.API.Queries;
using Dapper;

namespace Backend.API.Services
{
    public class GeneralService
	{
		private readonly IDbConnection _dbConnection;
		private readonly RabbitMQService _queueService;

		public GeneralService(
            IDbConnection dbConnection,
            RabbitMQService queueService
        )
		{
			_dbConnection = dbConnection;
			_queueService = queueService;
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
			var invoiceId = await _dbConnection.ExecuteScalarAsync<int>(GeneralQueries.PayInvoice(), invoice);

			if (invoiceId > 0)
			{
				var customer = await _dbConnection.QueryFirstAsync<Customer>(GeneralQueries.GetCustomer(), new
				{
					id = invoice.CustomerId
				});

				var service = await _dbConnection.QueryFirstAsync<Service>(GeneralQueries.GetService(), new
				{
                    id = invoice.ServiceId
                });

				var notification = new Notification
				{
					To = customer.Email,
					Subject = $"Invoice #{invoiceId}",
					Body = @$"Hello {customer.Name},
							Please find below the details of your payment:
							Service: {service.Name}
							Total: ${invoice.Amount}"
				};

				_queueService.SendMessage(notification, "invoices");
			}

			return invoiceId > 0;
		}
    }
}

