using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal
{
	// Token: 0x02000157 RID: 343
	internal static class QueryExpressionValidation
	{
		// Token: 0x06001375 RID: 4981 RVA: 0x00037B24 File Offset: 0x00035D24
		internal static void ValidateComparison(QueryComparisonKind kind, ConceptualResultType leftType, ConceptualResultType rightType)
		{
			bool flag = true;
			bool flag2 = true;
			switch (kind)
			{
			case QueryComparisonKind.Equals:
			case QueryComparisonKind.NotEquals:
			case QueryComparisonKind.EqualsIdentity:
			case QueryComparisonKind.NotEqualsIdentity:
				flag = leftType.IsEqualComparableTo(rightType);
				goto IL_0050;
			case QueryComparisonKind.GreaterThanOrEquals:
			case QueryComparisonKind.LessThanOrEquals:
				flag = leftType.IsEqualComparableTo(rightType);
				flag2 = leftType.IsOrderComparableTo(rightType);
				goto IL_0050;
			}
			flag2 = leftType.IsOrderComparableTo(rightType);
			IL_0050:
			if (!flag || !flag2)
			{
				QueryExpressionValidation.Fail("ComparisonExpression requires arguments with comparable types", Array.Empty<object>());
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00037B96 File Offset: 0x00035D96
		internal static void ValidateIsNullExpressionArgument(ConceptualResultType type)
		{
			if (type is ConceptualPrimitiveResultType || type is ConceptualTableType)
			{
				return;
			}
			QueryExpressionValidation.Fail("The argument to IsNullExpression must refer to a primitive or table", Array.Empty<object>());
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00037BB8 File Offset: 0x00035DB8
		internal static void ValidateSortClause(ConceptualResultType type)
		{
			if (!type.IsOrderComparable())
			{
				QueryExpressionValidation.Fail("SortClause expressions must have a type that is order comparable", Array.Empty<object>());
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00037BD4 File Offset: 0x00035DD4
		internal static void ValidateFunction(EdmFunction function, IReadOnlyList<ConceptualResultType> argumentTypes)
		{
			if (function.ConceptualReturnType.IsNullType())
			{
				QueryExpressionValidation.Fail("Function metadata used in FunctionExpression cannot have a void return type.", Array.Empty<object>());
			}
			if (function.Parameters.Count != argumentTypes.Count)
			{
				QueryExpressionValidation.Fail("Incorrect number of arguments passed to function {0}. Expected {1} but got {2}.", new object[]
				{
					function.Name,
					function.Parameters.Count,
					argumentTypes.Count
				});
			}
			for (int i = 0; i < argumentTypes.Count; i++)
			{
				ConceptualResultType type = function.Parameters[i].Type;
				if (!argumentTypes[i].IsPromotableTo(type))
				{
					QueryExpressionValidation.Fail("The ResultType of the specified argument is not compatible with the required type. The argument ResultType is '{0}' but the required type is '{1}'.", new object[]
					{
						argumentTypes[i],
						type
					});
				}
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00037C9C File Offset: 0x00035E9C
		internal static void ValidateOperator(EdmOperator @operator, IReadOnlyList<ConceptualResultType> argumentTypes)
		{
			foreach (ConceptualResultType conceptualResultType in argumentTypes)
			{
				if (conceptualResultType != @operator.ParameterType)
				{
					QueryExpressionValidation.Fail("The ResultType of the specified argument is not compatible with the required type. The argument ResultType is '{0}' but the required type is '{1}'.", new object[] { conceptualResultType, @operator.ParameterType });
				}
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00037D04 File Offset: 0x00035F04
		internal static void ValidateGroupBy(IReadOnlyList<ConceptualTypeColumn> keys, IReadOnlyList<ConceptualTypeColumn> aggregates)
		{
			if (keys.Count == 0 && aggregates.Count == 0)
			{
				QueryExpressionValidation.Fail("At least one group key or aggregate is required.", Array.Empty<object>());
			}
			using (IEnumerator<ConceptualTypeColumn> enumerator = aggregates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ConceptualTypeColumn aggregate = enumerator.Current;
					if (keys.FirstOrDefault((ConceptualTypeColumn c) => object.Equals(c.Name, aggregate.Name)) != null)
					{
						QueryExpressionValidation.Fail("An aggregate named '{0}' cannot be used because the specified group keys include a key with the same name.", new object[] { aggregate.Name });
					}
				}
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00037DA4 File Offset: 0x00035FA4
		private static void Fail(string message, params object[] arguments)
		{
			throw new ArgumentException(StringUtil.FormatInvariant(message, arguments));
		}
	}
}
