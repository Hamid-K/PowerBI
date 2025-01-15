using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NLog.Targets
{
	// Token: 0x0200003D RID: 61
	internal interface IColoredConsolePrinter
	{
		// Token: 0x06000687 RID: 1671
		TextWriter AcquireTextWriter(TextWriter consoleStream, StringBuilder reusableBuilder);

		// Token: 0x06000688 RID: 1672
		void ReleaseTextWriter(TextWriter consoleWriter, TextWriter consoleStream, ConsoleColor? oldForegroundColor, ConsoleColor? oldBackgroundColor);

		// Token: 0x06000689 RID: 1673
		ConsoleColor? ChangeForegroundColor(TextWriter consoleWriter, ConsoleColor? foregroundColor);

		// Token: 0x0600068A RID: 1674
		ConsoleColor? ChangeBackgroundColor(TextWriter consoleWriter, ConsoleColor? backgroundColor);

		// Token: 0x0600068B RID: 1675
		void ResetDefaultColors(TextWriter consoleWriter, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor);

		// Token: 0x0600068C RID: 1676
		void WriteSubString(TextWriter consoleWriter, string text, int index, int endIndex);

		// Token: 0x0600068D RID: 1677
		void WriteChar(TextWriter consoleWriter, char text);

		// Token: 0x0600068E RID: 1678
		void WriteLine(TextWriter consoleWriter, string text);

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600068F RID: 1679
		IList<ConsoleRowHighlightingRule> DefaultConsoleRowHighlightingRules { get; }
	}
}
