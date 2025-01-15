using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000135 RID: 309
	internal static class DaxIdentityOperatorTranslator
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x0002FCAC File Offset: 0x0002DEAC
		internal static DaxExpression EqualsIdentity(QueryExpression left, DaxExpression leftDax, QueryExpression right, DaxExpression rightDax)
		{
			DaxExpression daxExpression = DaxOperators.Equal(leftDax, rightDax);
			if (!QueryAlgorithms.IsExemptFromBlankComparison(left, right))
			{
				daxExpression = DaxFunctions.And(daxExpression, DaxOperators.Equal(DaxFunctions.IsBlank(leftDax), DaxFunctions.IsBlank(rightDax)));
			}
			return daxExpression;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0002FCE4 File Offset: 0x0002DEE4
		internal static DaxExpression NotEqualsIdentity(QueryExpression left, DaxExpression leftDax, QueryExpression right, DaxExpression rightDax)
		{
			DaxExpression daxExpression = DaxOperators.NotEqual(leftDax, rightDax);
			if (!QueryAlgorithms.IsExemptFromBlankComparison(left, right))
			{
				daxExpression = DaxFunctions.Or(daxExpression, DaxOperators.NotEqual(DaxFunctions.IsBlank(leftDax), DaxFunctions.IsBlank(rightDax)));
			}
			return daxExpression;
		}
	}
}
