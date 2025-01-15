using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C9 RID: 713
	public sealed class ReportStringProperty : ReportProperty
	{
		// Token: 0x06001AEC RID: 6892 RVA: 0x0006BC34 File Offset: 0x00069E34
		internal ReportStringProperty()
		{
			this.m_value = null;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0006BC43 File Offset: 0x00069E43
		internal ReportStringProperty(bool isExpression, string expressionString, string value)
			: this(isExpression, expressionString, value, null)
		{
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0006BC4F File Offset: 0x00069E4F
		internal ReportStringProperty(bool isExpression, string expressionString, string value, string defaultValue)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_value = value;
				return;
			}
			this.m_value = defaultValue;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0006BC6C File Offset: 0x00069E6C
		internal ReportStringProperty(Microsoft.ReportingServices.ReportProcessing.ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.Value;
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0006BCA4 File Offset: 0x00069EA4
		internal ReportStringProperty(Microsoft.ReportingServices.ReportProcessing.ExpressionInfo expression, string formulaText)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : ((expression.IsExpression && expression.OriginalText == null) ? formulaText : expression.OriginalText))
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.Value;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0006BCFC File Offset: 0x00069EFC
		internal ReportStringProperty(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				if (expression.ConstantType != DataType.String)
				{
					this.m_value = expression.OriginalText;
					return;
				}
				this.m_value = expression.StringValue;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x0006BD55 File Offset: 0x00069F55
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000D61 RID: 3425
		private string m_value;
	}
}
