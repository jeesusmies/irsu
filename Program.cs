using System;
using System.Reflection;
using System.Threading.Tasks;
using umaru.Library.Attributes.Command;
using umaru.Library;

class TestClass
{
    public static void Main(string[] args)
    {
        // TODO: config
        var client = new Client("jeesusmies", "", "!");
        client.Start();
    }
    
    [Event]
    public static async Task OnReady(Context ctx)
    {
        //await ctx.JoinChannel("#osu");
        Console.WriteLine("Bot is ready!");
    }

    [Event]
    public static async Task OnMessage(Context ctx)
    {
        Console.WriteLine($"<{ctx.Message.Author}> -> " +
                          $"{ctx.Message.Channel}: " +
                          $"{ctx.Message.Contents}");
    }

    [Command]
    public static async Task Ping(Context ctx)
    {
        await ctx.Send("Pong!");
    }
}