using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.Reporting.Common.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D8 RID: 216
	internal static class FilterExtensions
	{
		// Token: 0x06000D98 RID: 3480 RVA: 0x00022D54 File Offset: 0x00020F54
		public static bool AreEquivalent(FilterCondition left, FilterCondition right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			QueryExpression queryExpression = left.ToPredicate();
			QueryExpression queryExpression2 = right.ToPredicate();
			return queryExpression.Equals(queryExpression2);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00022D84 File Offset: 0x00020F84
		public static bool IsNullOrEmpty(this FilterCondition filterCondition)
		{
			if (filterCondition == null)
			{
				return true;
			}
			CompoundFilterCondition compoundFilterCondition = filterCondition as CompoundFilterCondition;
			return compoundFilterCondition != null && compoundFilterCondition.Conditions.Count == 0;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00022DB0 File Offset: 0x00020FB0
		internal static CompoundFilterCondition ToCompoundFilterConditionIfNonNull(this FilterCondition condition)
		{
			if (condition.IsNullOrEmpty())
			{
				return null;
			}
			CompoundFilterCondition compoundFilterCondition = condition as CompoundFilterCondition;
			if (compoundFilterCondition != null)
			{
				return compoundFilterCondition;
			}
			return new CompoundFilterCondition(new FilterCondition[] { condition });
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00022DE4 File Offset: 0x00020FE4
		internal static CompoundFilterCondition CreateFilter(this QueryExpression referenceExpression, IEnumerable<QueryExpression> valueExpressions, CompoundFilterOperator compoundFilterOperator, FilterOperator filterOperator = FilterOperator.Equal)
		{
			CompoundFilterCondition compoundFilterCondition = null;
			if (valueExpressions != null && valueExpressions.Any<QueryExpression>())
			{
				compoundFilterCondition = new CompoundFilterCondition(compoundFilterOperator, valueExpressions.Select((QueryExpression v) => new SimpleFilterCondition(referenceExpression, filterOperator, v)));
			}
			return compoundFilterCondition;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00022E2C File Offset: 0x0002102C
		internal static CompoundFilterCondition CreateFilter(this IList<QueryExpression> expressions, CompoundFilterOperator @operator, IEnumerable<CompositeValue> values)
		{
			if (values == null || !values.Any<CompositeValue>())
			{
				return null;
			}
			return new CompoundFilterCondition(@operator, values.Select((CompositeValue v) => expressions.CreateFilter(v, FilterOperator.Equal, false)));
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00022E6C File Offset: 0x0002106C
		internal static FilterCondition CreateFilter(this IList<QueryExpression> expressions, CompositeValue values, FilterOperator @operator = FilterOperator.Equal, bool not = false)
		{
			if (expressions.Count == 1)
			{
				return expressions[0].CreateFilter(values[0], @operator, not);
			}
			FilterCondition[] array = new FilterCondition[expressions.Count];
			for (int i = 0; i < expressions.Count; i++)
			{
				array[i] = expressions[i].CreateFilter(values[i], @operator, not);
			}
			return new CompoundFilterCondition(CompoundFilterOperator.All, array);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00022ED4 File Offset: 0x000210D4
		internal static FilterCondition CreateFilter(this QueryExpression expression, ScalarValue value, FilterOperator @operator = FilterOperator.Equal, bool not = false)
		{
			QueryExpression queryExpression;
			if (value == ScalarValue.Null)
			{
				queryExpression = expression.ConceptualResultType.Null();
			}
			else
			{
				queryExpression = expression.ConceptualResultType.Literal(value);
			}
			return new SimpleFilterCondition(expression, not, @operator, queryExpression);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00022F12 File Offset: 0x00021112
		internal static IEnumerable<FilterCondition> Flatten(this IEnumerable<FilterCondition> filterConditions)
		{
			foreach (FilterCondition filterCondition in filterConditions)
			{
				CompoundFilterCondition compoundFilterCondition = filterCondition as CompoundFilterCondition;
				if (compoundFilterCondition != null && (compoundFilterCondition.Conditions.Count == 0 || compoundFilterCondition.Operator == CompoundFilterOperator.All || (compoundFilterCondition.Operator == CompoundFilterOperator.Any && compoundFilterCondition.Conditions.Count == 1)))
				{
					IEnumerable<FilterCondition> enumerable = compoundFilterCondition.Conditions.Flatten();
					foreach (FilterCondition filterCondition2 in enumerable)
					{
						yield return filterCondition2;
					}
					IEnumerator<FilterCondition> enumerator2 = null;
				}
				else
				{
					yield return filterCondition;
				}
			}
			IEnumerator<FilterCondition> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00022F24 File Offset: 0x00021124
		internal static IReadOnlyList<IEdmFieldInstance> GetReferencedModelFields(this FilterCondition filterCondition)
		{
			CompoundFilterCondition compoundFilterCondition = filterCondition as CompoundFilterCondition;
			if (compoundFilterCondition != null)
			{
				IEnumerable<FilterCondition> conditions = compoundFilterCondition.Conditions;
				Func<FilterCondition, IEnumerable<IEdmFieldInstance>> func;
				if ((func = FilterExtensions.<>O.<0>__GetReferencedModelFields) == null)
				{
					func = (FilterExtensions.<>O.<0>__GetReferencedModelFields = new Func<FilterCondition, IEnumerable<IEdmFieldInstance>>(FilterExtensions.GetReferencedModelFields));
				}
				return conditions.SelectMany(func).ToList<IEdmFieldInstance>();
			}
			return (filterCondition as SimpleFilterCondition).LeftExpression.GetReferencedModelFieldsForFilter();
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00022F78 File Offset: 0x00021178
		internal static IReadOnlyList<IConceptualColumn> GetReferencedModelColumns(this FilterCondition filterCondition)
		{
			CompoundFilterCondition compoundFilterCondition = filterCondition as CompoundFilterCondition;
			if (compoundFilterCondition != null)
			{
				IEnumerable<FilterCondition> conditions = compoundFilterCondition.Conditions;
				Func<FilterCondition, IEnumerable<IConceptualColumn>> func;
				if ((func = FilterExtensions.<>O.<1>__GetReferencedModelColumns) == null)
				{
					func = (FilterExtensions.<>O.<1>__GetReferencedModelColumns = new Func<FilterCondition, IEnumerable<IConceptualColumn>>(FilterExtensions.GetReferencedModelColumns));
				}
				return conditions.SelectMany(func).ToList<IConceptualColumn>();
			}
			return (filterCondition as SimpleFilterCondition).LeftExpression.GetReferencedModelColumnsForFilter();
		}

		// Token: 0x020002F2 RID: 754
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040010C3 RID: 4291
			public static Func<FilterCondition, IEnumerable<IEdmFieldInstance>> <0>__GetReferencedModelFields;

			// Token: 0x040010C4 RID: 4292
			public static Func<FilterCondition, IEnumerable<IConceptualColumn>> <1>__GetReferencedModelColumns;
		}
	}
}
