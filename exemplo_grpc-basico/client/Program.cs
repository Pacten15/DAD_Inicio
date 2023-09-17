using Grpc.Core;
using Grpc.Net.Client;
using server;
using System;

namespace GprcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //var input = new HelloRequest { Name = "Tim" };
            var channel = GrpcChannel.ForAddress("https://localhost:7268");
            var customerClient = new Customer.CustomerClient(channel);

            var clientRequest = new CustomerLookupModel { UserId = 2 };
            var customer = await customerClient.GestCustomerInfoAsync(clientRequest);

            Console.WriteLine($"{customer.FirstName}{customer.LastName}");

            Console.WriteLine();
            Console.WriteLine("New Cstomer List");
            Console.WriteLine();

            using (var call = customerClient.GetNewCustomer(new NewCustomerRequest()))
            {
                while(await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName}{currentCustomer.LastName}: {currentCustomer.EmailAddress}");
                }
            }
            Console.ReadLine();
        }
    }
}