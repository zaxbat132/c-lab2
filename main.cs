using System;
using System.Globalization;
using System.IO;
using System.Text;

internal class Program
{
    private string title;
    public string Title
    {
        get { return title; }
        set
        {
            if (value == null || value.Trim().Length == 0) throw new ArgumentException("Название не должно быть пустым.");
            title = value;
        }
    }
    public Program(string title)
    {
        Title = title;
    }
    public override string ToString()
    {
        return Title;
    }
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Program program = new Program("Лабораторная работа №2 — вариант 1");
        Console.WriteLine(program.ToString());
        try
        {
            while (true)
            {
                Console.WriteLine("\nВыберите задание:");
                Console.WriteLine("1 — Точка координат\n2 — Прямая\n3 — Студент\n4 — Создание объектов\n5 — Длина линии\n0 — Выход");
                int task = ReadInt("Задание", 0, 5);
                if (task == 0) break;
                while (true)
                {
                    Console.WriteLine("\nВыберите номер в задании " + task + ":");
                    if (task == 4) Console.WriteLine("1 — Создаём точку\n2 — Создаём линию");
                    else if (task == 5) Console.WriteLine("3 — Длина линии");
                    else Console.WriteLine("1 — " + TaskName(task));
                    Console.WriteLine("0 — Назад к заданиям");
                    int number = ReadInt("Номер", 0, task == 5 ? 3 : (task == 4 ? 2 : 1));
                    if (number == 0) break;
                    if (task == 5 && number != 3)
                    {
                        Console.WriteLine("В этом варианте доступен только номер 3.");
                        continue;
                    }
                    if (task == 1 || (task == 4 && number == 1)) ShowPoints();
                    else if (task == 2) ShowLines(true);
                    else if (task == 3) ShowStudents();
                    else if (task == 4) ShowLines(false);
                    else ShowLength();
                    Console.WriteLine("\nВыполнение завершено. Можно выбрать следующий номер.");
                }
            }
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nВвод завершён. Программа закрывается.");
        }
    }
    private static string TaskName(int task)
    {
        if (task == 1) return "Точка координат";
        if (task == 2) return "Прямая";
        return "Студент";
    }
    private static void ShowPoints()
    {
        Point[] points = new Point[3];
        for (int i = 0; i < points.Length; i++)
        {
            while (true)
            {
                Point candidate = ReadPoint("Точка " + (i + 1));
                bool duplicate = false;
                for (int j = 0; j < i; j++)
                    if (points[j].X == candidate.X && points[j].Y == candidate.Y) duplicate = true;
                if (!duplicate) { points[i] = candidate; break; }
                Console.WriteLine("Такая точка уже есть. Введите другую пару координат.");
            }
        }
        for (int i = 0; i < points.Length; i++)
            Console.WriteLine("Точка " + (i + 1) + ": " + points[i]);
    }
    private static void ShowLines(bool change)
    {
        Line first = new Line(ReadPoint("Начало линии 1"), ReadPoint("Конец линии 1"));
        Console.WriteLine("Линия 2 горизонтальная: обе точки имеют одинаковую высоту.");
        double y = ReadDouble("Высота Y");
        double x1 = ReadDouble("Начало X");
        double x2 = ReadDouble("Конец X");
        Line second = new Line(x1, y, x2, y);
        Line third = new Line(first.Start, second.End);
        PrintLines(first, second, third);
        if (!change) return;
        Console.WriteLine("\nМеняем координаты общих точек, сохраняя ссылки:");
        Point newStart = ReadPoint("Новое начало линии 1");
        Point newEnd = ReadPoint("Новый конец линии 2");
        first.Start.X = newStart.X;
        first.Start.Y = newStart.Y;
        second.End.X = newEnd.X;
        second.End.Y = newEnd.Y;
        PrintLines(first, second, third);
        Console.WriteLine("Линия 3 изменилась: её точки общие с линиями 1 и 2.");
        Console.WriteLine("\nТеперь заменяем точки линии 1 новыми объектами:");
        first.Start = ReadPoint("Независимое начало линии 1");
        first.End = ReadPoint("Независимый конец линии 1");
        PrintLines(first, second, third);
        Console.WriteLine("Линия 3 не изменилась: она сохранила ссылку на прежнюю точку начала.");
    }
    private static void PrintLines(Line first, Line second, Line third)
    {
        Console.WriteLine("Линия 1: " + first);
        Console.WriteLine("Линия 2: " + second);
        Console.WriteLine("Линия 3: " + third);
    }
    private static void ShowStudents()
    {
        int count = ReadInt("Количество оценок Васи", 1, 100);
        int[] grades = new int[count];
        for (int i = 0; i < count; i++) grades[i] = ReadInt("Оценка " + (i + 1), 2, 5);
        Student vasya = new Student("Вася", grades);
        Student petya = new Student("Петя", vasya.Grades);
        Console.WriteLine("\nДо изменения:");
        Console.WriteLine(vasya);
        Console.WriteLine(petya);
        petya.Grades[0] = 5;
        Console.WriteLine("\nПосле присваивания Петиной первой оценке значения 5:");
        Console.WriteLine(vasya);
        Console.WriteLine(petya);
        Console.WriteLine("Оба студента ссылаются на один массив, поэтому изменение видно у обоих.");
        Student andrey = new Student("Андрей", vasya.CopyGrades());
        Console.WriteLine("\nСоздана независимая копия:");
        Console.WriteLine(andrey);
        vasya.Grades[0] = 2;
        Console.WriteLine("\nПосле изменения первой оценки Васи на 2:");
        Console.WriteLine(vasya);
        Console.WriteLine(petya);
        Console.WriteLine(andrey);
        Console.WriteLine("У Андрея отдельный массив: его оценки не изменились.");
    }
    private static void ShowLength()
    {
        Line line = new Line(ReadPoint("Начало линии"), ReadPoint("Конец линии"));
        Console.WriteLine(line);
        Console.WriteLine("Длина линии (целое число): " + line.GetLength());
        Console.WriteLine("Дробная часть длины отбрасывается.");
    }
    private static string ReadText()
    {
        string text = Console.ReadLine();
        if (text == null) throw new EndOfStreamException();
        return text.Trim();
    }
    private static int ReadInt(string name, int min, int max)
    {
        while (true)
        {
            Console.Write(name + " (от " + min + " до " + max + "): ");
            int number;
            if (!int.TryParse(ReadText(), out number)) Console.WriteLine("Введите целое число.");
            else if (number < min || number > max) Console.WriteLine("Число вне допустимого диапазона.");
            else return number;
        }
    }
    private static double ReadDouble(string name)
    {
        while (true)
        {
            Console.Write(name + " (от -100000000 до 100000000: ");
            double number;
            string text = ReadText().Replace(',', '.');
            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out number))
                Console.WriteLine("Введите число, например 3,5 или -2.5.");
            else if (double.IsNaN(number) || double.IsInfinity(number) || Math.Abs(number) > 100000000)
                Console.WriteLine("Число вне допустимого диапазона.");
            else return number;
        }
    }
    private static Point ReadPoint(string name)
    {
        Console.WriteLine(name);
        double x = ReadDouble("X");
        double y = ReadDouble("Y");
        return new Point(x, y);
    }
}