using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200006A RID: 106
	[OriginalName("AllowedActionSingle")]
	public class AllowedActionSingle : DataServiceQuerySingle<AllowedAction>
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0000A009 File Offset: 0x00008209
		public AllowedActionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000A013 File Offset: 0x00008213
		public AllowedActionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000A01E File Offset: 0x0000821E
		public AllowedActionSingle(DataServiceQuerySingle<AllowedAction> query)
			: base(query)
		{
		}
	}
}
