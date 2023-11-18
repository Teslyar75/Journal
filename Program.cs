/*Задание 2:
Создайте программу для работы с информацией о журнале. 
Нужно хранить такую информацию об журнале:
1. Название журнала
2. Название издательства
3. Дата выпуска
4. Количество страниц
У программы должна быть такая функциональность:
1. Ввод информации о журнале
2. Вывод информации о журнале
3. Сериализация журнала
4. Сохранение сериализованного журнала в файл
5. Загрузка сериализованного журнала из файла. После 
загрузки нужно произвести десериализацию журнала.
Выбор конкретного формата сериализации необходимо сделать 
вам. Обращаем ваше внимание, что выбор должен быть 
обоснованным.*/
using System;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class Journal
{
    public string Title { get; set; }
    public string Publisher { get; set; }
    public DateTime Release_Date { get; set; }
    public int Page_Count { get; set; }

    public Journal() { } // Пустой конструктор необходим для XML-сериализации

    public Journal(string title, string publisher, DateTime release_Date, int page_Count)
    {
        Title = title;
        Publisher = publisher;
        Release_Date = release_Date;
        Page_Count = page_Count;
    }

    public void Print_Info()
    {
        Console.WriteLine($"Название журнала: {Title}");
        Console.WriteLine($"Издательство: {Publisher}");
        Console.WriteLine($"Дата выпуска: {Release_Date.ToShortDateString()}");
        Console.WriteLine($"Количество страниц: {Page_Count}");
    }
}

class Program
{
    static void Main()
    {
        Journal journal = null;

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Ввод информации о журнале");
            Console.WriteLine("2. Вывод информации о журнале");
            Console.WriteLine("3. Сериализация журнала и сохранение в файл");
            Console.WriteLine("4. Загрузка сериализованного журнала из файла и десериализация");
            Console.WriteLine("5. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal = Input_Journal_Info();
                    break;
                case "2":
                    Print_Journal_Info(journal);
                    break;
                case "3":
                    Serialize_And_Save(journal);
                    break;
                case "4":
                    Deserialize_From_File();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                    break;
            }
        }
    }

    static Journal Input_Journal_Info()
    {
        Console.Write("Введите название журнала: ");
        string title = Console.ReadLine();

        Console.Write("Введите название издательства: ");
        string publisher = Console.ReadLine();

        Console.Write("Введите дату выпуска (гггг-мм-дд): ");
        DateTime release_Date;
        while (!DateTime.TryParse(Console.ReadLine(), out release_Date))
        {
            Console.WriteLine("Некорректный ввод. Повторите попытку.");
        }

        Console.Write("Введите количество страниц: ");
        int page_Count;
        while (!int.TryParse(Console.ReadLine(), out page_Count))
        {
            Console.WriteLine("Некорректный ввод. Повторите попытку.");
        }

        return new Journal(title, publisher, release_Date, page_Count);
    }

    static void Print_Journal_Info(Journal journal)
    {
        if (journal != null)
        {
            Console.WriteLine("\nИнформация о журнале:");
            journal.Print_Info();
        }
        else
        {
            Console.WriteLine("Информация о журнале отсутствует. Введите данные сначала.");
        }
    }

    static void Serialize_And_Save(Journal journal)
    {
        if (journal != null)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Journal));
            using (TextWriter writer = new StreamWriter("journal.xml"))
            {
                serializer.Serialize(writer, journal);
                Console.WriteLine("\nЖурнал успешно сериализован в XML и сохранен в файл journal.xml");
            }
        }
        else
        {
            Console.WriteLine("Информация о журнале отсутствует. Введите данные сначала.");
        }
    }

    static void Deserialize_From_File()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Journal));

        try
        {
            using (TextReader reader = new StreamReader("journal.xml"))
            {
                Journal loaded_Journal = (Journal)serializer.Deserialize(reader);
                Console.WriteLine("\nЖурнал успешно загружен из файла и десериализован.");
                Console.WriteLine("\nИнформация о загруженном журнале:");
                loaded_Journal.Print_Info();
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден.");
        }
    }
}

