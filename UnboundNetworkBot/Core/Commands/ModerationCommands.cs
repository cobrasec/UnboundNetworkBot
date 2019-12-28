using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace UnboundNetworkBot.Core.Commands
{


    public class ModerationCommands : ModuleBase
    {

        [Command("userinfo"), Summary("Returns the users profile info")]
        [Alias("user", "whois")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)] 
        public async Task UserInfo(SocketGuildUser user = null)
        {
            EmbedBuilder Embed = new EmbedBuilder();

            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: Whoops, you didn't provide a username or a mention! **Use s! user <@user>**");
                return;
            }

            if (user.IsBot)
            {
                await Context.Channel.SendMessageAsync(" :x: Sorry but I cannot provide info for bots as they are not users!");
                return;
            }

            //Embed for userinfo

            var userInfo = user.CreatedAt.LocalDateTime.ToString("dddd, dd MMMM yyyy");

            Embed.AddField("Profile created on.", "\n" + userInfo, true);
            Embed.AddField("User dicriminator.", "\n" + user.Discriminator, false);
            Embed.AddField("Users status.", user.Status, true);
            Embed.WithColor(17, 0, 255);
            Embed.WithFooter("Action was performed by UnboundNetwork bot.");
            Embed.WithTitle("User's info has been provided.");


            Embed.WithFields();
            Embed.WithThumbnailUrl(user.GetAvatarUrl());
            
            await Context.Channel.SendMessageAsync($"{ user.Username}'s info has been provided.", false, Embed.Build());
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
        [RequireUserPermission(GuildPermission.BanMembers)]

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


        [Command("Purge"), Remarks("Purge the messages in the channel")]
        [Alias("p")]
        [RequireUserPermission(Discord.GuildPermission.ManageMessages)]

        public async Task Clear(int amountOfMessagesToDelete)
        {
            await (Context.Message.Channel as SocketTextChannel).DeleteMessagesAsync(await Context.Message.Channel.GetMessagesAsync(amountOfMessagesToDelete + 1).FlattenAsync());

            await Context.Channel.SendMessageAsync("Messages have been purged!");

        }

        [Command("UserPurge"), Summary("Purges a specified users messages")]
        [Alias("userp")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task Clear(SocketGuildUser user = null, int amountOfMessagesToDelete = 20)
        {

            if (user == null)
            {
                await Context.Channel.SendMessageAsync(":x: You didn't specify a user to purge! @ them or type their username!");
                return;
            }



            if (user == Context.User)
                amountOfMessagesToDelete++; //Because it will count the purge command as a message

            var messages = await Context.Message.Channel.GetMessagesAsync(amountOfMessagesToDelete).FlattenAsync();

            var result = messages.Where(x => x.Author.Id == user.Id && x.CreatedAt >= DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(14)));

            await (Context.Message.Channel as SocketTextChannel).DeleteMessagesAsync(result);

            await Context.Channel.SendMessageAsync("Users messages have been purged!");


        }



        [Command("help")]
        public async Task Help()
        {

            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithFooter("xXCobraGamingXx made this");
            Embed.WithColor(68, 63, 209);
            Embed.WithTitle("UnboundNetwork bot");
            Embed.WithDescription("Hello and welcome to the UnboundNetwork bot! This bot was made with love. So please, if there is anything wrong please contact the author! \n " +
               "Bot prefix is `s!` \n" +
                "To use commands type `s!` then the command name.  \n " +
                "List of commands \n" +
                "\n **UserInfo** _**(Alias: user)**_ UserInfo will bring up a detailed list of the users account. Creation date, profile picture etc. \n " +
                "**Ban** _**(Alias: b)**_ Ban will ban the user, please provide a reason or staff will not have a detailed description of what the user did.  \n " +
                "**Kick** _**(Alais: k)**_ Kick will kick the user,  please provide a reason or staff will not have a detailed description of what the user did. \n " +
                "**Mute** _**(Alias: m)**_ Mute will mute the user, it gives them a role that allows them to see chats but cannot talk in them. \n " +
                "**Purge** _**(Alias: p)**_ This command will purge a specified ammount of messages **Use this with care** (You need to use this command in the channel where the user is talking.)\n" +
                "**UserPurge** _**(Alias: userp)**_ This command will purge a set amount of user messages. **Use this with care** (You need to use this command in the channel where the user is talking.) \n" +
                "**Use these commands in the log channel provided. As it will log the kicked, muted and banned users keep this in mind**");


            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }



    }

}
