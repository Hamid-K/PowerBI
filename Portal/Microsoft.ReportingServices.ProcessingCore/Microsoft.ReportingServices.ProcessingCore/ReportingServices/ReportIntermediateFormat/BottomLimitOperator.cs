using System;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003CB RID: 971
	internal sealed class BottomLimitOperator : DataShapeLimitOperator
	{
		// Token: 0x06002740 RID: 10048 RVA: 0x000BA6B9 File Offset: 0x000B88B9
		public BottomLimitOperator(int count)
		{
			this.m_count = count;
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x000BA6C8 File Offset: 0x000B88C8
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000BA6D0 File Offset: 0x000B88D0
		public override LimitOperator TranslateToRom()
		{
			return new BottomLimitOperator(this.Count);
		}

		// Token: 0x0400167C RID: 5756
		private readonly int m_count;
	}
}
