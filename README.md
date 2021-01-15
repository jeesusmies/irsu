osu! bots made easy!

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
}

[Command]
public static async Task Ping(Context ctx) 
{
    await ctx.Send("Pong!");
}
```
