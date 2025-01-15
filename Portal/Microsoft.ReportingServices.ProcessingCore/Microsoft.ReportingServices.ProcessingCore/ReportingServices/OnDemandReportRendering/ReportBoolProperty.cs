using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002CA RID: 714
	public sealed class ReportBoolProperty : ReportProperty
	{
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0006BD5D File Offset: 0x00069F5D
		internal ReportBoolProperty()
		{
			this.m_value = false;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0006BD6C File Offset: 0x00069F6C
		internal ReportBoolProperty(bool value)
		{
			this.m_value = value;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0006BD7B File Offset: 0x00069F7B
		internal ReportBoolProperty(Microsoft.ReportingServices.ReportProcessing.ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.BoolValue;
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0006BDB2 File Offset: 0x00069FB2
		internal ReportBoolProperty(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.BoolValue;
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0006BDE9 File Offset: 0x00069FE9
		internal ReportBoolProperty(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, bool value)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = value;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x0006BE1B File Offset: 0x0006A01B
		public bool Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000D62 RID: 3426
		private bool m_value;
	}
}
