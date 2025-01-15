using System;
using Microsoft.DataShaping;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000111 RID: 273
	internal sealed class QueryFilterExpressionAnalyzer
	{
		// Token: 0x06000FCF RID: 4047 RVA: 0x0002BDBC File Offset: 0x00029FBC
		internal static bool TryExtractExpression<TTarget>(QueryExpression expr, out TTarget targetExpression, out bool isNegated) where TTarget : QueryExpression
		{
			targetExpression = expr as TTarget;
			if (targetExpression != null)
			{
				isNegated = false;
				return true;
			}
			QueryFunctionExpression queryFunctionExpression = expr as QueryFunctionExpression;
			if (queryFunctionExpression != null && queryFunctionExpression.Function.FullName == "Core.Not")
			{
				QueryExpression queryExpression = queryFunctionExpression.Arguments.Single("Expected only 1 argument for the Not QueryFunctionExpression", Array.Empty<string>());
				targetExpression = queryExpression as TTarget;
				if (targetExpression != null)
				{
					isNegated = true;
					return true;
				}
			}
			targetExpression = default(TTarget);
			isNegated = false;
			return false;
		}
	}
}
