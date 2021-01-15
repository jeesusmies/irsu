using System;
using System.Threading.Tasks;

namespace umaru.Library
{
    public class Context
    {
        internal Client Client { get; init; }

        public Message Message { get; init; }

        public async Task Send(string message)
        {
            await Client._outputStream.WriteLineAsync($"PRIVMSG {Message.Author} :{message}");
            await Client._outputStream.FlushAsync();
        }

        public async Task JoinChannel(string channel)
        {
            await Client._outputStream.WriteLineAsync($"JOIN {channel}");
            await Client._outputStream.FlushAsync();
        }
    }
}