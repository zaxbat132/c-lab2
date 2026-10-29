using System;
using System.Text;

// Задания 1.1 и 4.1
public class Point
{
    private double x;
    private double y;

    public double X
    {
        get { return x; }
        set
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || Math.Abs(value) > 100000000)
                throw new ArgumentOutOfRangeException("value",
                    "Координата должна быть от -100000000 до 100000000.");
            x = value;
        }
    }
    public double Y
    {
        get { return y; }
        set
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || Math.Abs(value) > 100000000)
                throw new ArgumentOutOfRangeException("value",
                    "Координата должна быть от -100000000 до 100000000.");
            y = value;
        }
    }
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
    public override string ToString()
    {
        return "{" + X + ";" + Y + "}";
    }
}

// Задания 2.1, 4.2 и 5.3
public class Line
{
    private Point start;
    private Point end;

    public Point Start
    {
        get { return start; }
        set
        {
            if (value == null) throw new ArgumentNullException("value");
            start = value;
        }
    }
    public Point End
    {
        get { return end; }
        set
        {
            if (value == null) throw new ArgumentNullException("value");
            end = value;
        }
    }
    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }
    public Line(double x1, double y1, double x2, double y2)
        : this(new Point(x1, y1), new Point(x2, y2))
    {
    }
    public int GetLength()
    {
        double dx = End.X - Start.X;
        double dy = End.Y - Start.Y;
        return (int)Math.Sqrt(dx * dx + dy * dy);
    }
    public override string ToString()
    {
        return "Линия от " + Start + " до " + End;
    }
}

// Задание 3.1.
public class Student
{
    private string name;
    private int[] grades;

    public string Name
    {
        get { return name; }
        set
        {
            if (value == null || value.Trim().Length == 0)
                throw new ArgumentException("Имя не должно быть пустым.");
            name = value.Trim();
        }
    }
    public int[] Grades
    {
        get { return grades; }
        set
        {
            if (value == null) throw new ArgumentNullException("value");
            grades = value;
        }
    }
    public Student(string name, int[] grades)
    {
        Name = name;
        Grades = grades;
    }
    public int[] CopyGrades()
    {
        int[] copy = new int[Grades.Length];
        for (int i = 0; i < Grades.Length; i++) copy[i] = Grades[i];
        return copy;
    }
    public override string ToString()
    {
        StringBuilder result = new StringBuilder(Name + ": [");
        for (int i = 0; i < Grades.Length; i++)
        {
            if (i > 0) result.Append(", ");
            result.Append(Grades[i]);
        }
        result.Append("]");
        return result.ToString();
    }
}