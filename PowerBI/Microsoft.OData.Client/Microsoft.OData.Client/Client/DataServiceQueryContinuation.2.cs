using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200004A RID: 74
	public sealed class DataServiceQueryContinuation<T> : DataServiceQueryContinuation
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00009514 File Offset: 0x00007714
		internal DataServiceQueryContinuation(Uri nextLinkUri, ProjectionPlan plan)
			: base(nextLinkUri, plan)
		{
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000951E File Offset: 0x0000771E
		internal override Type ElementType
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
