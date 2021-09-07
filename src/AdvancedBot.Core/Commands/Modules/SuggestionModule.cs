using System.Threading.Tasks;
using AdvancedBot.Core.Services;
using Discord;
using Discord.Commands;

namespace AdvancedBot.Core.Commands.Modules
{
    public class SuggestionModule : TopModule
    {
        private SuggestionService _suggestions;
        private ulong _botChannelId = 857741268720549898;

        public SuggestionModule(SuggestionService suggestions)
        {
            _suggestions = suggestions;
        }

        [Command("suggest")]
        public async Task SuggestCommandAsync([Remainder]string feature)
        {
            if (Context.Channel.Id != _botChannelId)
            {
                await Task.Delay(150).ContinueWith(async t => await Context.Message.DeleteAsync());
            }
            else await Context.Message.AddReactionAsync(new Emoji("✅"));

            await _suggestions.CreateSuggestionAsync(Context.Guild.Id, Context.User, feature);
        }
    }
}
