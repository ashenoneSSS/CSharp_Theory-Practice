using System;

public class number_source
{
    public event Action<int> number_received;

    public void process_text(string text)
    {
        int i = 0;

        while (i < text.Length)
        {
            while (i < text.Length && (text[i] == ' ' || text[i] == ',' || text[i] == ';'))
                i++;

            int sign = 1;
            if (i < text.Length && text[i] == '-')
            {
                sign = -1;
                i++;
            }

            bool has_digits = false;
            int value = 0;

            while (i < text.Length && text[i] >= '0' && text[i] <= '9')
            {
                has_digits = true;
                value = value * 10 + (text[i] - '0');
                i++;
            }

            if (has_digits)
                number_received?.Invoke(sign * value);
            else
                i++;
        }
    }
}

public class stats
{
    private int sum;

    public void on_number(int x)
    {
        sum += x;
        Console.WriteLine($"STATS: sum = {sum}");
    }
}

public class program
{
    static void Main()
    {
        var source = new number_source();
        var s = new stats();

        source.number_received += ui_show;
        source.number_received += log_write;
        source.number_received += s.on_number;

        source.process_text("10, 20, -5;  3");

        source.number_received -= log_write;

        source.process_text("7, 8");
    }

    static void ui_show(int x)
    {
        Console.WriteLine($"UI: last number = {x}");
    }

    static void log_write(int x)
    {
        Console.WriteLine($"LOG: got {x}");
    }
}

// Microsoft Learn:
// https://learn.microsoft.com/dotnet/csharp/programming-guide/events/
// https://learn.microsoft.com/dotnet/api/system.action-1
