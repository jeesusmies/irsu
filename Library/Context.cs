namespace umaru.Library
{
    public class Context
    {
        private Client Client { get; init; }

        public Message Message { get; init; }
        public string Opcode { get; init; }

        public void Send(string message)
        {
            Client._outputStream.WriteLine($"PRIVMSG {Message.Name} :{message}");
        }
    }
}