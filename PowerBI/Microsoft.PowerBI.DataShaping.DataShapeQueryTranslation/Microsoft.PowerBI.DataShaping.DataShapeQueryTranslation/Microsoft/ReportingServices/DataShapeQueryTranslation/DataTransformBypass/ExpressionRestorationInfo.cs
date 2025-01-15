using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C4 RID: 196
	internal sealed class ExpressionRestorationInfo
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x0001FD89 File Offset: 0x0001DF89
		internal ExpressionRestorationInfo(ExpressionReference expression, ExpressionNode originalNode)
		{
			this.m_expression = expression;
			this.m_originalNode = originalNode;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001FD9F File Offset: 0x0001DF9F
		internal ExpressionReference Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001FDA7 File Offset: 0x0001DFA7
		internal ExpressionNode OriginalNode
		{
			get
			{
				return this.m_originalNode;
			}
		}

		// Token: 0x04000417 RID: 1047
		private readonly ExpressionReference m_expression;

		// Token: 0x04000418 RID: 1048
		private readonly ExpressionNode m_originalNode;
	}
}
