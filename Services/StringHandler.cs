using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Unit11HW.Configuration;
using Unit11HW.Extentions;
using Telegram.Bot;


namespace Unit11HW.Services
{
    public class StringHandler:IStringHandler
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramBotClient;

        public StringHandler(ITelegramBotClient telegramBotClient, AppSettings appSettings){
            _appSettings = appSettings;
            _telegramBotClient = telegramBotClient;
        }


        public string Process (string inputData, string action){
            
            switch (action)
            {
                case "Count":
                return $"Количество символов в строке {inputData.Length}";
                case "Sum":

                return $"Сумма всех цифр в строке: {StringExtentions.GetSum(inputData)}";
                default:
                return "Произошла ошибка";
            }
        }
    }
}