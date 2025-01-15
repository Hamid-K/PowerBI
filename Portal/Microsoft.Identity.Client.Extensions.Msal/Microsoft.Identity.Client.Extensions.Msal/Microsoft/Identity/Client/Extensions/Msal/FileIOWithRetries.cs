using System;
using System.IO;
using System.Threading;
using Microsoft.Identity.Client.Extensions.Msal.Accessors;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000016 RID: 22
	internal static class FileIOWithRetries
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002DE0 File Offset: 0x00000FE0
		internal static void DeleteCacheFile(string filePath, TraceSourceLogger logger)
		{
			bool flag = File.Exists(filePath);
			logger.LogInformation(string.Format("DeleteCacheFile Cache file exists '{0}'", flag));
			FileIOWithRetries.TryProcessFile(delegate
			{
				logger.LogInformation("Before deleting the cache file");
				try
				{
					File.Delete(filePath);
				}
				catch (Exception ex)
				{
					logger.LogError(string.Format("Problem deleting the cache file '{0}'", ex));
				}
				logger.LogInformation("After deleting the cache file.");
			}, logger);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E40 File Offset: 0x00001040
		internal static void CreateAndWriteToFile(string filePath, byte[] data, bool setChmod600, TraceSourceLogger logger)
		{
			FileIOWithRetries.EnsureParentDirectoryExists(filePath, logger);
			logger.LogInformation("Writing cache file");
			FileIOWithRetries.TryProcessFile(delegate
			{
				if (setChmod600)
				{
					logger.LogInformation("Writing file with chmod 600");
					FileWithPermissions.WriteToNewFileWithOwnerRWPermissions(filePath, data);
					return;
				}
				logger.LogInformation("Writing file without special permissions");
				File.WriteAllBytes(filePath, data);
			}, logger);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EA8 File Offset: 0x000010A8
		private static void EnsureParentDirectoryExists(string filePath, TraceSourceLogger logger)
		{
			if (!Directory.Exists(Path.GetDirectoryName(filePath)))
			{
				string directoryName = Path.GetDirectoryName(filePath);
				logger.LogInformation("Creating directory '" + directoryName + "'");
				Directory.CreateDirectory(directoryName);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EE8 File Offset: 0x000010E8
		internal static void TouchFile(string filePath, TraceSourceLogger logger)
		{
			FileIOWithRetries.EnsureParentDirectoryExists(filePath, logger);
			logger.LogInformation("Touching file...");
			FileIOWithRetries.TryProcessFile(delegate
			{
				if (!File.Exists(filePath))
				{
					logger.LogInformation("File " + filePath + " does not exist. Creating it..");
					File.Create(filePath).Dispose();
				}
				File.SetLastWriteTimeUtc(filePath, DateTime.UtcNow);
			}, logger);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F44 File Offset: 0x00001144
		internal static void TryProcessFile(Action action, TraceSourceLogger logger)
		{
			for (int i = 0; i <= 20; i++)
			{
				try
				{
					action();
					break;
				}
				catch (Exception ex)
				{
					Thread.Sleep(TimeSpan.FromMilliseconds(200.0));
					if (i == 20)
					{
						logger.LogError(string.Format("An exception was encountered while processing the cache file ex:'{0}'", ex));
					}
					else
					{
						logger.LogWarning(string.Format("An exception was encountered while processing the cache file. Operation will be retried. Ex:'{0}'", ex));
					}
				}
			}
		}

		// Token: 0x04000059 RID: 89
		private const int FileLockRetryCount = 20;

		// Token: 0x0400005A RID: 90
		private const int FileLockRetryWaitInMs = 200;
	}
}
