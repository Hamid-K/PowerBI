using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002CC RID: 716
	public sealed class ReportIntProperty : ReportProperty
	{
		// Token: 0x06001AFC RID: 6908 RVA: 0x0006BEF3 File Offset: 0x0006A0F3
		internal ReportIntProperty(int value)
		{
			this.m_value = value;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0006BF02 File Offset: 0x0006A102
		internal ReportIntProperty(bool isExpression, string expressionString, int value, int defaultValue)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_value = value;
				return;
			}
			this.m_value = defaultValue;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0006BF1F File Offset: 0x0006A11F
		internal ReportIntProperty(ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.IntValue;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0006BF56 File Offset: 0x0006A156
		public int Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000D64 RID: 3428
		private int m_value;
	}
}
