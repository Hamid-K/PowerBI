using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C7 RID: 711
	public sealed class ReportVariantProperty : ReportProperty
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x0006BB34 File Offset: 0x00069D34
		internal ReportVariantProperty()
		{
			this.m_value = null;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0006BB43 File Offset: 0x00069D43
		internal ReportVariantProperty(bool isExpression)
			: base(isExpression, null)
		{
			this.m_value = null;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0006BB54 File Offset: 0x00069D54
		internal ReportVariantProperty(Microsoft.ReportingServices.ReportProcessing.ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.Value;
			}
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006BB8B File Offset: 0x00069D8B
		internal ReportVariantProperty(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.Value;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0006BBC2 File Offset: 0x00069DC2
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000D5F RID: 3423
		private object m_value;
	}
}
