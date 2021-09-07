using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedBot.Core.Services.DataStorage;
using Discord;
using Discord.WebSocket;

namespace AdvancedBot.Core.Services
{
    public class SuggestionService
    {
        private DiscordSocketClient _client;
        private GuildAccountService _guilds;
        private readonly ulong _suggestionChannelId = 870211340906680340;
        private readonly ulong _implementedChannelId = 0;

        public SuggestionService(DiscordSocketClient client, GuildAccountService guilds)
        {
            _client = client;
            _guilds = guilds;
        }

        public async Task CreateSuggestionAsync(ulong guildId, IUser user, string suggestion)
        {
            var guild = _guilds.GetOrCreateGuildAccount(guildId);
            var channel = _client.GetChannel(_suggestionChannelId) as SocketTextChannel;

            guild.SuggestionNumber++;

            var msg = await channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = $"Suggestion #{guild.SuggestionNumber}",
                Description = suggestion
            }
            .WithColor(Color.LighterGrey)
            .WithFooter($"{user.Username}#{user.DiscriminatorValue} ({user.Id})", user.GetAvatarUrl())
            .WithCurrentTimestamp()
            .Build());

            await msg.AddReactionsAsync(new List<IEmote>() { new Emoji("⬆️"), new Emoji("⬇️") }.ToArray());

            _guilds.SaveGuildAccount(guild);
        }

        public async Task ImplementSuggestionAsync(ulong guildId, uint suggestionId)
        {

        }
    }
}
