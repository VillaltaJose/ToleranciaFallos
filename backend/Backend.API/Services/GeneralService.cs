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

        private static async Task<T> ExecuteWithRetry<T>(Func<Task<T>> operation)
        {
            int retries = 3;
            int delay = 1000;

            for (int i = 0; i < retries; i++)
            {
                try
                {
                    return await operation();
                }
                catch (Exception)
                {
                    if (i == retries - 1)
                    {
                        throw;
                    }

                    await Task.Delay(delay);
                    delay += 1000;
                }
            }

            throw new InvalidOperationException("Exceeded number of retries");
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await ExecuteWithRetry(() => _dbConnection.QueryAsync<Customer>(GeneralQueries.GetCustomers()));
        }

        public async Task<IEnumerable<Service>> GetServices(int customerId)
        {
            return await ExecuteWithRetry(() => _dbConnection.QueryAsync<Service>(GeneralQueries.GetServices(), new
            {
                customerId
            }));
        }

        public async Task<bool> PayInvoice(Invoice invoice)
        {
            var invoiceId = await ExecuteWithRetry(() => _dbConnection.ExecuteScalarAsync<int>(GeneralQueries.PayInvoice(), invoice));

            if (invoiceId > 0)
            {
                var customer = await ExecuteWithRetry(() => _dbConnection.QueryFirstAsync<Customer>(GeneralQueries.GetCustomer(), new
                {
                    id = invoice.CustomerId
                }));

                var service = await ExecuteWithRetry(() => _dbConnection.QueryFirstAsync<Service>(GeneralQueries.GetService(), new
                {
                    id = invoice.ServiceId
                }));

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