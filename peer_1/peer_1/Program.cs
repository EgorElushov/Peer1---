using System;

namespace peer_1
{
    internal class Program
    {
        /// <summary>
        ///     Вывод приветственного сообщения.
        /// </summary>
        public static void GreetingsMessage()
        {
            Console.WriteLine("Привет! Это игра \"Быки и Коровы\"");
            Console.WriteLine("Правила очень просты");
            Console.WriteLine("Компьютер загадывает число (цифры в нем не повторяются)");
            Console.WriteLine("Твоя задача угадать его, предлагая варианты");
            Console.WriteLine("Если угадаешь цифру, но не угадаешь ее позицию - это корова");
            Console.WriteLine("Угадаешь и число и позицию - бык");
            Console.WriteLine("Удачи!\n");
        }

        /// <summary>
        ///     Вывод сообщения о победе.
        /// </summary>
        /// <param name="number">Загаданное компьютером число</param>
        public static void FinishMessage(string number)
        {
            Console.WriteLine("Молодец! Ты угадал число: " + number);
            Console.WriteLine("Для выхода из игры напиши: Exit");
            Console.WriteLine("Или напиши что угодно и сыграем еще раз!\n");
        }

        /// <summary>
        ///     Генерация числа компьютером.
        /// </summary>
        /// <param name="numberSize">Размер загадываемого числа</param>
        /// <returns>Строковое представление загаданного числа</returns>
        public static string GenValue(int numberSize)
        {
            Random rand = new Random();
            string getNumber = "0123456789";
            int randomIndex = rand.Next(1, 10);
            string s = getNumber[randomIndex].ToString();
            getNumber = getNumber.Remove(randomIndex, 1);

            for (var i = 0; i < numberSize - 1; i++)
            {
                randomIndex = rand.Next(0, getNumber.Length);
                // Выбор случайной цифры из строки со всеми возможными цифрами.
                s += getNumber[randomIndex].ToString();
                // Добавление выбранного цифры к итоговой 
                getNumber = getNumber.Remove(randomIndex, 1);
                // Удаление использованного числа из возможных для подстановки.
            }

            return s;
        }

        /// <summary>
        ///     Метод подсчета числа коров.
        /// </summary>
        /// <param name="hiddenNumber">Загаданное число</param>
        /// <param name="playersNumber">Число пользователя</param>
        /// <returns>Количество коров в числе пользователя</returns>
        public static int CowsCount(string hiddenNumber, string playersNumber)
        {
            int cowsCount = 0;

            for (int i = 0; i < hiddenNumber.Length; i++)
            {
                var charIndex = playersNumber.IndexOf(hiddenNumber[i]);
                if (charIndex != -1 && charIndex != i)
                    cowsCount++;
            }

            return cowsCount;
        }

        /// <summary>
        ///     Метод подсчета количества быков.
        /// </summary>
        /// <param name="hiddenNumber">Загаданное число</param>
        /// <param name="playersNumber">Число пользователя</param>
        /// <returns>Количество быков в числе пользователя</returns>
        public static int BullsCount(string hiddenNumber, string playersNumber)
        {
            int bullsCount = 0;
            for (int i = 0; i < hiddenNumber.Length; i++)
                if (hiddenNumber[i] == playersNumber[i])
                    bullsCount++;
            return bullsCount;
        }

        /// <summary>
        ///     Метод подсчета количества цифр в числе.
        /// </summary>
        /// <param name="number">Число</param>
        /// <returns>Количество цифр</returns>
        public static int IntSize(long number)
        {
            int size = 0;
            while ((number /= 10) != 0)
                size++;
            // Вычисление количества цифр в числе "отрезанием" последней.
            return size + 1;
        }

        /// <summary>
        ///     Метод проверки числа на повторения цифр внутри него
        /// </summary>
        /// <param name="number">Число</param>
        /// <returns>T - повторы есть, F - повторов нет</returns>
        public static bool IsRepeat(long number)
        {
            string stringNumber = number.ToString();
            for (int i = 0; i < stringNumber.Length; i++)
                for (int j = i + 1; j < stringNumber.Length; j++)
                    if (stringNumber[i] == stringNumber[j])
                        return true;
            return false;
        }

        private static void Main(string[] args)
        {
            GreetingsMessage();

            do
            {
                int numberSize;
                long playersNumber;

                Console.WriteLine("Введи число n[1;10] - количество цифр в загаданном числе");
                while (!int.TryParse(Console.ReadLine(), out numberSize) || numberSize < 1 || numberSize > 10)
                    Console.WriteLine("Некорректные данные, введи еще раз");
                //Проверка введенного размера на корректность.

                string hiddenNumber = GenValue(numberSize);
                do
                {
                    Console.WriteLine("Введи свое число, размером: " + numberSize);
                    while (!long.TryParse(Console.ReadLine(), out playersNumber) ||
                           IntSize(playersNumber) != numberSize || IsRepeat(playersNumber))
                        Console.WriteLine("Некорректные данные, введи еще раз");
                    // Проверка на соответствие введенного пользователем числа на размер и отсутствия повторений.

                    Console.WriteLine("Количество коров: {0}",
                        CowsCount(hiddenNumber, playersNumber.ToString()));
                    Console.WriteLine("Количество быков: {0}",
                        BullsCount(hiddenNumber, playersNumber.ToString()));
                } while (BullsCount(hiddenNumber, playersNumber.ToString()) != numberSize);

                FinishMessage(hiddenNumber);
            } while (Console.ReadLine() != "Exit");

            Console.WriteLine("\nСпасибо за игру!");
        }
    }
}