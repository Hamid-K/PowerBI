using System;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x02000002 RID: 2
	public abstract class OnDemandObjectModel : ObjectModel
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		public abstract Variables Variables { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		public abstract Lookups Lookups { get; }

		// Token: 0x06000003 RID: 3
		public abstract object MinValue(params object[] arguments);

		// Token: 0x06000004 RID: 4
		public abstract object MaxValue(params object[] arguments);
	}
}
