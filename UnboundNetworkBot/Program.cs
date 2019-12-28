using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace UnboundNetworkBot
{
    class Program
    {
        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;

        public CommandService Commands { get => _commands; set => _commands = value; }

        static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();
        public async Task Start()

        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug
            }); 

            var token = File.ReadAllText("Token.txt");
            

            _services = new ServiceCollection()
                .BuildServiceProvider();

            await InstallCommandsAsync();
            await _client.SetGameAsync("Hi! I am bot nice to meet you!");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            _client.Log += LogAsync;
            await Task.Delay(-1);

            

        }


      


        public async Task InstallCommandsAsync()
        {
            // Hook the messageRecieved event into the command handler
            _client.MessageReceived += HandleCommandAsync;
            // Decovering all the commands in this assembly and loading them
            await Commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                         services: null);
        }

        public async Task HandleCommandAsync(SocketMessage messsageParam)
        {
            //Not going to run the command if it is a system message
            var message = messsageParam as SocketUserMessage;
            if (message == null) return;

            //This is where it reads where the prefix ends and the command starts
            int argPos = 0;

            //Seeing if the message is a command or not
            if (!(message.HasStringPrefix("s!" + " ", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot) return;

            //Command Context
            var context = new CommandContext(_client, message);

            //Command execution
            var result = await Commands.ExecuteAsync(context: context, argPos: argPos, services: null);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);

            //Userjoin event and leave event
            _client.UserJoined += AnnounceJoinedUser;
            //  _client.UserLeft += AnnounceUserLeft;
        }

        

        public async Task AnnounceJoinedUser(SocketGuildUser user) //Welcomes the new user
        {
            var channel = _client.GetChannel(489175046317670412) as SocketTextChannel; 
            await channel.SendMessageAsync($"Welcome {user.Mention} to {channel.Guild.Name}"); //Welcomes the new user
            return;
        }


       // public async Task AnnounceUserLeft(SocketGuildUser user)
        //{
            //will add this later on.
        //}


        private Task LogAsync(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.Message);
            return Task.CompletedTask;
        }

      


    }
}
