using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace chatServer
{
    // ChatServerService is the namespace defined in the protobuf
    // ChatServerServiceBase is the generated base implementation of the service
    public class ServerService : ChatServerService.ChatServerServiceBase
    {
        private GrpcChannel channel;
        private Dictionary<string, ChatClientService.ChatClientServiceClient> clientMap =new Dictionary<string, ChatClientService.ChatClientServiceClient>();

        public ServerService()
        {
        }

        public override Task<ChatClientRegisterReply> Register(
            ChatClientRegisterRequest request, ServerCallContext context)
        {
            Console.WriteLine("Deadline: " + context.Deadline);
            Console.WriteLine("Host: " + context.Host);
            Console.WriteLine("Method: " + context.Method);
            Console.WriteLine("Peer: " + context.Peer);
            return Task.FromResult(Reg(request));
        }

        public override Task<BcastMsgReply> BcastMsg(BcastMsgRequest request, ServerCallContext context)
        {
            return Task.FromResult(Bcast(request));
        }


        public BcastMsgReply Bcast(BcastMsgRequest request)
        {
            foreach(var users in  clientMap) {
                if(users.Key != request.Nick)
                {
                    var message_send = new RecvMsgRequest {Nick = request.Nick, Msg = request.Msg };
                    users.Value.RecvMsgAsync(message_send);
                }
            }
            return new BcastMsgReply();
        }

        public ChatClientRegisterReply Reg(ChatClientRegisterRequest request)
        {
            //Thread.Sleep(5001);
            channel = GrpcChannel.ForAddress(request.Url);
            ChatClientService.ChatClientServiceClient client = new ChatClientService.ChatClientServiceClient(channel);
            lock (this)
            {
                clientMap.Add(request.Nick, client);
            }
            Console.WriteLine($"Registered client {request.Nick} with URL {request.Url}");
            ChatClientRegisterReply reply = new ChatClientRegisterReply();

            return reply;
        }
    }
    class Program
    {

        public static void Main(string[] args)
        {
            const int port = 5001;
            const string hostname = "localhost";
            string startupMessage;
            ServerPort serverPort;

            serverPort = new ServerPort(hostname, port, ServerCredentials.Insecure);
            startupMessage = "Insecure ChatServer server listening on port " + port;

            Server server = new Server
            {
                Services = { ChatServerService.BindService(new ServerService()) },
                Ports = { serverPort }
            };

            server.Start();

            Console.WriteLine(startupMessage);
            //Configuring HTTP for client connections in Register method
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            while (true) ;
        }
    }
}


