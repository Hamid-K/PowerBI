using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000019 RID: 25
	public sealed class CrossPlatLock : IDisposable
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003610 File Offset: 0x00001810
		public CrossPlatLock(string lockfilePath, int lockFileRetryDelay = 100, int lockFileRetryCount = 600)
		{
			Exception ex = null;
			FileStream fileStream = null;
			Directory.CreateDirectory(Path.GetDirectoryName(lockfilePath));
			string text = string.Format("{0} {1}", SharedUtilities.GetCurrentProcessId(), SharedUtilities.GetCurrentProcessName());
			for (int i = 0; i < lockFileRetryCount; i++)
			{
				try
				{
					FileShare fileShare = FileShare.None;
					if (SharedUtilities.IsWindowsPlatform())
					{
						fileShare = FileShare.Read;
					}
					FileOptions fileOptions = FileOptions.DeleteOnClose;
					if (SharedUtilities.IsMonoPlatform())
					{
						fileOptions = FileOptions.None;
					}
					fileStream = new FileStream(lockfilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, fileShare, 4096, fileOptions);
					if (SharedUtilities.IsMonoPlatform())
					{
						fileStream.Lock(0L, 0L);
					}
					using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8, 4096, true))
					{
						streamWriter.WriteLine(text);
					}
					break;
				}
				catch (IOException ex2)
				{
					if (fileStream != null)
					{
						fileStream.Dispose();
					}
					fileStream = null;
					ex = ex2;
					Thread.Sleep(lockFileRetryDelay);
				}
				catch (UnauthorizedAccessException ex3)
				{
					if (fileStream != null)
					{
						fileStream.Dispose();
					}
					fileStream = null;
					ex = ex3;
					Thread.Sleep(lockFileRetryDelay);
				}
			}
			FileStream fileStream2 = fileStream;
			if (fileStream2 == null)
			{
				throw new InvalidOperationException("Could not get access to the shared lock file.", ex);
			}
			this._lockFileStream = fileStream2;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003734 File Offset: 0x00001934
		public void Dispose()
		{
			FileStream lockFileStream = this._lockFileStream;
			if (lockFileStream != null)
			{
				lockFileStream.Dispose();
			}
			this._lockFileStream = null;
		}

		// Token: 0x04000066 RID: 102
		internal const int LockfileRetryDelayDefault = 100;

		// Token: 0x04000067 RID: 103
		internal const int LockfileRetryCountDefault = 600;

		// Token: 0x04000068 RID: 104
		private FileStream _lockFileStream;
	}
}
