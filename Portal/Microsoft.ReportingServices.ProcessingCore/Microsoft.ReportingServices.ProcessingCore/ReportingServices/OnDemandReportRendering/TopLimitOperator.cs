using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000094 RID: 148
	internal sealed class TopLimitOperator : LimitOperator
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x0002698B File Offset: 0x00024B8B
		internal TopLimitOperator(int count)
			: base(count)
		{
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00026994 File Offset: 0x00024B94
		internal override bool IgnoreLimitCount
		{
			get
			{
				return false;
			}
		}
	}
}
