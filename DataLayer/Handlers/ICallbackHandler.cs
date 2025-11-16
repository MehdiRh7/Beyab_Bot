using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DataLayer.Handlers
{
    public interface ICallbackHandler
    {
        Task HandleCallbackAsync(Update update, TelegramBotClient bot);
    }
}