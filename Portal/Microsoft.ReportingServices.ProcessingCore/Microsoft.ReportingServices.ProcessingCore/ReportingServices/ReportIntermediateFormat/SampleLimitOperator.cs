using System;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003CA RID: 970
	internal sealed class SampleLimitOperator : DataShapeLimitOperator
	{
		// Token: 0x0600273D RID: 10045 RVA: 0x000BA695 File Offset: 0x000B8895
		public SampleLimitOperator(int count)
		{
			this.m_count = count;
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x000BA6A4 File Offset: 0x000B88A4
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000BA6AC File Offset: 0x000B88AC
		public override LimitOperator TranslateToRom()
		{
			return new SampleLimitOperator(this.Count);
		}

		// Token: 0x0400167B RID: 5755
		private readonly int m_count;
	}
}
