using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200008F RID: 143
	internal sealed class DataShapeInstance : DataRegionInstance
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x0002665E File Offset: 0x0002485E
		internal DataShapeInstance(DataShape dataShape)
			: base(dataShape)
		{
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00026667 File Offset: 0x00024867
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0002666F File Offset: 0x0002486F
		internal int PrimaryLeafCount { get; set; }

		// Token: 0x06000906 RID: 2310 RVA: 0x00026678 File Offset: 0x00024878
		internal override void SetNewContext()
		{
			base.SetNewContext();
			this.PrimaryLeafCount = 0;
		}
	}
}
