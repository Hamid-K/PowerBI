using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NLog.Targets
{
	// Token: 0x0200002A RID: 42
	internal class ColoredConsoleSystemPrinter : IColoredConsolePrinter
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0000A351 File Offset: 0x00008551
		public TextWriter AcquireTextWriter(TextWriter consoleStream, StringBuilder reusableBuilder)
		{
			return consoleStream;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000A354 File Offset: 0x00008554
		public void ReleaseTextWriter(TextWriter consoleWriter, TextWriter consoleStream, ConsoleColor? oldForegroundColor, ConsoleColor? oldBackgroundColor)
		{
			this.ResetDefaultColors(consoleWriter, oldForegroundColor, oldBackgroundColor);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000A360 File Offset: 0x00008560
		public ConsoleColor? ChangeForegroundColor(TextWriter consoleWriter, ConsoleColor? foregroundColor)
		{
			ConsoleColor foregroundColor2 = Console.ForegroundColor;
			if (foregroundColor != null && foregroundColor2 != foregroundColor.Value)
			{
				Console.ForegroundColor = foregroundColor.Value;
			}
			return new ConsoleColor?(foregroundColor2);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000A398 File Offset: 0x00008598
		public ConsoleColor? ChangeBackgroundColor(TextWriter consoleWriter, ConsoleColor? backgroundColor)
		{
			ConsoleColor backgroundColor2 = Console.BackgroundColor;
			if (backgroundColor != null && backgroundColor2 != backgroundColor.Value)
			{
				Console.BackgroundColor = backgroundColor.Value;
			}
			return new ConsoleColor?(backgroundColor2);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000A3D0 File Offset: 0x000085D0
		public void ResetDefaultColors(TextWriter consoleWriter, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
		{
			if (foregroundColor != null)
			{
				Console.ForegroundColor = foregroundColor.Value;
			}
			if (backgroundColor != null)
			{
				Console.BackgroundColor = backgroundColor.Value;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000A3FC File Offset: 0x000085FC
		public void WriteSubString(TextWriter consoleWriter, string text, int index, int endIndex)
		{
			consoleWriter.Write(text.Substring(index, endIndex - index));
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000A40F File Offset: 0x0000860F
		public void WriteChar(TextWriter consoleWriter, char text)
		{
			consoleWriter.Write(text);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000A418 File Offset: 0x00008618
		public void WriteLine(TextWriter consoleWriter, string text)
		{
			consoleWriter.WriteLine(text);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000A421 File Offset: 0x00008621
		public IList<ConsoleRowHighlightingRule> DefaultConsoleRowHighlightingRules { get; } = new List<ConsoleRowHighlightingRule>
		{
			new ConsoleRowHighlightingRule("level == LogLevel.Fatal", ConsoleOutputColor.Red, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Error", ConsoleOutputColor.Yellow, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Warn", ConsoleOutputColor.Magenta, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Info", ConsoleOutputColor.White, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Debug", ConsoleOutputColor.Gray, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Trace", ConsoleOutputColor.DarkGray, ConsoleOutputColor.NoChange)
		};
	}
}
