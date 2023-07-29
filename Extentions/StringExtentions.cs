using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unit11HW.Extentions
{
    public static class StringExtentions
    {
        static public int GetSum(string str)
        {

            List<int> numbers = GetNumbersFromString(str);

            int sum = 0;
            if (numbers.Count == 0){
                return sum;
            }

            foreach(int number in numbers){
                sum += number;
            }
            return sum;
        }

        public static List<int> GetNumbersFromString(string str){
            List<int> numbers = new List<int>();
            try{
                while (str.IndexOf(' ') != -1){
                    string number = str.Substring(0, str.IndexOf(' '));
                    numbers.Add(int.Parse(number));
                    str = str.Substring(str.IndexOf(' ')+1);
                }
                numbers.Add(int.Parse(str));
            }
            catch{
                Console.WriteLine("Произошла ошибка ввода данных");
                return new List<int>();
            }
                    
            return numbers;
        }
    }
}