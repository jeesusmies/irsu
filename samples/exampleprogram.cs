using System;
using System.Threading.Tasks;
using irsu.Library.Attributes.Command;
using irsu.Library;

class TestClass
{
    public static void Main(string[] args)
    {
        var client = new Client("jeesusmies", "password", "");
        client.Start();
    }

    [Event]
    public static async Task OnReady(Context ctx)
    {
        Console.WriteLine("Bot is ready!");
    }

    [Event]
    public static async Task OnMessage(Context ctx)
    {
        Console.WriteLine($"<{ctx.Message.Author}> -> " +
                          $"{ctx.Message.Channel}: " +
                          $"{ctx.Message.Contents}");
                          // output example; <jeesusmies> -> #osu: fuck pp!
    }

    [Command]
    [Alias(new [] { "ping" })] // command can be called using Ping or ping
    public static async Task Ping(Context ctx) 
    {
        await ctx.Send("Pong!");
    }
}
