using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F2 RID: 242
	[OriginalName("AllowedActionSingle")]
	public class AllowedActionSingle : DataServiceQuerySingle<AllowedAction>
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00015441 File Offset: 0x00013641
		public AllowedActionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001544B File Offset: 0x0001364B
		public AllowedActionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00015456 File Offset: 0x00013656
		public AllowedActionSingle(DataServiceQuerySingle<AllowedAction> query)
			: base(query)
		{
		}
	}
}
