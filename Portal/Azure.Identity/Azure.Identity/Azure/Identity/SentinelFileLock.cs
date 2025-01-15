using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Identity
{
	// Token: 0x0200007D RID: 125
	internal class SentinelFileLock : IDisposable
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0000D315 File Offset: 0x0000B515
		private SentinelFileLock(FileStream lockFileStream)
		{
			this._lockFileStream = lockFileStream;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000D324 File Offset: 0x0000B524
		public static async Task<SentinelFileLock> AcquireAsync(string lockfilePath, int lockFileRetryCount, TimeSpan lockFileRetryDelay)
		{
			Exception exception = null;
			FileStream fileStream = null;
			Directory.CreateDirectory(Path.GetDirectoryName(lockfilePath));
			int num;
			for (int tryCount = 0; tryCount < lockFileRetryCount; tryCount = num + 1)
			{
				num = 0;
				object obj;
				Exception ex;
				bool flag;
				Exception ex2;
				try
				{
					fileStream = new FileStream(lockfilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose);
					using (StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8, 4096, true))
					{
						await writer.WriteLineAsync(string.Format("{0} {1}", Process.GetCurrentProcess().Id, Process.GetCurrentProcess().ProcessName)).ConfigureAwait(false);
					}
					StreamWriter writer = null;
					break;
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					ex = obj as Exception;
					if (ex == null)
					{
						flag = false;
					}
					else
					{
						ex2 = (Exception)ex;
						flag = (ex2 is IOException || ex2 is UnauthorizedAccessException) > false;
					}
					endfilter(flag);
				})
				{
					num = 1;
				}
				if (num == 1)
				{
					exception = ex2;
					await Task.Delay(lockFileRetryDelay).ConfigureAwait(false);
				}
				num = tryCount;
			}
			if (fileStream == null)
			{
				throw new InvalidOperationException("Could not get access to the shared lock file.", exception);
			}
			return new SentinelFileLock(fileStream);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000D377 File Offset: 0x0000B577
		public void Dispose()
		{
			FileStream lockFileStream = this._lockFileStream;
			if (lockFileStream != null)
			{
				lockFileStream.Dispose();
			}
			this._lockFileStream = null;
		}

		// Token: 0x0400026C RID: 620
		private FileStream _lockFileStream;

		// Token: 0x0400026D RID: 621
		private const int DefaultFileBufferSize = 4096;
	}
}
