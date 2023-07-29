using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

using Unit11HW.Models;
using Unit11HW.Services;

namespace Unit11HW.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private readonly IStringHandler _stringHandler;

        public TextMessageController(ITelegramBotClient telegramClient, IStorage memoryStorage, IStringHandler stringHandler){
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
            _stringHandler = stringHandler;
        }
        
public async Task Handle(Message message, CancellationToken ct)
        {
            switch(message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]{
                        InlineKeyboardButton.WithCallbackData($"Подсчет символов в строке", $"Count"),
                        InlineKeyboardButton.WithCallbackData($"Сумма чисел", $"Sum")
                        });
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Наш бот подсчитывает количество символов в строке, а также находит сумму чисел, записанных через пробел.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}Числа необходимо записывать через пробел.{Environment.NewLine}", cancellationToken: ct, parseMode:ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    Session session = _memoryStorage.GetSession(message.From.Id);

                    string answerMessage = _stringHandler.Process(message.Text, session.Action);
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, text: answerMessage, cancellationToken: ct);

                    break;
            }
        }
    }
}