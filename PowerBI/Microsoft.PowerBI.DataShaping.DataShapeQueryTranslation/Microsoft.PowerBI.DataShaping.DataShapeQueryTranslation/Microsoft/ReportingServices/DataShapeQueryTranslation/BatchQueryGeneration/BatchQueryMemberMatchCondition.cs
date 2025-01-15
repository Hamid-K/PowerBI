using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000144 RID: 324
	internal sealed class BatchQueryMemberMatchCondition
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x000306F3 File Offset: 0x0002E8F3
		internal BatchQueryMemberMatchCondition(ExpressionId expressionId, bool matchValue)
		{
			this.m_expressionId = expressionId;
			this.m_matchValue = matchValue;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00030709 File Offset: 0x0002E909
		public ExpressionId ExpressionId
		{
			get
			{
				return this.m_expressionId;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00030711 File Offset: 0x0002E911
		public bool MatchValue
		{
			get
			{
				return this.m_matchValue;
			}
		}

		// Token: 0x0400060C RID: 1548
		private readonly ExpressionId m_expressionId;

		// Token: 0x0400060D RID: 1549
		private readonly bool m_matchValue;
	}
}
