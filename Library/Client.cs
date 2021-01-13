using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace umaru.Library
{
    public class Client
    {
        private TcpClient _tcpClient;

        private StreamReader inputStream;
        private StreamWriter outputStream;
        
        public Client()
        {
            // irc server for osu
            // i assume u can literally use this for any server
            _tcpClient = new TcpClient("irc.ppy.sh", 6667);
        }

        public void Start(string username, string password, string prefix)
        {
            inputStream = new StreamReader(_tcpClient.GetStream());
            outputStream = new StreamWriter(_tcpClient.GetStream()) { NewLine = "\r\n" };;
            
            outputStream.WriteLine($"PASS {password}\r\nUSER {username} 0 * :{username}\r\nNICK {username}");
            outputStream.Flush();
        }
    }
}