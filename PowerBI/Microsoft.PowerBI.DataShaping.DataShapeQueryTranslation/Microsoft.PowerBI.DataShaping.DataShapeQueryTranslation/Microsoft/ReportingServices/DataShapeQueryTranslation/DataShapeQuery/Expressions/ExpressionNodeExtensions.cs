using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeQuery.Expressions
{
	// Token: 0x020000D1 RID: 209
	internal static class ExpressionNodeExtensions
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x000223C8 File Offset: 0x000205C8
		public static string ToString(this ExpressionNode node, ExpressionTable expressionTable)
		{
			return new ExpressionStringBuilder(expressionTable, false).Write(node);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000223D8 File Offset: 0x000205D8
		public static ConceptualPrimitiveResultType GetType(this LiteralExpressionNode node)
		{
			ConceptualPrimitiveResultType primitive = node.ClrType.GetPrimitive();
			if (primitive == null)
			{
				return null;
			}
			return primitive;
		}
	}
}
