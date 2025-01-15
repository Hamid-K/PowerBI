using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000046 RID: 70
	internal static class ExpressionStringBuilderFactory
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x00007FCE File Offset: 0x000061CE
		internal static IExpressionStringBuilder Create(ExpressionTable expressionTable = null, bool outputConceptualSchemaRefs = false)
		{
			return new ExpressionStringBuilder(expressionTable, outputConceptualSchemaRefs);
		}
	}
}
