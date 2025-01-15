using System;
using System.IO;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001950 RID: 6480
	public class CacheSession : ICacheSession, IDisposable
	{
		// Token: 0x0600A44F RID: 42063 RVA: 0x00220508 File Offset: 0x0021E708
		public CacheSession(string path, Action<Exception> onException)
		{
			this.cachePath = Path.Combine(path, "Caches\\" + Guid.NewGuid().ToString());
			this.onException = onException;
		}

		// Token: 0x170029ED RID: 10733
		// (get) Token: 0x0600A450 RID: 42064 RVA: 0x0022054B File Offset: 0x0021E74B
		public string CachePath
		{
			get
			{
				return this.cachePath;
			}
		}

		// Token: 0x0600A451 RID: 42065 RVA: 0x00220554 File Offset: 0x0021E754
		public void Dispose()
		{
			try
			{
				Directory.Delete(this.cachePath, true);
			}
			catch (IOException ex)
			{
				this.onException(ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				this.onException(ex2);
			}
		}

		// Token: 0x04005592 RID: 21906
		private string cachePath;

		// Token: 0x04005593 RID: 21907
		private Action<Exception> onException;
	}
}
