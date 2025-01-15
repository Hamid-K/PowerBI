using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B0 RID: 688
	public abstract class DataValueInstance : BaseInstance
	{
		// Token: 0x06001A5D RID: 6749 RVA: 0x0006A6A3 File Offset: 0x000688A3
		internal DataValueInstance(IReportScope repotScope)
			: base(repotScope)
		{
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06001A5E RID: 6750
		public abstract string Name { get; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06001A5F RID: 6751
		public abstract object Value { get; }
	}
}
