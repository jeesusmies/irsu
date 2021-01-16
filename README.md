osu! bots made easy! (or any irc bots)  
structured like discord.py  
wip and prefix doesnt yet work but events and commands do work

# Example
```cs
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
```
