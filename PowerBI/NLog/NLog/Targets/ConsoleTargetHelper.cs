using System;
using System.IO;
using System.Text;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x0200002F RID: 47
	internal static class ConsoleTargetHelper
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x0000B254 File Offset: 0x00009454
		public static bool IsConsoleAvailable(out string reason)
		{
			reason = string.Empty;
			try
			{
				if (!Environment.UserInteractive)
				{
					if (PlatformDetector.IsMono && Console.In is StreamReader)
					{
						return true;
					}
					reason = "Environment.UserInteractive = False";
					return false;
				}
				else if (Console.OpenStandardInput(1) == Stream.Null)
				{
					reason = "Console.OpenStandardInput = Null";
					return false;
				}
			}
			catch (Exception ex)
			{
				reason = "Unexpected exception: " + ex.GetType().Name + ":" + ex.Message;
				InternalLogger.Warn(ex, "Failed to detect whether console is available.");
				return false;
			}
			return true;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public static Encoding GetConsoleOutputEncoding(Encoding currentEncoding, bool isInitialized, bool pauseLogging)
		{
			if (currentEncoding != null)
			{
				return currentEncoding;
			}
			string text;
			if ((isInitialized && !pauseLogging) || ConsoleTargetHelper.IsConsoleAvailable(out text))
			{
				return Console.OutputEncoding;
			}
			return Encoding.Default;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000B31C File Offset: 0x0000951C
		public static bool SetConsoleOutputEncoding(Encoding newEncoding, bool isInitialized, bool pauseLogging)
		{
			if (!isInitialized)
			{
				return true;
			}
			if (!pauseLogging)
			{
				try
				{
					Console.OutputEncoding = newEncoding;
					return true;
				}
				catch (Exception ex)
				{
					InternalLogger.Warn(ex, "Failed changing Console.OutputEncoding to {0}", new object[] { newEncoding });
				}
				return false;
			}
			return false;
		}
	}
}
