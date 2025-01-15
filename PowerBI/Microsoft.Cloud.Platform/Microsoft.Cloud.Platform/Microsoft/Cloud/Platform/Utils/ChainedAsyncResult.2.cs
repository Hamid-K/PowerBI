using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001BE RID: 446
	public class ChainedAsyncResult<TWorkTicket, TData> : ChainedAsyncResult<TWorkTicket> where TWorkTicket : WorkTicket
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x00027E60 File Offset: 0x00026060
		public ChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket)
			: base(callback, context, ticket)
		{
			this.m_data = default(TData);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00027E77 File Offset: 0x00026077
		public ChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket, IAsyncResult innerResult)
			: base(callback, context, ticket, innerResult)
		{
			this.m_data = default(TData);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00027E90 File Offset: 0x00026090
		public ChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket, TData data)
			: base(callback, context, ticket)
		{
			this.m_data = data;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00027EA3 File Offset: 0x000260A3
		public ChainedAsyncResult(AsyncCallback callback, object context, TWorkTicket ticket, TData data, IAsyncResult innerResult)
			: base(callback, context, ticket, innerResult)
		{
			this.m_data = data;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00027EB8 File Offset: 0x000260B8
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x00027EC0 File Offset: 0x000260C0
		public TData Data
		{
			get
			{
				return this.m_data;
			}
			set
			{
				this.m_data = value;
			}
		}

		// Token: 0x0400047C RID: 1148
		private TData m_data;
	}
}
