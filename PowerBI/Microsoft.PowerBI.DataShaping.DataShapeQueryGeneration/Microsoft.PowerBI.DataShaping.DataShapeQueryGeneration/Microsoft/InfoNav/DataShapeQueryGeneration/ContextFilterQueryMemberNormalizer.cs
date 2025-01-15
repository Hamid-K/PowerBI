using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000057 RID: 87
	internal sealed class ContextFilterQueryMemberNormalizer : IQueryGroupValueVisitor<QueryGroupValue>
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000DF19 File Offset: 0x0000C119
		private ContextFilterQueryMemberNormalizer()
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000DF24 File Offset: 0x0000C124
		internal QueryMember Normalize(QueryMember member, QuerySortGenerator sort)
		{
			IReadOnlyList<QueryGroupKey> readOnlyList = this.Normalize(member.Group.Keys);
			IReadOnlyList<QueryGroupValue> readOnlyList2 = this.Normalize(member.Values);
			return new QueryMember(new QueryGroup(readOnlyList, sort.DetermineDefaultSortKeys(readOnlyList, readOnlyList2), null, SubtotalType.None, null, member.Group.SuppressSortByMeasureRollup, member.Group.IsSubtotalContextOnly), readOnlyList2, Util.EmptyReadOnlyCollection<ProjectedDsqExpression>(), member.ColumnProjectionExpressions, null, member.HasExplicitSubtotal, member.IsContextOnly);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000DF98 File Offset: 0x0000C198
		private IReadOnlyList<QueryGroupKey> Normalize(IReadOnlyList<QueryGroupKey> keys)
		{
			List<QueryGroupKey> list = new List<QueryGroupKey>(keys.Count);
			for (int i = 0; i < keys.Count; i++)
			{
				QueryGroupKey queryGroupKey = keys[i];
				QueryGroupKey queryGroupKey2 = queryGroupKey.Clone(this.Normalize(queryGroupKey.Expression, queryGroupKey.Field));
				list.Add(queryGroupKey2);
			}
			return list;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000DFEB File Offset: 0x0000C1EB
		private IReadOnlyList<QueryGroupValue> Normalize(IReadOnlyList<QueryGroupValue> values)
		{
			return Util.VisitReadOnlyList<QueryGroupValue>(values, (QueryGroupValue value) => value.Accept<QueryGroupValue>(this));
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000E000 File Offset: 0x0000C200
		public QueryGroupValue Visit(QueryGroupSingleValue value)
		{
			ProjectedDsqExpression projectedDsqExpression = this.Normalize(value.ProjectedExpression, value.BindingHints.Field);
			if (projectedDsqExpression == value.ProjectedExpression)
			{
				return value;
			}
			return value.CloneWithOverride(projectedDsqExpression);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000E037 File Offset: 0x0000C237
		public QueryGroupValue Visit(QueryGroupIntervalValue value)
		{
			throw new NotImplementedException("IntervalValue and ContextFilters are not supported");
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E044 File Offset: 0x0000C244
		private ProjectedDsqExpression Normalize(ProjectedDsqExpression projectedExpr, IConceptualProperty underlyingProperty)
		{
			ExpressionNode expressionNode = this.Normalize(projectedExpr.Value.DsqExpression, underlyingProperty);
			if (projectedExpr.Value.DsqExpression == expressionNode)
			{
				return projectedExpr;
			}
			ProjectedDsqExpressionValue projectedDsqExpressionValue = new ProjectedDsqExpressionValue(expressionNode, projectedExpr.Value.FormatString, underlyingProperty ?? projectedExpr.Value.LineageProperty);
			return projectedExpr.CloneWithOverrides(projectedDsqExpressionValue, null);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000E0A6 File Offset: 0x0000C2A6
		private ExpressionNode Normalize(ExpressionNode expression, IConceptualProperty underlyingProperty)
		{
			if (underlyingProperty != null)
			{
				return underlyingProperty.DsqExpression();
			}
			return expression;
		}

		// Token: 0x04000228 RID: 552
		internal static readonly ContextFilterQueryMemberNormalizer Instance = new ContextFilterQueryMemberNormalizer();
	}
}
