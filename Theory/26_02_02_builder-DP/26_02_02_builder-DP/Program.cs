using System;
using System.Collections.Generic;

public interface i_report
{
    string render();
}

public sealed class text_report : i_report
{
    private string title;
    private readonly List<string> lines = new List<string>();

    public void set_title(string title)
    {
        this.title = title;
    }

    public void add_line(string line)
    {
        lines.Add(line);
    }

    public string render()
    {
        string result = $"TITLE: {title}\n";
        for (int i = 0; i < lines.Count; i++)
            result += $"- {lines[i]}\n";
        return result;
    }
}

public sealed class json_report : i_report
{
    private string title;
    private readonly List<string> lines = new List<string>();

    public void set_title(string title)
    {
        this.title = title;
    }

    public void add_line(string line)
    {
        lines.Add(line);
    }

    public string render()
    {
        string escaped_title = escape(title);
        string result = $"{{\"title\":\"{escaped_title}\",\"lines\":[";
        for (int i = 0; i < lines.Count; i++)
        {
            result += $"\"{escape(lines[i])}\"";
            if (i < lines.Count - 1)
                result += ",";
        }
        result += "]}}";
        return result;
    }

    private static string escape(string value)
    {
        if (value == null)
            return "";
        return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}

public interface i_report_builder
{
    void reset();
    void set_title(string title);
    void add_line(string line);
    i_report build();
}

public sealed class text_report_builder : i_report_builder
{
    private text_report report;

    public text_report_builder()
    {
        reset();
    }

    public void reset()
    {
        report = new text_report();
    }

    public void set_title(string title)
    {
        report.set_title(title);
    }

    public void add_line(string line)
    {
        report.add_line(line);
    }

    public i_report build()
    {
        i_report result = report;
        reset();
        return result;
    }
}

public sealed class json_report_builder : i_report_builder
{
    private json_report report;

    public json_report_builder()
    {
        reset();
    }

    public void reset()
    {
        report = new json_report();
    }

    public void set_title(string title)
    {
        report.set_title(title);
    }

    public void add_line(string line)
    {
        report.add_line(line);
    }

    public i_report build()
    {
        i_report result = report;
        reset();
        return result;
    }
}

public class program
{
    public static void Main()
    {
        i_report_builder builder_1 = new text_report_builder();
        builder_1.set_title("weekly report");
        builder_1.add_line("users: 1200");
        builder_1.add_line("errors: 3");
        builder_1.add_line("avg response: 120ms");
        i_report report_1 = builder_1.build();
        Console.WriteLine(report_1.render());

        i_report_builder builder_2 = new json_report_builder();
        builder_2.set_title("weekly report");
        builder_2.add_line("users: 1200");
        builder_2.add_line("errors: 3");
        builder_2.add_line("avg response: 120ms");
        i_report report_2 = builder_2.build();
        Console.WriteLine(report_2.render());
    }
}
