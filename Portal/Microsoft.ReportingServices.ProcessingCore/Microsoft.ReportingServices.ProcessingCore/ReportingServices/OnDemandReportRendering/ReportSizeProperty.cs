using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002CD RID: 717
	public sealed class ReportSizeProperty : ReportProperty
	{
		// Token: 0x06001B00 RID: 6912 RVA: 0x0006BF5E File Offset: 0x0006A15E
		internal ReportSizeProperty(bool isExpression, string expressionString, ReportSize value)
			: this(isExpression, expressionString, value, null)
		{
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006BF6A File Offset: 0x0006A16A
		internal ReportSizeProperty(bool isExpression, string expressionString, ReportSize value, ReportSize defaultValue)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_value = value;
				return;
			}
			this.m_value = defaultValue;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0006BF87 File Offset: 0x0006A187
		internal ReportSizeProperty(ExpressionInfo expressionInfo)
			: base(expressionInfo != null && expressionInfo.IsExpression, (expressionInfo == null) ? null : expressionInfo.OriginalText)
		{
			if (expressionInfo != null && !expressionInfo.IsExpression)
			{
				this.m_value = new ReportSize(expressionInfo.StringValue);
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006BFC3 File Offset: 0x0006A1C3
		internal ReportSizeProperty(ExpressionInfo expressionInfo, bool allowNegative)
			: base(expressionInfo != null && expressionInfo.IsExpression, (expressionInfo == null) ? null : expressionInfo.OriginalText)
		{
			if (expressionInfo != null && !expressionInfo.IsExpression)
			{
				this.m_value = new ReportSize(expressionInfo.StringValue, true, allowNegative);
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0006C004 File Offset: 0x0006A204
		internal ReportSizeProperty(ExpressionInfo expressionInfo, ReportSize defaultValue)
			: base(expressionInfo != null && expressionInfo.IsExpression, (expressionInfo == null) ? defaultValue.ToString() : expressionInfo.OriginalText)
		{
			if (expressionInfo != null && !expressionInfo.IsExpression)
			{
				this.m_value = new ReportSize(expressionInfo.StringValue);
				return;
			}
			this.m_value = defaultValue;
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0006C058 File Offset: 0x0006A258
		public ReportSize Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000D65 RID: 3429
		private ReportSize m_value;
	}
}
