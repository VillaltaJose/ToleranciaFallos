namespace Backend.API.Queries
{
    public static class GeneralQueries
	{
		public static string GetCustomers()
		{
			return @"select id, name, email from customers";
		}

        public static string GetCustomer()
        {
            return @"select id, name, email from customers
                    where id = :id";
        }

        public static string GetServices()
        {
            return @"SELECT s.id, s.name, s.price
                    FROM services s
                    LEFT JOIN invoices i on
                    s.id = i.service_id AND i.customer_id = :customerId
                    WHERE i.id IS NULL;";
        }

        public static string GetService()
        {
            return @"SELECT s.id, s.name, s.price
                    FROM services s
                    where s.id = :id";
        }

        public static string PayInvoice()
        {
            return @"insert into invoices 
                    (
                    customer_id,
                    service_id,
                    amount,
                    created_at
                    )
                    values 
                    (
                    :CustomerId,
                    :ServiceId,
                    :Amount,
                    now()
                    ) returning id";
        }
    }
}

