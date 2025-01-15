using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002CB RID: 715
	public sealed class ReportDoubleProperty : ReportProperty
	{
		// Token: 0x06001AF9 RID: 6905 RVA: 0x0006BE24 File Offset: 0x0006A024
		internal ReportDoubleProperty(Microsoft.ReportingServices.ReportProcessing.ExpressionInfo expressionInfo)
			: base(expressionInfo != null && expressionInfo.IsExpression, (expressionInfo == null) ? null : expressionInfo.OriginalText)
		{
			if (expressionInfo != null && !expressionInfo.IsExpression && !double.TryParse(expressionInfo.Value, out this.m_value))
			{
				this.m_value = 0.0;
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0006BE7C File Offset: 0x0006A07C
		internal ReportDoubleProperty(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
			: base(expressionInfo != null && expressionInfo.IsExpression, (expressionInfo == null) ? null : expressionInfo.OriginalText)
		{
			if (expressionInfo != null && !expressionInfo.IsExpression)
			{
				if (expressionInfo.ConstantType == DataType.Float)
				{
					this.m_value = expressionInfo.FloatValue;
					return;
				}
				if (!double.TryParse(expressionInfo.StringValue, out this.m_value))
				{
					this.m_value = 0.0;
				}
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0006BEEB File Offset: 0x0006A0EB
		public double Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000D63 RID: 3427
		private double m_value;
	}
}
