using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001BD RID: 445
	public class ChainedAsyncResult<TWorkTicket> : AsyncResult where TWorkTicket : WorkTicket
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x00027DE3 File Offset: 0x00025FE3
		public ChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket)
			: this(callback, context, ticket, null)
		{
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00027DEF File Offset: 0x00025FEF
		public ChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket, IAsyncResult innerResult)
			: base(callback, context)
		{
			this.m_ticket = ticket;
			this.m_innerResult = innerResult;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00027E08 File Offset: 0x00026008
		public TWorkTicket WorkTicket
		{
			get
			{
				return this.m_ticket;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00027E10 File Offset: 0x00026010
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00027E18 File Offset: 0x00026018
		public IAsyncResult InnerResult
		{
			get
			{
				return this.m_innerResult;
			}
			set
			{
				this.m_innerResult = value;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00027E21 File Offset: 0x00026021
		public bool HasValidWorkTicket()
		{
			return this.m_ticket != null && this.m_ticket.IsValid();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00027E42 File Offset: 0x00026042
		public void SignalCompletion(bool completedSynchronously)
		{
			base.SignalCompletionInternal(completedSynchronously);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00023A2D File Offset: 0x00021C2D
		public void SignalCompletion(bool completedSynchronously, Exception ex)
		{
			base.SignalCompletionInternal(completedSynchronously, ex);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00023D93 File Offset: 0x00021F93
		public void End()
		{
			base.EndInternal();
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00027E4B File Offset: 0x0002604B
		public void BeginAsyncFunctionCallback(IAsyncResult asyncResult)
		{
			this.m_innerResult = asyncResult;
			this.SignalCompletion(asyncResult.CompletedSynchronously);
		}

		// Token: 0x0400047A RID: 1146
		private TWorkTicket m_ticket;

		// Token: 0x0400047B RID: 1147
		private IAsyncResult m_innerResult;
	}
}
