using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.ReportingServices.Common;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x02000079 RID: 121
	internal sealed class CancellationTokenManager : ICancellationTokenManager, IDisposable
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000A95A File Offset: 0x00008B5A
		public CancellationTokenManager()
			: this(new ConcurrentDictionary<Guid, CancellationTokenSource>())
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000A967 File Offset: 0x00008B67
		internal CancellationTokenManager(ConcurrentDictionary<Guid, CancellationTokenSource> dictionary)
		{
			this.m_cancellationTokenDictionary = dictionary;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000A978 File Offset: 0x00008B78
		public CancellationToken RegisterRequest(Guid? jobId)
		{
			if (Interlocked.Read(ref this.m_disposed) == 1L)
			{
				throw new ObjectDisposedException("CancellationTokenManager");
			}
			if (jobId == null)
			{
				return CancellationToken.None;
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			if (!this.m_cancellationTokenDictionary.TryAdd(jobId.Value, cancellationTokenSource))
			{
				ExploreHostUtils.TraceInsightsCancellationError(string.Format("Unable to register new cancellation token. Key already exists for {0}", jobId), null);
				cancellationTokenSource.Dispose();
				return CancellationToken.None;
			}
			return cancellationTokenSource.Token;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000A9F4 File Offset: 0x00008BF4
		public void CancelRequest(Guid jobId)
		{
			if (Interlocked.Read(ref this.m_disposed) == 1L)
			{
				throw new ObjectDisposedException("CancellationTokenManager");
			}
			CancellationTokenSource cancellationTokenSource;
			if (!this.m_cancellationTokenDictionary.TryGetValue(jobId, out cancellationTokenSource))
			{
				return;
			}
			try
			{
				cancellationTokenSource.Cancel();
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				ExploreHostUtils.TraceInsightsCancellationError("Exception during cancel request", ex);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000AA70 File Offset: 0x00008C70
		public void UnregisterRequest(Guid? jobId)
		{
			if (jobId == null)
			{
				return;
			}
			CancellationTokenSource cancellationTokenSource;
			if (!this.m_cancellationTokenDictionary.TryRemove(jobId.Value, out cancellationTokenSource))
			{
				return;
			}
			cancellationTokenSource.Dispose();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public void Dispose()
		{
			if (Interlocked.Read(ref this.m_disposed) == 0L)
			{
				foreach (CancellationTokenSource cancellationTokenSource in this.m_cancellationTokenDictionary.Values)
				{
					try
					{
						cancellationTokenSource.Cancel();
						cancellationTokenSource.Dispose();
					}
					catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						ExploreHostUtils.TraceInsightsCancellationError("Exception during dispose", ex);
					}
				}
				this.m_cancellationTokenDictionary.Clear();
			}
			Interlocked.Exchange(ref this.m_disposed, 1L);
		}

		// Token: 0x0400017A RID: 378
		private readonly ConcurrentDictionary<Guid, CancellationTokenSource> m_cancellationTokenDictionary;

		// Token: 0x0400017B RID: 379
		private long m_disposed;
	}
}
