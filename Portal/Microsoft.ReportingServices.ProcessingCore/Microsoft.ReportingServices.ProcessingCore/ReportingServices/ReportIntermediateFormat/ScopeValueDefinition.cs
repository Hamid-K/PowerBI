using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003D2 RID: 978
	internal sealed class ScopeValueDefinition
	{
		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x000BAB72 File Offset: 0x000B8D72
		// (set) Token: 0x06002784 RID: 10116 RVA: 0x000BAB7A File Offset: 0x000B8D7A
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_expression;
			}
			set
			{
				this.m_expression = value;
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000BAB83 File Offset: 0x000B8D83
		internal void Initialize(string propertyName, InitializationContext context)
		{
			if (this.m_expression != null)
			{
				this.m_expression.Initialize(propertyName, context);
			}
		}

		// Token: 0x04001688 RID: 5768
		private ExpressionInfo m_expression;
	}
}
