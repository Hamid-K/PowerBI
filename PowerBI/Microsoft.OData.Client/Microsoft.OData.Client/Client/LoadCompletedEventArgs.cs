using System;
using System.ComponentModel;

namespace Microsoft.OData.Client
{
	// Token: 0x020000BC RID: 188
	public sealed class LoadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x0001BD78 File Offset: 0x00019F78
		internal LoadCompletedEventArgs(QueryOperationResponse queryOperationResponse, Exception error)
			: this(queryOperationResponse, error, false)
		{
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001BD83 File Offset: 0x00019F83
		internal LoadCompletedEventArgs(QueryOperationResponse queryOperationResponse, Exception error, bool cancelled)
			: base(error, cancelled, null)
		{
			this.queryOperationResponse = queryOperationResponse;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001BD95 File Offset: 0x00019F95
		public QueryOperationResponse QueryOperationResponse
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.queryOperationResponse;
			}
		}

		// Token: 0x040002CE RID: 718
		private QueryOperationResponse queryOperationResponse;
	}
}
