using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

public sealed class ColorConsoleFormatter : ConsoleFormatter
{
    private static readonly Lock Lock = new();
    
    public ColorConsoleFormatter() : base("color") { }

    public override void Write<TState>(in LogEntry<TState> entry,
        IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var msg = entry.Formatter?.Invoke(entry.State, entry.Exception) ?? entry.State?.ToString();
        if (string.IsNullOrEmpty(msg) && entry.Exception is null) return;

        var color = entry.LogLevel switch
        {
            LogLevel.Critical or LogLevel.Error => ConsoleColor.Red,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Information => ConsoleColor.Gray,
            LogLevel.Debug => ConsoleColor.DarkGray,
            LogLevel.Trace => ConsoleColor.DarkGray,
            _ => ConsoleColor.White
        };

        lock (Lock)
        {
            var prev = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                // Optionally print scopes:
                scopeProvider?.ForEachScope<object?>((scope, _) => textWriter.Write($"[{scope}] "), null);
                textWriter.WriteLine(msg + (entry.Exception != null ? $" {entry.Exception}" : ""));
            }
            finally { Console.ForegroundColor = prev; }
        }
    }
}