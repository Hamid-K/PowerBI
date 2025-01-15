using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedSemanticQuery
{
	// Token: 0x0200009A RID: 154
	[ImmutableObject(true)]
	public sealed class ResolvedQueryExpressionSerializer : ResolvedQueryExpressionVisitor<QueryExpression>
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000A9BE File Offset: 0x00008BBE
		private ResolvedQueryExpressionSerializer()
		{
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000A9C6 File Offset: 0x00008BC6
		public override QueryExpression Visit(ResolvedQuerySourceRefExpression expression)
		{
			if (expression.SourceName == null)
			{
				return expression.SourceEntity.SourceRef();
			}
			return expression.SourceName.SourceRef();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000A9E7 File Offset: 0x00008BE7
		public override QueryExpression Visit(ResolvedQueryExpressionSourceRefExpression expression)
		{
			return expression.SourceName.SourceRef();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000A9F4 File Offset: 0x00008BF4
		public override QueryExpression Visit(ResolvedQuerySubqueryExpression expression)
		{
			QueryDefinition queryDefinition;
			if (ResolvedQueryDefinitionSerializer.TrySerializeResolvedQuery(expression.Subquery, out queryDefinition))
			{
				return queryDefinition.Subquery();
			}
			throw this.FailQuerySerialization("Unexpected failure when serializing ResolvedQueryDefinition");
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000AA22 File Offset: 0x00008C22
		public override QueryExpression Visit(ResolvedQueryColumnExpression expression)
		{
			return this.VisitExpression(expression.Expression).Column(expression.Column);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000AA3B File Offset: 0x00008C3B
		public override QueryExpression Visit(ResolvedQueryMeasureExpression expression)
		{
			return this.VisitExpression(expression.Expression).Measure(expression.Measure);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000AA54 File Offset: 0x00008C54
		public override QueryExpression Visit(ResolvedQueryHierarchyExpression expression)
		{
			return this.VisitExpression(expression.Expression).Hierarchy(expression.Hierarchy);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000AA6D File Offset: 0x00008C6D
		public override QueryExpression Visit(ResolvedQueryHierarchyLevelExpression expression)
		{
			return this.VisitExpression(expression.HierarchyExpression).HierarchyLevel(expression.Level);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000AA86 File Offset: 0x00008C86
		public override QueryExpression Visit(ResolvedQueryPropertyVariationSourceExpression expression)
		{
			return this.VisitExpression(expression.SourceRefExpression).VariationSource(expression.VariationSource, expression.Property);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000AAA5 File Offset: 0x00008CA5
		public override QueryExpression Visit(ResolvedQueryColumnReferenceExpression expression)
		{
			return this.VisitExpression(expression.Source).Column(expression.SelectName);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000AABE File Offset: 0x00008CBE
		public override QueryExpression Visit(ResolvedQueryNotExpression expression)
		{
			return this.VisitExpression(expression.Expression).Not();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000AAD1 File Offset: 0x00008CD1
		public override QueryExpression Visit(ResolvedQueryAndExpression expression)
		{
			return this.VisitExpression(expression.Left).And(this.VisitExpression(expression.Right));
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		public override QueryExpression Visit(ResolvedQueryOrExpression expression)
		{
			return this.VisitExpression(expression.Left).Or(this.VisitExpression(expression.Right));
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000AB0F File Offset: 0x00008D0F
		public override QueryExpression Visit(ResolvedQueryAggregationExpression expression)
		{
			return this.VisitExpression(expression.Expression).Aggregate(expression.Function);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000AB28 File Offset: 0x00008D28
		public override QueryExpression Visit(ResolvedQueryArithmeticExpression expression)
		{
			return this.VisitExpression(expression.Left).Arithmetic(this.VisitExpression(expression.Right), expression.Operator);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000AB4D File Offset: 0x00008D4D
		public override QueryExpression Visit(ResolvedQueryBetweenExpression expression)
		{
			return this.VisitExpression(expression.Expression).Between(this.VisitExpression(expression.LowerBound), this.VisitExpression(expression.UpperBound));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000AB78 File Offset: 0x00008D78
		public override QueryExpression Visit(ResolvedQueryInExpression expression)
		{
			if (expression.HasValues)
			{
				List<List<QueryExpressionContainer>> list = new List<List<QueryExpressionContainer>>(expression.Values.Count);
				foreach (IReadOnlyList<ResolvedQueryExpression> readOnlyList in expression.Values)
				{
					list.Add(this.SerializeExpressionList(readOnlyList.ToList<ResolvedQueryExpression>()));
				}
				return this.SerializeExpressionList(expression.Expressions.ToList<ResolvedQueryExpression>()).In(list, expression.EqualityKind);
			}
			return this.SerializeExpressionList(expression.Expressions.ToList<ResolvedQueryExpression>()).In(this.VisitExpression(expression.Table));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public override QueryExpression Visit(ResolvedQueryScopedEvalExpression expression)
		{
			return this.VisitExpression(expression.Expression).ScopedEval(this.SerializeExpressionList(expression.Scope));
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000AC4B File Offset: 0x00008E4B
		public override QueryExpression Visit(ResolvedQueryFilteredEvalExpression expression)
		{
			return this.VisitExpression(expression.Expression).FilteredEval(this.SerializeQueryFilterList(expression.Filters));
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000AC6A File Offset: 0x00008E6A
		public override QueryExpression Visit(ResolvedQueryComparisonExpression expression)
		{
			return this.VisitExpression(expression.Left).Comparison(this.VisitExpression(expression.Right), expression.ComparisonKind);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000AC8F File Offset: 0x00008E8F
		public override QueryExpression Visit(ResolvedQueryContainsExpression expression)
		{
			return this.VisitExpression(expression.Left).Contains(this.VisitExpression(expression.Right));
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000ACAE File Offset: 0x00008EAE
		public override QueryExpression Visit(ResolvedQueryDateAddExpression expression)
		{
			return this.VisitExpression(expression.Expression).DateAdd(expression.Amount, expression.TimeUnit);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000ACCD File Offset: 0x00008ECD
		public override QueryExpression Visit(ResolvedQueryPercentileExpression expression)
		{
			return this.VisitExpression(expression.Expression).Percentile(expression.Exclusive, expression.K);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000ACEC File Offset: 0x00008EEC
		public override QueryExpression Visit(ResolvedQueryMinExpression expression)
		{
			return this.VisitExpression(expression.Expression).Min(expression.IncludeAllTypes);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000AD05 File Offset: 0x00008F05
		public override QueryExpression Visit(ResolvedQueryMaxExpression expression)
		{
			return this.VisitExpression(expression.Expression).Max(expression.IncludeAllTypes);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000AD1E File Offset: 0x00008F1E
		public override QueryExpression Visit(ResolvedQueryDateSpanExpression expression)
		{
			return this.VisitExpression(expression.Expression).DateSpan(expression.TimeUnit);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000AD37 File Offset: 0x00008F37
		public override QueryExpression Visit(ResolvedQueryExistsExpression expression)
		{
			return this.VisitExpression(expression.Expression).Exists();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000AD4A File Offset: 0x00008F4A
		public override QueryExpression Visit(ResolvedQueryFloorExpression expression)
		{
			return this.VisitExpression(expression.Expression).Floor(expression.Size, expression.TimeUnit);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000AD69 File Offset: 0x00008F69
		public override QueryExpression Visit(ResolvedQueryDiscretizeExpression expression)
		{
			return this.VisitExpression(expression.Expression).Discretize(expression.Count);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000AD84 File Offset: 0x00008F84
		public override QueryExpression Visit(ResolvedQuerySparklineDataExpression expression)
		{
			if (!(expression.ScalarKey == null))
			{
				return this.VisitExpression(expression.Measure).SparklineData(this.SerializeExpressionList(expression.Groupings), expression.PointsPerSparkline, expression.IncludeMinGroupingInterval, this.VisitExpression(expression.ScalarKey));
			}
			return this.VisitExpression(expression.Measure).SparklineData(this.SerializeExpressionList(expression.Groupings), expression.PointsPerSparkline, expression.IncludeMinGroupingInterval, null);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000ADFF File Offset: 0x00008FFF
		public override QueryExpression Visit(ResolvedQueryMemberExpression expression)
		{
			return this.VisitExpression(expression.Expression).Member(expression.Member);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000AE18 File Offset: 0x00009018
		public override QueryExpression Visit(ResolvedQueryLiteralExpression expression)
		{
			return expression.Value.Literal();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000AE25 File Offset: 0x00009025
		public override QueryExpression Visit(ResolvedQueryNowExpression expression)
		{
			return SemanticQueryExpressionBuilder.Now();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000AE2C File Offset: 0x0000902C
		public override QueryExpression Visit(ResolvedQueryStartsWithExpression expression)
		{
			return this.VisitExpression(expression.Left).StartsWith(this.VisitExpression(expression.Right));
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000AE4B File Offset: 0x0000904B
		public override QueryExpression Visit(ResolvedQueryEndsWithExpression expression)
		{
			return this.VisitExpression(expression.Left).EndsWith(this.VisitExpression(expression.Right));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000AE6A File Offset: 0x0000906A
		public override QueryExpression Visit(ResolvedQueryDefaultValueExpression expression)
		{
			return SemanticQueryExpressionBuilder.DefaultValue();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000AE71 File Offset: 0x00009071
		public override QueryExpression Visit(ResolvedQueryAnyValueExpression expression)
		{
			return SemanticQueryExpressionBuilder.AnyValue(expression.DefaultValueOverridesAncestors);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000AE7E File Offset: 0x0000907E
		public override QueryExpression Visit(ResolvedQueryTransformOutputRoleRefExpression expression)
		{
			return expression.Role.TransformOutputRoleRef();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000AE8B File Offset: 0x0000908B
		public override QueryExpression Visit(ResolvedQueryTransformTableColumnExpression expression)
		{
			return expression.Table.Name.TransformTableRef().Column(expression.Column.Name);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000AEAD File Offset: 0x000090AD
		public override QueryExpression Visit(ResolvedQueryNativeFormatExpression expression)
		{
			return this.VisitExpression(expression.Expression).NativeFormat(expression.FormatString);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000AEC6 File Offset: 0x000090C6
		public override QueryExpression Visit(ResolvedQueryNativeMeasureExpression expression)
		{
			return SemanticQueryExpressionBuilder.NativeMeasure(expression.Language, expression.Expression);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000AED9 File Offset: 0x000090D9
		public override QueryExpression Visit(ResolvedQueryLetRefExpression expression)
		{
			return expression.Binding.Name.LetRef();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000AEEB File Offset: 0x000090EB
		public override QueryExpression Visit(ResolvedQueryRoleRefExpression expression)
		{
			return expression.Role.RoleRef();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000AEF8 File Offset: 0x000090F8
		public override QueryExpression Visit(ResolvedSummaryValueRefExpression expression)
		{
			return expression.Name.SummaryValueRef();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000AF05 File Offset: 0x00009105
		public override QueryExpression Visit(ResolvedQueryParameterRefExpression expression)
		{
			return expression.Declaration.Name.ParameterRef();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000AF17 File Offset: 0x00009117
		public override QueryExpression Visit(ResolvedQueryPrimitiveTypeExpression expression)
		{
			return expression.Type.PrimitiveType();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000AF24 File Offset: 0x00009124
		public override QueryExpression Visit(ResolvedQueryTableTypeExpression expression)
		{
			return Util.RemapReadOnly<ResolvedQueryTableTypeColumn, QueryExpressionContainer>(expression.Columns, new Func<ResolvedQueryTableTypeColumn, QueryExpressionContainer>(this.Visit)).TableType();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000AF42 File Offset: 0x00009142
		private QueryExpressionContainer Visit(ResolvedQueryTableTypeColumn column)
		{
			return this.VisitExpression(column.Expression).Container(column.Name);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000AF5B File Offset: 0x0000915B
		public override QueryExpression Visit(ResolvedQueryTypeOfExpression expression)
		{
			return this.VisitExpression(expression.Expression).TypeOf();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000AF6E File Offset: 0x0000916E
		public override QueryExpression Visit(ResolvedQueryNativeVisualCalculationExpression expression)
		{
			return SemanticQueryExpressionBuilder.NativeVisualCalculation(expression.Language, expression.Expression);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000AF81 File Offset: 0x00009181
		private QueryExpression VisitExpression(ResolvedQueryExpression expression)
		{
			return expression.Accept<QueryExpression>(this);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000AF8C File Offset: 0x0000918C
		private List<QueryFilter> SerializeQueryFilterList(IReadOnlyList<ResolvedQueryFilter> resolvedQueryFilters)
		{
			List<QueryFilter> list = new List<QueryFilter>(resolvedQueryFilters.Count);
			foreach (ResolvedQueryFilter resolvedQueryFilter in resolvedQueryFilters)
			{
				QueryFilter queryFilter = ResolvedQueryDefinitionSerializer.SerializeQueryFilter(resolvedQueryFilter);
				list.Add(queryFilter);
			}
			return list;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000AFE8 File Offset: 0x000091E8
		private List<QueryExpressionContainer> SerializeExpressionList(List<ResolvedQueryExpression> expressions)
		{
			return this.SerializeExpressionList(expressions.Count, expressions);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000AFF7 File Offset: 0x000091F7
		private List<QueryExpressionContainer> SerializeExpressionList(IReadOnlyList<ResolvedQueryExpression> expressions)
		{
			return this.SerializeExpressionList(expressions.Count, expressions);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000B008 File Offset: 0x00009208
		private List<QueryExpressionContainer> SerializeExpressionList(int count, IEnumerable<ResolvedQueryExpression> expressions)
		{
			List<QueryExpressionContainer> list = new List<QueryExpressionContainer>(count);
			foreach (ResolvedQueryExpression resolvedQueryExpression in expressions)
			{
				list.Add(this.VisitExpression(resolvedQueryExpression));
			}
			return list;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000B064 File Offset: 0x00009264
		private bool TryGetSourceEntity(ResolvedQueryExpression expression, out IConceptualEntity entity)
		{
			ResolvedQuerySourceRefExpression resolvedQuerySourceRefExpression = expression as ResolvedQuerySourceRefExpression;
			if (resolvedQuerySourceRefExpression != null)
			{
				entity = resolvedQuerySourceRefExpression.SourceEntity;
				return true;
			}
			ResolvedQueryPropertyVariationSourceExpression resolvedQueryPropertyVariationSourceExpression = expression as ResolvedQueryPropertyVariationSourceExpression;
			if (resolvedQueryPropertyVariationSourceExpression != null)
			{
				entity = resolvedQueryPropertyVariationSourceExpression.SourceEntity;
				return true;
			}
			entity = null;
			return false;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000B0A9 File Offset: 0x000092A9
		private ResolvedQuerySerializerException FailQuerySerialization(string message)
		{
			throw new ResolvedQuerySerializerException(message);
		}

		// Token: 0x040001D6 RID: 470
		public static readonly ResolvedQueryExpressionSerializer Instance = new ResolvedQueryExpressionSerializer();
	}
}
