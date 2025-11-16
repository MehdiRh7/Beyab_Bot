using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using DataLayer;
using DataLayer.Utilities;
using DataLayer.Handlers;

namespace DataLayer.Services
{
    public class BotRunner : IDisposable
    {
        private readonly string _token;
        private readonly IMessageHandler _messageHandler;
        private readonly ICallbackHandler _callbackHandler;
        private readonly TelegramBotClient _bot;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private int _offset = 0;

        public BotRunner(string token, IMessageHandler messageHandler, ICallbackHandler callbackHandler)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            _messageHandler = messageHandler ?? throw new ArgumentNullException(nameof(messageHandler));
            _callbackHandler = callbackHandler ?? throw new ArgumentNullException(nameof(callbackHandler));
            _bot = new TelegramBotClient(_token);
        }

        public void Start()
        {
            Task.Run(RunLoopAsync);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        private async Task RunLoopAsync()
        {
            try
            {
                while (!_cts.IsCancellationRequested)
                {
                    var updates = await _bot.GetUpdatesAsync(_offset, cancellationToken: _cts.Token).ConfigureAwait(false);
                    foreach (var up in updates)
                    {
                        // keep offset to next update id
                        _offset = up.Id + 1;

                        if (up.CallbackQuery != null)
                        {
                            await _callbackHandler.HandleCallbackAsync(up, _bot).ConfigureAwait(false);
                        }
                        else
                        {
                            await _messageHandler.HandleMessageAsync(up, _bot).ConfigureAwait(false);
                        }
                    }

                    // small delay to avoid tight loop
                    await Task.Delay(300, _cts.Token).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException) { /* stopping */ }
        }

        public void Dispose()
        {
            Stop();
            _cts.Dispose();
        }
    }
}