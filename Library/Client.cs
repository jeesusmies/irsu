using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using umaru.Library.Attributes.Command;

namespace umaru.Library
{
    public class Client
    {
        private TcpClient _tcpClient;

        internal StreamReader _inputStream;
        internal StreamWriter _outputStream;

        private Dictionary<string, object> _commandHandlers = new();
        private Dictionary<string, object> _eventHandlers = new();
        
        public Client()
        {
            RegisterMethods();
            
            // irc server for osu
            // i think u can literally use this for any server
            _tcpClient = new TcpClient("irc.ppy.sh", 6667);
        }

        public void Start(string username, string password, string prefix)
        {
            _inputStream = new StreamReader(_tcpClient.GetStream());
            _outputStream = new StreamWriter(_tcpClient.GetStream()) { NewLine = "\r\n" };;
            
            // not sure if i should do 3 writelines or 1 writeline?
            _outputStream.WriteLine($"PASS {password}\r\nUSER {username} 0 * :{username}\r\nNICK {username}");
            _outputStream.Flush();
        }

        private void RegisterMethods()
        {
            /* REFLECTION TO GET ALL EVENTS AND COMMANDS */
            Assembly info = Assembly.GetExecutingAssembly();

            object att;
            foreach(var type in info.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    if (method.GetCustomAttributes(typeof(Command), false).Length > 0)
                    {
                        var classInstance = Activator.CreateInstance(type, null);
                        Alias aliasAtt;
                        if ((aliasAtt = (Alias) method.GetCustomAttribute(typeof(Alias), false)) != null)
                        {
                            foreach (var alias in aliasAtt.Aliases)
                            {
                                _commandHandlers.Add(alias, method);
                            }
                        }
                        _commandHandlers.Add(method.Name, method);
                    }
                }
            }
            
            foreach(var type in info.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    if (method.GetCustomAttributes(typeof(Event), false).Length > 0)
                    {
                        _eventHandlers.Add(method.Name, method);
                    }
                }
            }
        }
    }
}