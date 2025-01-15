using System;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003C9 RID: 969
	internal sealed class TopLimitOperator : DataShapeLimitOperator
	{
		// Token: 0x0600273A RID: 10042 RVA: 0x000BA671 File Offset: 0x000B8871
		public TopLimitOperator(int count)
		{
			this.m_count = count;
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x000BA680 File Offset: 0x000B8880
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000BA688 File Offset: 0x000B8888
		public override LimitOperator TranslateToRom()
		{
			return new TopLimitOperator(this.Count);
		}

		// Token: 0x0400167A RID: 5754
		private readonly int m_count;
	}
}
