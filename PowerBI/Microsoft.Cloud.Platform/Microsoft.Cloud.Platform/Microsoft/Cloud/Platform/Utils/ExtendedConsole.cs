using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001C8 RID: 456
	public static class ExtendedConsole
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x000288B9 File Offset: 0x00026AB9
		public static void WriteLine(ConsoleColor color, string value)
		{
			ExtendedConsole.WriteLine(Console.Out, color, value, null);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000288C8 File Offset: 0x00026AC8
		[StringFormatMethod("format")]
		public static void WriteLine(ConsoleColor color, [NotNull] string format, params object[] args)
		{
			ExtendedConsole.WriteLine(Console.Out, color, format, args);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000288D7 File Offset: 0x00026AD7
		public static void WriteLineToError(ConsoleColor color, string value)
		{
			ExtendedConsole.WriteLine(Console.Error, color, value, null);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000288E6 File Offset: 0x00026AE6
		[StringFormatMethod("format")]
		public static void WriteLineToError(ConsoleColor color, [NotNull] string format, params object[] args)
		{
			ExtendedConsole.WriteLine(Console.Error, color, format, args);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000288F5 File Offset: 0x00026AF5
		public static void WriteLineToError(string value)
		{
			ExtendedConsole.WriteLine(Console.Error, ConsoleColor.Red, value, null);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00028905 File Offset: 0x00026B05
		[StringFormatMethod("format")]
		public static void WriteLineToError([NotNull] string format, params object[] args)
		{
			ExtendedConsole.WriteLine(Console.Error, ConsoleColor.Red, format, args);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00028915 File Offset: 0x00026B15
		public static void Minimize()
		{
			ExtendedConsole.NativeMethods.ShowWindow(Process.GetCurrentProcess().MainWindowHandle, 6);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00028928 File Offset: 0x00026B28
		public static void Restore()
		{
			ExtendedConsole.NativeMethods.ShowWindow(Process.GetCurrentProcess().MainWindowHandle, 9);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002893C File Offset: 0x00026B3C
		[StringFormatMethod("format")]
		private static void WriteLine(TextWriter stream, ConsoleColor color, [NotNull] string format, params object[] args)
		{
			object obj = ExtendedConsole.s_locker;
			lock (obj)
			{
				try
				{
					ConsoleColor foregroundColor = Console.ForegroundColor;
					Console.ForegroundColor = color;
					if (args != null)
					{
						stream.WriteLine(format, args);
					}
					else
					{
						stream.WriteLine(format);
					}
					Console.ForegroundColor = foregroundColor;
				}
				catch (ObjectDisposedException)
				{
					if (CurrentProcess.WellKnownHost != ProcessWellKnownHost.MSTest)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0400048C RID: 1164
		private static readonly object s_locker = new object();

		// Token: 0x02000682 RID: 1666
		private static class NativeMethods
		{
			// Token: 0x06002DBD RID: 11709
			[DllImport("user32.dll")]
			public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			// Token: 0x04001260 RID: 4704
			public const int SW_MINIMIZE = 6;

			// Token: 0x04001261 RID: 4705
			public const int SW_MAXIMIZE = 3;

			// Token: 0x04001262 RID: 4706
			public const int SW_RESTORE = 9;
		}
	}
}
