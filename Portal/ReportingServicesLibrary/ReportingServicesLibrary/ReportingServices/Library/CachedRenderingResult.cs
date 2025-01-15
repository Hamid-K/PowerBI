using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200026B RID: 619
	internal sealed class CachedRenderingResult : HierarchicalCachedItem, IDisposable
	{
		// Token: 0x0600164D RID: 5709 RVA: 0x00058CEC File Offset: 0x00056EEC
		internal CachedRenderingResult(ReportRenderingResult renderingResult, RSStream stream)
		{
			this.m_renderingResult = new ReportRenderingResult(renderingResult, null);
			this.m_cachedData = new CachedData(stream);
			this.m_inMemorySizeKb = this.m_cachedData.GetInMemoryByteCount() / 1024L;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00058D3B File Offset: 0x00056F3B
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00058D44 File Offset: 0x00056F44
		private void Dispose(bool disposing)
		{
			object sync = this.m_sync;
			lock (sync)
			{
				try
				{
					if (!this.m_disposed && disposing)
					{
						if (this.m_cachedData != null)
						{
							this.m_cachedData.Dispose();
						}
						this.m_cachedData = null;
						if (this.m_renderingResult != null)
						{
							this.m_renderingResult.Dispose();
						}
						this.m_renderingResult = null;
					}
				}
				finally
				{
					this.m_disposed = true;
				}
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00058DD4 File Offset: 0x00056FD4
		internal ReportRenderingResult GetRenderingResult()
		{
			object sync = this.m_sync;
			ReportRenderingResult reportRenderingResult;
			lock (sync)
			{
				if (this.m_disposed)
				{
					reportRenderingResult = null;
				}
				else
				{
					CachedData.CacheStream newStream = this.m_cachedData.GetNewStream();
					if (newStream != null)
					{
						reportRenderingResult = new ReportRenderingResult(this.m_renderingResult, newStream);
					}
					else
					{
						reportRenderingResult = null;
					}
				}
			}
			return reportRenderingResult;
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00058E3C File Offset: 0x0005703C
		public CachedData Data
		{
			get
			{
				return this.m_cachedData;
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00058E44 File Offset: 0x00057044
		public override void NotifyItemIsCached()
		{
			if (this.m_cachedData != null)
			{
				this.m_cachedData.MarkDataAsCached();
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x00058E59 File Offset: 0x00057059
		public override long SizeEstimateKb
		{
			get
			{
				return this.m_inMemorySizeKb;
			}
		}

		// Token: 0x04000822 RID: 2082
		private ReportRenderingResult m_renderingResult;

		// Token: 0x04000823 RID: 2083
		private bool m_disposed;

		// Token: 0x04000824 RID: 2084
		private CachedData m_cachedData;

		// Token: 0x04000825 RID: 2085
		private long m_inMemorySizeKb;

		// Token: 0x04000826 RID: 2086
		private readonly object m_sync = new object();
	}
}
