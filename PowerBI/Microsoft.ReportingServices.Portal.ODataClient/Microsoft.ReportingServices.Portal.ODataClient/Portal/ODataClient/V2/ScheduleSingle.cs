using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200004D RID: 77
	[OriginalName("ScheduleSingle")]
	public class ScheduleSingle : DataServiceQuerySingle<Schedule>
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00008162 File Offset: 0x00006362
		public ScheduleSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000816C File Offset: 0x0000636C
		public ScheduleSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00008177 File Offset: 0x00006377
		public ScheduleSingle(DataServiceQuerySingle<Schedule> query)
			: base(query)
		{
		}
	}
}
