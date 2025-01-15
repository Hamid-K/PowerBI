using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200000B RID: 11
	public sealed class ScopedFileDelete : IDisposable
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000026F0 File Offset: 0x000008F0
		public ScopedFileDelete(FileInfo fileInfo)
			: this(fileInfo.FullName)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026FE File Offset: 0x000008FE
		public ScopedFileDelete(string filePath)
		{
			this.FullPath = Path.GetFullPath(filePath);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002712 File Offset: 0x00000912
		public void Dispose()
		{
			this.DeleteFileIfExists().ConfigureAwait(false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002724 File Offset: 0x00000924
		public async Task DeleteFileIfExists()
		{
			if (!string.IsNullOrWhiteSpace(this.FullPath) && File.Exists(this.FullPath))
			{
				try
				{
					Logger.Trace("Deleting scoped temp file : {0}", new object[] { this.FullPath });
					File.Delete(this.FullPath);
					return;
				}
				catch (Exception ex)
				{
					Logger.Debug(ex, "Failed to delete temp file {0}, will reschedule one retry", new object[] { this.FullPath });
				}
				await this.SingleDeleteRetry();
			}
			else
			{
				Logger.Trace("Skipping delete of non-existant scoped temp file : {0}", new object[] { this.FullPath });
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000276C File Offset: 0x0000096C
		private async Task SingleDeleteRetry()
		{
			await Task.Delay(ScopedFileDelete.DeleteRetrySpan);
			try
			{
				Logger.Trace("Retry: Deleting scoped temp file : {0}", new object[] { this.FullPath });
				File.Delete(this.FullPath);
			}
			catch (Exception ex)
			{
				Logger.Warning(ex, "Retry: Failed to delete temp file {0}. Abandoning...", new object[] { this.FullPath });
			}
		}

		// Token: 0x04000038 RID: 56
		private static readonly int deleteRetryMintues = StaticConfig.Current.GetIntOrDefault("TempFileDeleteRetryMinutes", 2);

		// Token: 0x04000039 RID: 57
		private static readonly TimeSpan DeleteRetrySpan = TimeSpan.FromMinutes((double)ScopedFileDelete.deleteRetryMintues);

		// Token: 0x0400003A RID: 58
		public readonly string FullPath;
	}
}
