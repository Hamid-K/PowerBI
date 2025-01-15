using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000095 RID: 149
	internal sealed class SampleLimitOperator : LimitOperator
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x00026997 File Offset: 0x00024B97
		internal SampleLimitOperator(int count)
			: base(count)
		{
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000269A0 File Offset: 0x00024BA0
		internal override bool IgnoreLimitCount
		{
			get
			{
				return true;
			}
		}
	}
}
