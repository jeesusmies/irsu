using System;

/*
 ILLUSTRATION;
 
var bot = new Bot(">") // prefix

[bot.Event]
public async Task OnUserJoin(ctx) {
  Console.WriteLine(ctx.User.name);
}

[bot.Command]
[Alias(["p", "pi"])]
public async Task Ping(ctx) { // also usable by command name
  ctx.Send("Pong!");
}
*/