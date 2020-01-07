using Discord.WebSocket;
using System;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;
using System.Linq;

namespace UnboundNetworkBot.Core.Commands
{

    //Public commands everyone can use. They are non destructive and give the community something to do, feel free to suggest a command.
    public class PublicUserCommands : ModuleBase
    {
        [Command("Argument"), Summary("Sends a new ice breaker for everyone to yell at each other")]
        public async Task Argument()
        {
            Random rand;
            rand = new Random();
            string[] Argument;
            Argument = new string[]
                {
                    "iPhone or Android?",
                    "Game of Thrones or Lord of the Rings?",
                    "Windows or Mac?",
                    "Discord or Skype?",
                    "Pokemon or Digimon?",
                    "Laptop or Desktop?",
                    "Will add more please bear with me."
                };
            int randomArgument = rand.Next(Argument.Length);
            string randomArgumentToPost = Argument[randomArgument];
            await Context.Channel.SendMessageAsync(randomArgumentToPost);

        }

        [Command("8Ball"), Summary("Ask the 8ball how your day will be or maybe.... your life!")]
        [Alias("8ball", "ball", "8ball,", "8Ball,", "magic")]
        public async Task MagicBall([Remainder]string args = null)
        {
            Random rand;
            rand = new Random();
            string[] magicBall;
            magicBall = new string[]
                {
                    ":8ball: It is certain.",
                    ":8ball: It is decidedly so.",
                    ":8ball: Without a doubt.",
                    ":8ball: Yes, definitely.",
                    ":8ball: You may reply on it.",
                    ":8ball: As I see it, yes.",
                    ":8ball: Most likely.",
                    ":8ball: Outlook good.",
                    ":8ball: Yes.",
                    ":8ball: Signs point to yes.",
                    ":8ball: Reply hazy, try again.",
                    ":8ball: Ask again later",
                    ":8ball: Better not tell you now.",
                    ":8ball: Cannot predict now.",
                    ":8ball: Concentrate and ask again.",
                    ":8ball: Don't count on it.",
                    ":8ball: My reply is no.",
                    ":8ball: My Sources say no.",
                    ":8ball: Outlook not so good.",
                    ":8ball: Very doubtful."
                };
            int randomBall = rand.Next(magicBall.Length);
            string randomBallToPost = magicBall[randomBall];
            await Context.Channel.SendMessageAsync(randomBallToPost);
        }


        [Command("WYR"), Summary("Bot will ask Would you rather questions to users.")]
        [Alias("Would you rather", "wyr")]
        public async Task WYR([Remainder] string args = null)
        {
            Random rand;
            rand = new Random();
            string[] wouldRather;
            wouldRather = new string[]
                {
                    "Would you rather the aliens that make first contact be robotic or orangic?",
                    "Would you rather lose the ability to read or the ability to speak?",
                    "Would you rather have a golden voice or silver tongue?",
                    "Would you rather always be 10 minutes late or 20 minutes early?",
                    "Would you rather know the history or every object you touched or be able to talk to animals?",
                    "Would you rather have all traffic lights turn green or never have to stand in line again?",
                    "Would you rather be the first to explore a planet or be the invetor of a drug that cures a deadly disease?",

                };
            int wyRather = rand.Next(wouldRather.Length);
            string wouldYouRatherToPost = wouldRather[wyRather];
            await Context.Channel.SendMessageAsync(wouldYouRatherToPost);

        }

    }
}
