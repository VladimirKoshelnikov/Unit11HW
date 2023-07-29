using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using Unit11HW.Services;

namespace Unit11HW.Controllers
{

    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        
        public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage){
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }
        
          public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct){
             if (callbackQuery?.Data == null)
                return;
            _memoryStorage.GetSession(callbackQuery.From.Id).Action = callbackQuery.Data;

            string action = callbackQuery.Data switch
            {
                "Count" => "Подсчет количеста символов в строке",
                "Sum" => "Сумма всех чисел в строке",
                _ => String.Empty
            };
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"<b>Выбрана функция - {action}.</b> {Environment.NewLine}" + 
            $"{Environment.NewLine} Можно поменять в главном меню", cancellationToken: ct, parseMode: ParseMode.Html);
        
            
            
            Console.WriteLine($"Контроллер {GetType().Name} обнаружил нажатие на кнопку {callbackQuery.Data}" );
        }

    }
}