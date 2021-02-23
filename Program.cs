using System;

namespace ScoreModelImplementation
{
    class Program
    {
        static void Main(string[] args)
        {

            string data =
                @"Возраст!1,5:До 25 лет_-20!25-50_+40!За 50_-10
Семейное положение!0,3:Не в браке_0!В браке_+40
Дети!0,8:Нет_-10!Есть_+40
Есть ли в данный момент кредит!0,5:Нет_+20!Есть_-10
Кредитная история!0,5:Отрицательная_-30!Положительная_+20
Трудоустройство!1:Нет/неофициальное_-40!Есть/официальное_+50
Трудовой стаж!0,4:Нет_-40!Менее 2 лет_0!Более 2 лет и менее 5 лет_+20!Более 5 лет_+40
Ур. з/п!1,2:Ниже 2xМРОТ(24к)_-20!2-4 МРОТ (24-48к)_+20!Выше 4xМРОТ(48к)_+40
Паспортные данные!0,3:Не совпадает_-20!Совпадает/верифицировано_+20
Количество аварий за последние 5 лет!1,5:Нет_+30!1-2_0!3-4_-20!Более 4_-40
Водительский стаж!1,2:До года_0!1-2 года_+20!3-5 лет_+30!Более 5_+40
Наличие собственной машины!0,7:Нет_0!Есть_+30";

            Console.WriteLine(data + "\n");
            ScoreModel testModel = new ScoreModel(data);

            int[] simpleCheck = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            Console.WriteLine(testModel.ReturnScore(simpleCheck)); //Ожидаем 234
            Console.WriteLine(testModel.ReturnChoices(simpleCheck)); //Везде второй вариант


            int[] simpleCheckZero = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Console.WriteLine(testModel.ReturnScore(simpleCheckZero)); 
            Console.WriteLine(testModel.ReturnChoices(simpleCheckZero)); //Везде первый вариант 
            //Везде второй вариант
        }
    }
}
