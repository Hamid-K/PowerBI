using System;
using System.Threading;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200001B RID: 27
	public sealed class QueryCancelTokenWrapper : IDisposable
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003274 File Offset: 0x00001474
		public QueryCancelTokenWrapper(string queryId, CancellationTokenSource tokenSource, IQueryCancellationManager cancellationManager)
			: this(queryId, tokenSource, tokenSource.Token, cancellationManager)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003285 File Offset: 0x00001485
		private QueryCancelTokenWrapper(string queryId, CancellationTokenSource tokenSource, CancellationToken token, IQueryCancellationManager cancellationManager)
		{
			this.m_tokenSource = tokenSource;
			this.QueryId = queryId;
			this.Token = token;
			this.CancellationManager = cancellationManager;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000032AA File Offset: 0x000014AA
		public CancellationToken Token { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000032B2 File Offset: 0x000014B2
		private IQueryCancellationManager CancellationManager { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000032BA File Offset: 0x000014BA
		private string QueryId { get; }

		// Token: 0x060000AF RID: 175 RVA: 0x000032C4 File Offset: 0x000014C4
		public void Dispose()
		{
			CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.m_tokenSource, null);
			if (cancellationTokenSource != null)
			{
				this.CancellationManager.CompleteRunningQuery(this.QueryId);
				cancellationTokenSource.Dispose();
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000032F8 File Offset: 0x000014F8
		internal void Cancel()
		{
			CancellationTokenSource tokenSource = this.m_tokenSource;
			if (tokenSource != null)
			{
				try
				{
					tokenSource.Cancel();
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x0400006E RID: 110
		public static readonly QueryCancelTokenWrapper None = new QueryCancelTokenWrapper(null, null, CancellationToken.None, NoOpQueryCancellationManager.Instance);

		// Token: 0x0400006F RID: 111
		private CancellationTokenSource m_tokenSource;
	}
}
