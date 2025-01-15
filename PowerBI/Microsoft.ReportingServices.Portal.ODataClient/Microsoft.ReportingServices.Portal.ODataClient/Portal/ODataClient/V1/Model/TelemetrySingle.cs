using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E5 RID: 229
	[OriginalName("TelemetrySingle")]
	public class TelemetrySingle : DataServiceQuerySingle<Telemetry>
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x000147E5 File Offset: 0x000129E5
		public TelemetrySingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000147EF File Offset: 0x000129EF
		public TelemetrySingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000147FA File Offset: 0x000129FA
		public TelemetrySingle(DataServiceQuerySingle<Telemetry> query)
			: base(query)
		{
		}
	}
}
