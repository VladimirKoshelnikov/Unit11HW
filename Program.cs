using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using Unit11HW.Configuration;
using Unit11HW.Controllers;
using Unit11HW.Services;

namespace Unit11HW{
    public class Program{
        
        public static AppSettings BuildAppSettings(){
            using (StreamReader sr = new StreamReader("BotToken.txt")){
                return new AppSettings(){
                    BotToken = sr.ReadLine()
                };
            }
        }    

        public static async Task Main(){
            Console.OutputEncoding = Encoding.Unicode;
            var host = new HostBuilder()
                .ConfigureServices((hostcontent, services)=>ConfigureServices(services))
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

                // Запускаем сервис
                // Для подключения к боту используйте ссылку 
                // t.me/Unit11KVSHomeWorkBot

            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");

        }

        static void ConfigureServices(IServiceCollection services){
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());
            // Подключаем контроллеры
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<IStringHandler, StringHandler>();
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
            
        } 
    }
}