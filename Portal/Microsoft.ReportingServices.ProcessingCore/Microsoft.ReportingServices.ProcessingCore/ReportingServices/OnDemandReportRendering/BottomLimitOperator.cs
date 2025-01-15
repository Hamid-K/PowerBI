using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000096 RID: 150
	internal sealed class BottomLimitOperator : LimitOperator
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x000269A3 File Offset: 0x00024BA3
		internal BottomLimitOperator(int count)
			: base(count)
		{
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000269AC File Offset: 0x00024BAC
		internal override bool IgnoreLimitCount
		{
			get
			{
				return true;
			}
		}
	}
}
