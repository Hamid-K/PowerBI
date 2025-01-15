using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000062 RID: 98
	[OriginalName("TelemetrySingle")]
	public class TelemetrySingle : DataServiceQuerySingle<Telemetry>
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00009721 File Offset: 0x00007921
		public TelemetrySingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000972B File Offset: 0x0000792B
		public TelemetrySingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00009736 File Offset: 0x00007936
		public TelemetrySingle(DataServiceQuerySingle<Telemetry> query)
			: base(query)
		{
		}
	}
}
