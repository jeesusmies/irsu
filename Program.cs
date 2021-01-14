using System;
using System.Reflection;
using System.Threading.Tasks;
using umaru.Library.Attributes.Command;
using umaru.Library;

// TODO: config
var client = new Client();

[Event] 
static async Task OnMessage(Context ctx)
{
    Console.WriteLine(ctx.Message.Contents);
}

[Command]
//[Alias(new [] { "p" })] // csharp lol
static async Task Ping(Context ctx)
{
    
}

client.Start();

/*
 ILLUSTRATION;
 
var bot = new Bot(username, password, ">") // prefix

[command.Event]
public async Task OnUserJoin(ctx) {
  Console.WriteLine(ctx.User.name);
}

[command.Command]
[Alias(["p", "pi"])]
public async Task Ping(ctx) { // also usable by command name
  ctx.Send("Pong!");
}

bot.Start("username", "password", "!");
*/