using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using irsu.Library.Attributes.Command;

namespace irsu.Library
{
    public class Client
    {
        internal TcpClient _tcpClient;

        internal StreamReader _inputStream;
        internal StreamWriter _outputStream;

        internal Dictionary<string, MethodInfo> _commandHandlers = new();
        internal Dictionary<string, MethodInfo> _eventHandlers = new();

        public string Username;
        private string _password;
        private string _prefix;

        public Client(string username, string password, string prefix)
        {
            Username = username;
            _password = password;
            _prefix = prefix;

            RegisterMethods();

            // irc server for osu
            // i think u can literally use this for any server
            _tcpClient = new TcpClient("irc.ppy.sh", 6667);
        }

        public void Start()
        {
            RegisterMethods();
            
            _inputStream = new StreamReader(_tcpClient.GetStream());
            _outputStream = new StreamWriter(_tcpClient.GetStream()) {NewLine = "\r\n"};

            // not sure if i should do 3 writelines or 1 writeline?
            _outputStream.WriteLine($"PASS {_password}\r\nUSER {Username} 0 * :{Username}\r\nNICK {Username}");
            _outputStream.Flush();

            string data;
            while (_tcpClient.Connected)
            {
                if ((data = _inputStream.ReadLine()) != null)
                {
                    Console.WriteLine(data);
                    
                    var dataArr = data.Split(' ');

                    if (dataArr[1] == "001")
                        break;
                }
            }

            _eventHandlers.InvokeMethod("OnReady", new object[] { new Context() { Client = this } } ).ConfigureAwait(true);
            this.StartHandling().Wait();
        }

        private void RegisterMethods()
        {
            /* REFLECTION TO GET ALL EVENTS AND COMMANDS */
            var info = Assembly.GetExecutingAssembly();

            object att;
            foreach(var type in info.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                                       BindingFlags.Static | BindingFlags.Instance))
                {
                    if (method.GetCustomAttributes(typeof(Command), false).Length > 0)
                    {
                        var classInstance = Activator.CreateInstance(type, null);
                        Alias aliasAtt;
                        if ((aliasAtt = (Alias) method.GetCustomAttribute(typeof(Alias), false)) != null)
                        {
                            foreach (var alias in aliasAtt.Aliases)
                            {
                                try
                                {
                                    _commandHandlers.Add(alias, method);
                                }
                                catch
                                {
                                }
                            }
                        }
                        
                        try
                        {
                            _commandHandlers.Add(method.Name, method);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            
            foreach(var type in info.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                                       BindingFlags.Static | BindingFlags.Instance))
                {
                    if (method.GetCustomAttributes(typeof(Event), false).Length > 0)
                    {
                        try
                        {
                            _eventHandlers.Add(method.GetBaseDefinition().Name, method);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
    }
}
