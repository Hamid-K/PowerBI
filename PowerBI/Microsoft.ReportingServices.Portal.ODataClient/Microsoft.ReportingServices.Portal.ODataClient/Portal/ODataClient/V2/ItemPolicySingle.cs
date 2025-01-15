using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200001E RID: 30
	[OriginalName("ItemPolicySingle")]
	public class ItemPolicySingle : DataServiceQuerySingle<ItemPolicy>
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00003C35 File Offset: 0x00001E35
		public ItemPolicySingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003C3F File Offset: 0x00001E3F
		public ItemPolicySingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00003C4A File Offset: 0x00001E4A
		public ItemPolicySingle(DataServiceQuerySingle<ItemPolicy> query)
			: base(query)
		{
		}
	}
}
