using System.Net.Sockets;
using System.Text;

namespace umaru.Library
{
    public class Client
    {
        private TcpClient _tcpClient;
        
        public Client()
        {
            // irc server for osu
            _tcpClient = new TcpClient("irc.ppy.sh", 6667);
        }
    }
}