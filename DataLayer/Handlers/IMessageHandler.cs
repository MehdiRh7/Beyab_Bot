using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DataLayer.Handlers
{
    public interface IMessageHandler
    {
        Task HandleMessageAsync(Update update, TelegramBotClient bot);
    }
}