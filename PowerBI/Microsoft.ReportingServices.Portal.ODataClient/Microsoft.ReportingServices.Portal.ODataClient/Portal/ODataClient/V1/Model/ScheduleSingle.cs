using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000CE RID: 206
	[OriginalName("ScheduleSingle")]
	public class ScheduleSingle : DataServiceQuerySingle<Schedule>
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00012B81 File Offset: 0x00010D81
		public ScheduleSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00012B8B File Offset: 0x00010D8B
		public ScheduleSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00012B96 File Offset: 0x00010D96
		public ScheduleSingle(DataServiceQuerySingle<Schedule> query)
			: base(query)
		{
		}
	}
}
