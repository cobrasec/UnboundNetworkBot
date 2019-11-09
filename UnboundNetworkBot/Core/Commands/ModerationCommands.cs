using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord.Rest;
using UnboundNetworkBot;
namespace UnboundNetworkBot.Core.Commands
{


    public class ModerationCommands : ModuleBase
    {
        //private DiscordSocketClient

        [Command("userinfo"), Summary("Returns the users profile info")]
        [Alias("user", "whois")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task Client_Ready(SocketGuildUser user = null, SocketTextChannel channel = null)
        {

            EmbedBuilder Embed = new EmbedBuilder();

            if (user == null)
            {
                await channel.SendMessageAsync(":x: Whoops, you didn't provide a username or a mention! **Use s! user <@user>**");
                return;
            }

            if (user.IsBot)
            {
                await channel.SendMessageAsync(" :x: Sorry but I cannot provide info for bots as they are not users!");
                return;
            }


            //Embed for userinfo

            var userInfo = user.CreatedAt.LocalDateTime.ToString("dddd, dd MMMM yyyy");

            Embed.AddField("Profile created on.", "\n" + userInfo, true);
            Embed.AddField("User dicriminator.", "\n" + user.Discriminator, true);
            Embed.AddField("Users status.", user.Status, true);
            Embed.WithColor(17, 0, 255);
            Embed.WithFooter("Action was performed by UnboundNetwork bot.");
            Embed.WithTitle("User's info has been provided.");


            Embed.WithFields();
            Embed.WithThumbnailUrl(user.GetAvatarUrl());

            ulong id = 489175046317670412;

            //This is here to commit a this code


            await channel.Guild.GetTextChannel(id).SendMessageAsync($"{ user.Username}'s info has been provided.", false, Embed.Build());
        }


        [Command("say")]
        public async Task SayTest([Remainder] string message)
        {
            DiscordSocketClient client = new DiscordSocketClient();
            ulong channelID = 489175046317670412;

            var channel = client.GetChannel(channelID) as SocketTextChannel;
            await channel.SendMessageAsync(message);

        }




        [Command("Kick"), Summary("Kicks a specified user")]
        [Alias("k", "kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]

        public async Task Kick(IGuildUser user = null, [Remainder] string reason = "Reason not provided.")
        {



            EmbedBuilder Embed = new EmbedBuilder();
            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: You didn't specify a user to kick! Please use s!<@username>");
                return;
            }


            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: If you want to kick a bot do it manually! **This is a security feature so people can't kick bots via command.**");
                return;
            }

            if (reason == "Reason not provided.")
            {
                await Context.Channel.SendMessageAsync(":x: You did not provide a reason to kick the user!");
                return;
            }

            await user.KickAsync();

            //Shows the date the user was kicked on
            var date = Context.Message.CreatedAt.ToString("dddd, dd MMMM yyyy");

            //This is the embed for the kick
            Embed.WithTitle("User kicked information.");
            Embed.AddField("Username:", user.Username, true);
            Embed.AddField("Kicked by:", Context.User.Mention, true);
            Embed.AddField("Kicked on:", date, true);
            Embed.AddField("Reason:", reason, true);
            Embed.WithColor(255, 30, 6);
            Embed.WithThumbnailUrl(user.GetAvatarUrl());

            await Context.Channel.SendMessageAsync($"{user.Username} was kicked! Providing info of the kick...", false, Embed.Build());

        }
        [Command("Ban"), Summary("Bans a specified user")]
        [Alias("b", "ban")]
        [RequireUserPermission(GuildPermission.KickMembers)]

        public async Task Ban(IGuildUser user = null, [Remainder] string reason = "Reason not provided.")
        {



            EmbedBuilder Embed = new EmbedBuilder();
            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: You didn't specify a user to kick! Please use s!<@username>");
                return;
            }


            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(":x: If you want to ban a bot do it manually! **This is a security feature so people can't kick bots via command.**");
                return;
            }

            if (reason == "Reason not provided.")
            {
                await Context.Channel.SendMessageAsync(":x: You did not provide a reason to ban the user!");
                return;
            }

            await user.BanAsync();

                Embed.WithTitle("User ban information.");
                Embed.AddField("Username:", user.Username, true);
                Embed.AddField("banned by:", Context.User.Mention, true);
                Embed.AddField("Reason:", reason, true);
                Embed.WithColor(255, 30, 6);
                Embed.WithThumbnailUrl(user.GetAvatarUrl());

            await Context.Channel.SendMessageAsync($"{user.Username} was banned! Providing info of the ban...", false, Embed.Build());
            



        }


    }

}
