using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002CE RID: 718
	public sealed class ReportColorProperty : ReportProperty
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x0006C060 File Offset: 0x0006A260
		internal ReportColorProperty(bool isExpression, string expressionString, ReportColor value, ReportColor defaultValue)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_value = value;
				return;
			}
			this.m_value = defaultValue;
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x0006C07D File Offset: 0x0006A27D
		public ReportColor Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000D66 RID: 3430
		private ReportColor m_value;
	}
}
