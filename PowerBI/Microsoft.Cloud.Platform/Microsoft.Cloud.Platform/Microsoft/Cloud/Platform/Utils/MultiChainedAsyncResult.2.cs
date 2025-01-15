using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200025C RID: 604
	public class MultiChainedAsyncResult<TWorkTicket, TData> : MultiChainedAsyncResult<TWorkTicket> where TWorkTicket : WorkTicket
	{
		// Token: 0x06000FF8 RID: 4088 RVA: 0x000370C4 File Offset: 0x000352C4
		public MultiChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket)
			: base(callback, context, ticket)
		{
			this.Data = default(TData);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000370E9 File Offset: 0x000352E9
		public MultiChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket, TData data)
			: base(callback, context, ticket)
		{
			this.Data = data;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x000370FC File Offset: 0x000352FC
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x00037104 File Offset: 0x00035304
		public TData Data { get; set; }
	}
}
