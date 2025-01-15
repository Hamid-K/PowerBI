using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NLog.Targets
{
	// Token: 0x02000029 RID: 41
	internal class ColoredConsoleAnsiPrinter : IColoredConsolePrinter
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x0000A005 File Offset: 0x00008205
		public TextWriter AcquireTextWriter(TextWriter consoleStream, StringBuilder reusableBuilder)
		{
			return new StringWriter(reusableBuilder ?? new StringBuilder(50), consoleStream.FormatProvider);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000A020 File Offset: 0x00008220
		public void ReleaseTextWriter(TextWriter consoleWriter, TextWriter consoleStream, ConsoleColor? oldForegroundColor, ConsoleColor? oldBackgroundColor)
		{
			StringWriter stringWriter = consoleWriter as StringWriter;
			StringBuilder stringBuilder = ((stringWriter != null) ? stringWriter.GetStringBuilder() : null);
			if (stringBuilder != null)
			{
				stringBuilder.Append(ColoredConsoleAnsiPrinter.TerminalDefaultColorEscapeCode);
				consoleStream.WriteLine(stringBuilder.ToString());
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000A05C File Offset: 0x0000825C
		public ConsoleColor? ChangeForegroundColor(TextWriter consoleWriter, ConsoleColor? foregroundColor)
		{
			if (foregroundColor != null)
			{
				consoleWriter.Write(ColoredConsoleAnsiPrinter.GetForegroundColorEscapeCode(foregroundColor.Value));
			}
			return null;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000A090 File Offset: 0x00008290
		public ConsoleColor? ChangeBackgroundColor(TextWriter consoleWriter, ConsoleColor? backgroundColor)
		{
			if (backgroundColor != null)
			{
				consoleWriter.Write(ColoredConsoleAnsiPrinter.GetBackgroundColorEscapeCode(backgroundColor.Value));
			}
			return null;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000A0C1 File Offset: 0x000082C1
		public void ResetDefaultColors(TextWriter consoleWriter, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
		{
			consoleWriter.Write(ColoredConsoleAnsiPrinter.TerminalDefaultColorEscapeCode);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000A0D0 File Offset: 0x000082D0
		public void WriteSubString(TextWriter consoleWriter, string text, int index, int endIndex)
		{
			for (int i = index; i < endIndex; i++)
			{
				consoleWriter.Write(text[i]);
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000A0F7 File Offset: 0x000082F7
		public void WriteChar(TextWriter consoleWriter, char text)
		{
			consoleWriter.Write(text);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000A100 File Offset: 0x00008300
		public void WriteLine(TextWriter consoleWriter, string text)
		{
			consoleWriter.Write(text);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000A10C File Offset: 0x0000830C
		private static string GetForegroundColorEscapeCode(ConsoleColor color)
		{
			switch (color)
			{
			case ConsoleColor.Black:
				return "\u001b[30m";
			case ConsoleColor.DarkBlue:
				return "\u001b[34m";
			case ConsoleColor.DarkGreen:
				return "\u001b[32m";
			case ConsoleColor.DarkCyan:
				return "\u001b[36m";
			case ConsoleColor.DarkRed:
				return "\u001b[31m";
			case ConsoleColor.DarkMagenta:
				return "\u001b[35m";
			case ConsoleColor.DarkYellow:
				return "\u001b[33m";
			case ConsoleColor.Gray:
				return "\u001b[37m";
			case ConsoleColor.DarkGray:
				return "\u001b[90m";
			case ConsoleColor.Blue:
				return "\u001b[94m";
			case ConsoleColor.Green:
				return "\u001b[92m";
			case ConsoleColor.Cyan:
				return "\u001b[96m";
			case ConsoleColor.Red:
				return "\u001b[91m";
			case ConsoleColor.Magenta:
				return "\u001b[95m";
			case ConsoleColor.Yellow:
				return "\u001b[93m";
			case ConsoleColor.White:
				return "\u001b[97m";
			default:
				return ColoredConsoleAnsiPrinter.TerminalDefaultForegroundColorEscapeCode;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000A1C6 File Offset: 0x000083C6
		private static string TerminalDefaultForegroundColorEscapeCode
		{
			get
			{
				return "\u001b[39m\u001b[22m";
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000A1D0 File Offset: 0x000083D0
		private static string GetBackgroundColorEscapeCode(ConsoleColor color)
		{
			switch (color)
			{
			case ConsoleColor.Black:
				return "\u001b[40m";
			case ConsoleColor.DarkBlue:
				return "\u001b[44m";
			case ConsoleColor.DarkGreen:
				return "\u001b[42m";
			case ConsoleColor.DarkCyan:
				return "\u001b[46m";
			case ConsoleColor.DarkRed:
				return "\u001b[41m";
			case ConsoleColor.DarkMagenta:
				return "\u001b[45m";
			case ConsoleColor.DarkYellow:
				return "\u001b[43m";
			case ConsoleColor.Gray:
				return "\u001b[47m";
			case ConsoleColor.DarkGray:
				return "\u001b[100m";
			case ConsoleColor.Blue:
				return "\u001b[104m";
			case ConsoleColor.Green:
				return "\u001b[102m";
			case ConsoleColor.Cyan:
				return "\u001b[106m";
			case ConsoleColor.Red:
				return "\u001b[101m";
			case ConsoleColor.Magenta:
				return "\u001b[105m";
			case ConsoleColor.Yellow:
				return "\u001b[103m";
			case ConsoleColor.White:
				return "\u001b[107m";
			default:
				return ColoredConsoleAnsiPrinter.TerminalDefaultBackgroundColorEscapeCode;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000A28A File Offset: 0x0000848A
		private static string TerminalDefaultBackgroundColorEscapeCode
		{
			get
			{
				return "\u001b[49m";
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000A291 File Offset: 0x00008491
		private static string TerminalDefaultColorEscapeCode
		{
			get
			{
				return "\u001b[0m";
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000A298 File Offset: 0x00008498
		public IList<ConsoleRowHighlightingRule> DefaultConsoleRowHighlightingRules { get; } = new List<ConsoleRowHighlightingRule>
		{
			new ConsoleRowHighlightingRule("level == LogLevel.Fatal", ConsoleOutputColor.DarkRed, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Error", ConsoleOutputColor.DarkYellow, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Warn", ConsoleOutputColor.DarkMagenta, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Info", ConsoleOutputColor.NoChange, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Debug", ConsoleOutputColor.NoChange, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Trace", ConsoleOutputColor.NoChange, ConsoleOutputColor.NoChange)
		};
	}
}
