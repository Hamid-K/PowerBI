using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200014D RID: 333
	internal static class DataTransformConstants
	{
		// Token: 0x04000634 RID: 1588
		internal static readonly ConceptualPrimitiveResultType StringType = ConceptualPrimitiveResultType.Text;

		// Token: 0x04000635 RID: 1589
		internal static readonly QueryExpression NullStringLiteral = DataTransformConstants.StringType.Null();
	}
}
