using System;
using System.IO;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C6A RID: 7274
	public static class ContainerProcessLog
	{
		// Token: 0x0600B537 RID: 46391 RVA: 0x0024C9E8 File Offset: 0x0024ABE8
		public static string Setup(string logFolderPath)
		{
			try
			{
				if (logFolderPath != null && !Directory.Exists(logFolderPath))
				{
					Directory.CreateDirectory(logFolderPath);
				}
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				logFolderPath = null;
			}
			ContainerProcessLog.SetupCore(logFolderPath);
			return logFolderPath;
		}

		// Token: 0x0600B538 RID: 46392 RVA: 0x0024CA30 File Offset: 0x0024AC30
		public static StreamWriter CreateLog(string logFilePath)
		{
			Stream stream = Stream.Null;
			if (logFilePath != null)
			{
				try
				{
					stream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
			}
			return new StreamWriter(stream);
		}

		// Token: 0x0600B539 RID: 46393 RVA: 0x0024CA74 File Offset: 0x0024AC74
		public static void PruneLogs(string logFolderPath)
		{
			if (Directory.Exists(logFolderPath))
			{
				foreach (string text in Directory.GetFiles(logFolderPath))
				{
					try
					{
						using (new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.None))
						{
						}
						File.Delete(text);
					}
					catch (Exception ex)
					{
						if (!SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x0600B53A RID: 46394 RVA: 0x0000336E File Offset: 0x0000156E
		private static void SetupCore(string logFolderPath)
		{
		}

		// Token: 0x04005C90 RID: 23696
		public const string FolderName = "ContainerLogs";

		// Token: 0x04005C91 RID: 23697
		public const string LogFileArg = "--logfile";
	}
}
