using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Identity
{
	// Token: 0x0200005D RID: 93
	internal class FileContentsCache
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public FileContentsCache(string tokenFilePath, TimeSpan? refreshInterval = null)
		{
			this._refreshInterval = refreshInterval ?? TimeSpan.FromMinutes(5.0);
			this._tokenFilePath = tokenFilePath;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000A958 File Offset: 0x00008B58
		public async Task<string> GetTokenFileContentsAsync(CancellationToken cancellationToken)
		{
			if (this._refreshOn <= DateTimeOffset.UtcNow)
			{
				await this._lock.WaitAsync(cancellationToken).ConfigureAwait(false);
				try
				{
					if (this._refreshOn <= DateTimeOffset.UtcNow)
					{
						using (StreamReader reader = File.OpenText(this._tokenFilePath))
						{
							this._tokenFileContents = await reader.ReadToEndAsync().ConfigureAwait(false);
							this._refreshOn = DateTimeOffset.UtcNow + this._refreshInterval;
						}
						StreamReader reader = null;
					}
				}
				finally
				{
					this._lock.Release();
				}
			}
			return this._tokenFileContents;
		}

		// Token: 0x04000204 RID: 516
		private SemaphoreSlim _lock = new SemaphoreSlim(1);

		// Token: 0x04000205 RID: 517
		private readonly string _tokenFilePath;

		// Token: 0x04000206 RID: 518
		private string _tokenFileContents;

		// Token: 0x04000207 RID: 519
		private DateTimeOffset _refreshOn = DateTimeOffset.MinValue;

		// Token: 0x04000208 RID: 520
		private readonly TimeSpan _refreshInterval;
	}
}
