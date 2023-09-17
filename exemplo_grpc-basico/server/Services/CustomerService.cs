using Grpc.Core;
using server;


namespace server.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        public readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GestCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if(request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else
            {
                output.FirstName = "Greg";
                output.LastName = "Thomas";
            }


            return Task.FromResult(output);
        }

        public override async Task GetNewCustomer(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customer = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Tim",
                    LastName = "Corey",
                    EmailAddress = "tim@iamtimcorey.com",
                    Age = 41,
                    IsAlive = true,

                },
                new CustomerModel
                {
                    FirstName = "Sue",
                    LastName = "Storm",
                    EmailAddress = "sue@stormy.net",
                    Age = 28,
                    IsAlive = false,

                },
                new CustomerModel
                {
                    FirstName = "Bilbo",
                    LastName = "Baggins",
                    EmailAddress = "Bilbo@middleearth.net",
                    Age = 117,
                    IsAlive = false,

                }
            };
            
            foreach (var cust in customer)
            {
                await Task.Delay(3);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
