using System;
using System.Threading.Tasks;

namespace irsu.Library
{
    public static class CommandHandler
    {
        public static async Task StartHandling(this Client client)
        {
            string data;
            while (client._tcpClient.Connected)
            {
                if ((data = await client._inputStream.ReadLineAsync()) != null)
                {
                    var dataArr = data.Split(' ');
                    
                    if (dataArr[0] == "PING")
                    {
                        await client._outputStream.WriteLineAsync($"PONG {dataArr[1]}");
                        await client._outputStream.FlushAsync();
                        return;
                    }

                    switch (dataArr[1])
                    {
                        case "PRIVMSG":
                            var message = data.Split(':')[2];
                                
                            var context = new Context
                            {
                                Client = client,
                                Message = new Message()
                                {
                                    Author = data.Split('!')[0].Substring(1),
                                    Channel = dataArr[2],
                                    Hostname = data.Split('!')[1],
                                    Contents = message
                                }
                            };

                            Task.Factory.StartNew(async () =>
                            {
                                await client._commandHandlers.InvokeMethod(message.Split(' ')[0], new object[] { context } );
                                await client._eventHandlers.InvokeMethod("OnMessage", new object[] { context } );
                            }).ConfigureAwait(false);
                            
                            break;
                    }
                }
            }
        }
    }
}
