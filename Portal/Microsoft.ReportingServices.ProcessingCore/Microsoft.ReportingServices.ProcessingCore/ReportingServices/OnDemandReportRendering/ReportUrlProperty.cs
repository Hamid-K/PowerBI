using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002CF RID: 719
	public sealed class ReportUrlProperty : ReportProperty
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x0006C085 File Offset: 0x0006A285
		internal ReportUrlProperty(bool isExpression, string expressionString, ReportUrl reportUrl)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_reportUrl = reportUrl;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0006C099 File Offset: 0x0006A299
		public ReportUrl Value
		{
			get
			{
				return this.m_reportUrl;
			}
		}

		// Token: 0x04000D67 RID: 3431
		private ReportUrl m_reportUrl;
	}
}
