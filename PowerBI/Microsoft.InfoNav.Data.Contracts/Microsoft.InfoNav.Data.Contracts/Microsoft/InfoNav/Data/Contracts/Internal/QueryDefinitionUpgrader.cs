using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002F1 RID: 753
	internal class QueryDefinitionUpgrader
	{
		// Token: 0x06001914 RID: 6420 RVA: 0x0002D06B File Offset: 0x0002B26B
		private QueryDefinitionUpgrader(IErrorContext errorContext, IFederatedConceptualSchema federatedSchema = null)
		{
			this.ErrorContext = errorContext;
			this.FederatedSchema = federatedSchema;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0002D081 File Offset: 0x0002B281
		internal static bool TryUpgrade(IErrorContext errorContext, QueryDefinition query, IConceptualSchema schema, int? targetVersion = null)
		{
			return QueryDefinitionUpgrader.TryUpgrade(errorContext, query, schema.ToFederatedSchema(), targetVersion);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0002D091 File Offset: 0x0002B291
		internal static bool TryUpgrade(IErrorContext errorContext, QueryDefinition query, IFederatedConceptualSchema federatedSchema, int? targetVersion = null)
		{
			if (QueryDefinitionUpgrader.ShouldUpgrade(query.Version, 1, targetVersion) && !new QueryDefinitionUpgrader.V0ToV1(errorContext, federatedSchema).TryUpgrade(query))
			{
				return false;
			}
			if (QueryDefinitionUpgrader.ShouldUpgrade(query.Version, 2, targetVersion))
			{
				new QueryDefinitionUpgrader.V1ToV2(errorContext).Upgrade(query);
			}
			return true;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0002D0CF File Offset: 0x0002B2CF
		internal static bool TryUpgrade(IErrorContext errorContext, FilterDefinition filterDef, IFederatedConceptualSchema federatedSchema, int? targetVersion = null)
		{
			if (QueryDefinitionUpgrader.ShouldUpgrade(filterDef.Version, 1, targetVersion) && !new QueryDefinitionUpgrader.V0ToV1(errorContext, federatedSchema).TryUpgrade(filterDef))
			{
				return false;
			}
			if (QueryDefinitionUpgrader.ShouldUpgrade(filterDef.Version, 2, targetVersion))
			{
				new QueryDefinitionUpgrader.V1ToV2(errorContext).Upgrade(filterDef);
			}
			return true;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0002D110 File Offset: 0x0002B310
		private static bool ShouldUpgrade(int? itemVersionOrDefault, int stepTargetVersion, int? requestedTargetVersion)
		{
			if (itemVersionOrDefault.GetValueOrDefault() >= stepTargetVersion)
			{
				return false;
			}
			if (requestedTargetVersion != null)
			{
				int? num = requestedTargetVersion;
				return (num.GetValueOrDefault() >= stepTargetVersion) & (num != null);
			}
			return true;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0002D150 File Offset: 0x0002B350
		private static void Rewrite(QueryDefinition query, QueryDefinitionUpgrader.QueryExpressionRewriter rewriter)
		{
			QueryDefinitionUpgrader.Rewrite(query.Where, rewriter);
			if (!query.OrderBy.IsNullOrEmpty<QuerySortClause>())
			{
				foreach (QuerySortClause querySortClause in query.OrderBy)
				{
					QueryDefinitionUpgrader.Rewrite(querySortClause.Expression, rewriter);
				}
			}
			QueryDefinitionUpgrader.Rewrite(query.Select, rewriter);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		private static void Rewrite(FilterDefinition filterDef, QueryDefinitionUpgrader.QueryExpressionRewriter rewriter)
		{
			QueryDefinitionUpgrader.Rewrite(filterDef.Where, rewriter);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0002D1DC File Offset: 0x0002B3DC
		private static void Rewrite(List<QueryFilter> filters, QueryDefinitionUpgrader.QueryExpressionRewriter rewriter)
		{
			if (!filters.IsNullOrEmpty<QueryFilter>())
			{
				foreach (QueryFilter queryFilter in filters)
				{
					QueryDefinitionUpgrader.Rewrite(queryFilter.Target, rewriter);
					QueryDefinitionUpgrader.Rewrite(queryFilter.Condition, rewriter);
				}
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0002D244 File Offset: 0x0002B444
		private static void Rewrite(List<QueryExpressionContainer> expressions, QueryDefinitionUpgrader.QueryExpressionRewriter rewriter)
		{
			if (!expressions.IsNullOrEmpty<QueryExpressionContainer>())
			{
				for (int i = 0; i < expressions.Count; i++)
				{
					QueryDefinitionUpgrader.Rewrite(expressions[i], rewriter);
				}
			}
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0002D277 File Offset: 0x0002B477
		private static void Rewrite(QueryExpressionContainer container, QueryDefinitionUpgrader.QueryExpressionRewriter rewriter)
		{
			if (container != null)
			{
				container.ReplaceExpression(container.Expression.Accept<QueryExpression>(rewriter));
			}
		}

		// Token: 0x0400090F RID: 2319
		protected readonly IErrorContext ErrorContext;

		// Token: 0x04000910 RID: 2320
		protected readonly IFederatedConceptualSchema FederatedSchema;

		// Token: 0x0200035F RID: 863
		private class QueryExpressionRewriter : QueryExpressionVisitor<QueryExpression>
		{
			// Token: 0x06001A66 RID: 6758 RVA: 0x0002F5C7 File Offset: 0x0002D7C7
			protected internal override QueryExpression Visit(QuerySourceRefExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A67 RID: 6759 RVA: 0x0002F5CA File Offset: 0x0002D7CA
			protected internal override QueryExpression Visit(QueryPropertyExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A68 RID: 6760 RVA: 0x0002F5D9 File Offset: 0x0002D7D9
			protected internal override QueryExpression Visit(QueryColumnExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A69 RID: 6761 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
			protected internal override QueryExpression Visit(QueryMeasureExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A6A RID: 6762 RVA: 0x0002F5F7 File Offset: 0x0002D7F7
			protected internal override QueryExpression Visit(QueryHierarchyExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A6B RID: 6763 RVA: 0x0002F606 File Offset: 0x0002D806
			protected internal override QueryExpression Visit(QueryHierarchyLevelExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A6C RID: 6764 RVA: 0x0002F615 File Offset: 0x0002D815
			protected internal override QueryExpression Visit(QueryPropertyVariationSourceExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x0002F624 File Offset: 0x0002D824
			protected internal override QueryExpression Visit(QueryAggregationExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A6E RID: 6766 RVA: 0x0002F633 File Offset: 0x0002D833
			protected internal override QueryExpression Visit(QueryDatePartExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A6F RID: 6767 RVA: 0x0002F642 File Offset: 0x0002D842
			protected internal override QueryExpression Visit(QueryExistsExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A70 RID: 6768 RVA: 0x0002F651 File Offset: 0x0002D851
			protected internal override QueryExpression Visit(QueryFloorExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A71 RID: 6769 RVA: 0x0002F660 File Offset: 0x0002D860
			protected internal override QueryExpression Visit(QueryDiscretizeExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A72 RID: 6770 RVA: 0x0002F66F File Offset: 0x0002D86F
			protected internal override QueryExpression Visit(QuerySparklineDataExpression expression)
			{
				this.Rewrite(expression.Measure);
				this.RewriteList(expression.Groupings);
				this.Rewrite(expression.ScalarKey);
				return expression;
			}

			// Token: 0x06001A73 RID: 6771 RVA: 0x0002F696 File Offset: 0x0002D896
			protected internal override QueryExpression Visit(QueryMemberExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A74 RID: 6772 RVA: 0x0002F6A5 File Offset: 0x0002D8A5
			protected internal override QueryExpression Visit(QueryNativeFormatExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A75 RID: 6773 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
			protected internal override QueryExpression Visit(QueryNativeMeasureExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A76 RID: 6774 RVA: 0x0002F6B7 File Offset: 0x0002D8B7
			protected internal override QueryExpression Visit(QueryPercentileExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A77 RID: 6775 RVA: 0x0002F6C6 File Offset: 0x0002D8C6
			protected internal override QueryExpression Visit(QueryMinExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A78 RID: 6776 RVA: 0x0002F6D5 File Offset: 0x0002D8D5
			protected internal override QueryExpression Visit(QueryMaxExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A79 RID: 6777 RVA: 0x0002F6E4 File Offset: 0x0002D8E4
			protected internal override QueryExpression Visit(QueryNotExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A7A RID: 6778 RVA: 0x0002F6F3 File Offset: 0x0002D8F3
			protected internal override QueryExpression Visit(QueryAndExpression expression)
			{
				this.Rewrite(expression.Left);
				this.Rewrite(expression.Right);
				return expression;
			}

			// Token: 0x06001A7B RID: 6779 RVA: 0x0002F70E File Offset: 0x0002D90E
			protected internal override QueryExpression Visit(QueryOrExpression expression)
			{
				this.Rewrite(expression.Left);
				this.Rewrite(expression.Right);
				return expression;
			}

			// Token: 0x06001A7C RID: 6780 RVA: 0x0002F729 File Offset: 0x0002D929
			protected internal override QueryExpression Visit(QueryComparisonExpression expression)
			{
				this.Rewrite(expression.Left);
				this.Rewrite(expression.Right);
				return expression;
			}

			// Token: 0x06001A7D RID: 6781 RVA: 0x0002F744 File Offset: 0x0002D944
			protected internal override QueryExpression Visit(QueryContainsExpression expression)
			{
				this.Rewrite(expression.Left);
				this.Rewrite(expression.Right);
				return expression;
			}

			// Token: 0x06001A7E RID: 6782 RVA: 0x0002F75F File Offset: 0x0002D95F
			protected internal override QueryExpression Visit(QueryStartsWithExpression expression)
			{
				this.Rewrite(expression.Left);
				this.Rewrite(expression.Right);
				return expression;
			}

			// Token: 0x06001A7F RID: 6783 RVA: 0x0002F77A File Offset: 0x0002D97A
			protected internal override QueryExpression Visit(QueryEndsWithExpression expression)
			{
				this.Rewrite(expression.Left);
				this.Rewrite(expression.Right);
				return expression;
			}

			// Token: 0x06001A80 RID: 6784 RVA: 0x0002F795 File Offset: 0x0002D995
			protected internal override QueryExpression Visit(QueryArithmeticExpression expression)
			{
				this.Rewrite(expression.Left);
				this.Rewrite(expression.Right);
				return expression;
			}

			// Token: 0x06001A81 RID: 6785 RVA: 0x0002F7B0 File Offset: 0x0002D9B0
			protected internal override QueryExpression Visit(QueryBetweenExpression expression)
			{
				this.Rewrite(expression.Expression);
				this.Rewrite(expression.LowerBound);
				this.Rewrite(expression.UpperBound);
				return expression;
			}

			// Token: 0x06001A82 RID: 6786 RVA: 0x0002F7D8 File Offset: 0x0002D9D8
			protected internal override QueryExpression Visit(QueryInExpression expression)
			{
				this.RewriteList(expression.Expressions);
				if (expression.HasValues)
				{
					for (int i = 0; i < expression.Values.Count; i++)
					{
						this.RewriteList(expression.Values[i]);
					}
				}
				else
				{
					this.Rewrite(expression.Table);
				}
				return expression;
			}

			// Token: 0x06001A83 RID: 6787 RVA: 0x0002F830 File Offset: 0x0002DA30
			protected internal override QueryExpression Visit(QueryScopedEvalExpression expression)
			{
				this.Rewrite(expression.Expression);
				this.RewriteList(expression.Scope);
				return expression;
			}

			// Token: 0x06001A84 RID: 6788 RVA: 0x0002F84B File Offset: 0x0002DA4B
			protected internal override QueryExpression Visit(QueryFilteredEvalExpression expression)
			{
				this.Rewrite(expression.Expression);
				this.RewriteFilterList(expression.Filters);
				return expression;
			}

			// Token: 0x06001A85 RID: 6789 RVA: 0x0002F866 File Offset: 0x0002DA66
			protected internal override QueryExpression Visit(QueryLiteralExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A86 RID: 6790 RVA: 0x0002F869 File Offset: 0x0002DA69
			protected internal override QueryExpression Visit(QueryBooleanConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A87 RID: 6791 RVA: 0x0002F86C File Offset: 0x0002DA6C
			protected internal override QueryExpression Visit(QueryDateConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A88 RID: 6792 RVA: 0x0002F86F File Offset: 0x0002DA6F
			protected internal override QueryExpression Visit(QueryDateTimeConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A89 RID: 6793 RVA: 0x0002F872 File Offset: 0x0002DA72
			protected internal override QueryExpression Visit(QueryDateTimeSecondConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A8A RID: 6794 RVA: 0x0002F875 File Offset: 0x0002DA75
			protected internal override QueryExpression Visit(QueryDecadeConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A8B RID: 6795 RVA: 0x0002F878 File Offset: 0x0002DA78
			protected internal override QueryExpression Visit(QueryDecimalConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A8C RID: 6796 RVA: 0x0002F87B File Offset: 0x0002DA7B
			protected internal override QueryExpression Visit(QueryIntegerConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A8D RID: 6797 RVA: 0x0002F87E File Offset: 0x0002DA7E
			protected internal override QueryExpression Visit(QueryNullConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A8E RID: 6798 RVA: 0x0002F881 File Offset: 0x0002DA81
			protected internal override QueryExpression Visit(QueryStringConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A8F RID: 6799 RVA: 0x0002F884 File Offset: 0x0002DA84
			protected internal override QueryExpression Visit(QueryNumberConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A90 RID: 6800 RVA: 0x0002F887 File Offset: 0x0002DA87
			protected internal override QueryExpression Visit(QueryYearAndMonthConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A91 RID: 6801 RVA: 0x0002F88A File Offset: 0x0002DA8A
			protected internal override QueryExpression Visit(QueryYearAndWeekConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A92 RID: 6802 RVA: 0x0002F88D File Offset: 0x0002DA8D
			protected internal override QueryExpression Visit(QueryYearConstantExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A93 RID: 6803 RVA: 0x0002F890 File Offset: 0x0002DA90
			protected internal override QueryExpression Visit(QueryNowExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A94 RID: 6804 RVA: 0x0002F893 File Offset: 0x0002DA93
			protected internal override QueryExpression Visit(QueryDateAddExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A95 RID: 6805 RVA: 0x0002F8A2 File Offset: 0x0002DAA2
			protected internal override QueryExpression Visit(QueryDateSpanExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001A96 RID: 6806 RVA: 0x0002F8B1 File Offset: 0x0002DAB1
			protected internal override QueryExpression Visit(QueryDefaultValueExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A97 RID: 6807 RVA: 0x0002F8B4 File Offset: 0x0002DAB4
			protected internal override QueryExpression Visit(QueryAnyValueExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A98 RID: 6808 RVA: 0x0002F8B7 File Offset: 0x0002DAB7
			protected internal override QueryExpression Visit(QuerySubqueryExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A99 RID: 6809 RVA: 0x0002F8BA File Offset: 0x0002DABA
			protected internal override QueryExpression Visit(QueryTransformOutputRoleRefExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A9A RID: 6810 RVA: 0x0002F8BD File Offset: 0x0002DABD
			protected internal override QueryExpression Visit(QueryTransformTableRefExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A9B RID: 6811 RVA: 0x0002F8C0 File Offset: 0x0002DAC0
			protected internal override QueryExpression Visit(QueryLetRefExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A9C RID: 6812 RVA: 0x0002F8C3 File Offset: 0x0002DAC3
			protected internal override QueryExpression Visit(QueryRoleRefExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A9D RID: 6813 RVA: 0x0002F8C6 File Offset: 0x0002DAC6
			protected internal override QueryExpression Visit(QuerySummaryValueRefExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A9E RID: 6814 RVA: 0x0002F8C9 File Offset: 0x0002DAC9
			protected internal override QueryExpression Visit(QueryParameterRefExpression expression)
			{
				return expression;
			}

			// Token: 0x06001A9F RID: 6815 RVA: 0x0002F8CC File Offset: 0x0002DACC
			protected internal override QueryExpression Visit(QueryPrimitiveTypeExpression expression)
			{
				return expression;
			}

			// Token: 0x06001AA0 RID: 6816 RVA: 0x0002F8CF File Offset: 0x0002DACF
			protected internal override QueryExpression Visit(QueryTypeOfExpression expression)
			{
				this.Rewrite(expression.Expression);
				return expression;
			}

			// Token: 0x06001AA1 RID: 6817 RVA: 0x0002F8DE File Offset: 0x0002DADE
			protected internal override QueryExpression Visit(QueryTableTypeExpression expression)
			{
				if (expression.Columns != null)
				{
					this.RewriteList(expression.Columns);
				}
				return expression;
			}

			// Token: 0x06001AA2 RID: 6818 RVA: 0x0002F8F5 File Offset: 0x0002DAF5
			protected internal override QueryExpression Visit(QueryNativeVisualCalculationExpression expression)
			{
				return expression;
			}

			// Token: 0x06001AA3 RID: 6819 RVA: 0x0002F8F8 File Offset: 0x0002DAF8
			private void Rewrite(QueryExpressionContainer container)
			{
				if (container == null || container.Expression == null)
				{
					return;
				}
				QueryExpression queryExpression = container.Expression.Accept<QueryExpression>(this);
				if (queryExpression != container.Expression)
				{
					container.ReplaceExpression(queryExpression);
				}
			}

			// Token: 0x06001AA4 RID: 6820 RVA: 0x0002F93C File Offset: 0x0002DB3C
			private void RewriteList(List<QueryExpressionContainer> expressions)
			{
				for (int i = 0; i < expressions.Count; i++)
				{
					this.Rewrite(expressions[i]);
				}
			}

			// Token: 0x06001AA5 RID: 6821 RVA: 0x0002F968 File Offset: 0x0002DB68
			private void RewriteFilterList(List<QueryFilter> filters)
			{
				for (int i = 0; i < filters.Count; i++)
				{
					this.RewriteFilter(filters[i]);
				}
			}

			// Token: 0x06001AA6 RID: 6822 RVA: 0x0002F993 File Offset: 0x0002DB93
			private void RewriteFilter(QueryFilter filter)
			{
				if (filter.Target != null)
				{
					this.RewriteList(filter.Target);
				}
				this.Rewrite(filter.Condition);
			}
		}

		// Token: 0x02000360 RID: 864
		private sealed class V0ToV1 : QueryDefinitionUpgrader
		{
			// Token: 0x06001AA8 RID: 6824 RVA: 0x0002F9BD File Offset: 0x0002DBBD
			internal V0ToV1(IErrorContext errorContext, IFederatedConceptualSchema federatedSchema)
				: base(errorContext, federatedSchema)
			{
			}

			// Token: 0x06001AA9 RID: 6825 RVA: 0x0002F9C8 File Offset: 0x0002DBC8
			internal bool TryUpgrade(QueryDefinition query)
			{
				Dictionary<string, QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding> dictionary;
				if (!this.TryUpgradeEntitySources(query.From, out dictionary))
				{
					return false;
				}
				if (!this.TryUpgradePropertyRefs<QueryDefinition>(query, dictionary, new Action<QueryDefinition, QueryDefinitionUpgrader.QueryExpressionRewriter>(QueryDefinitionUpgrader.Rewrite)))
				{
					return false;
				}
				this.UpgradeFilterTargets(query.Where);
				query.Version = new int?(1);
				return true;
			}

			// Token: 0x06001AAA RID: 6826 RVA: 0x0002FA18 File Offset: 0x0002DC18
			internal bool TryUpgrade(FilterDefinition filterDef)
			{
				Dictionary<string, QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding> dictionary;
				if (!this.TryUpgradeEntitySources(filterDef.From, out dictionary))
				{
					return false;
				}
				if (!this.TryUpgradePropertyRefs<FilterDefinition>(filterDef, dictionary, new Action<FilterDefinition, QueryDefinitionUpgrader.QueryExpressionRewriter>(QueryDefinitionUpgrader.Rewrite)))
				{
					return false;
				}
				this.UpgradeFilterTargets(filterDef.Where);
				filterDef.Version = new int?(1);
				return true;
			}

			// Token: 0x06001AAB RID: 6827 RVA: 0x0002FA68 File Offset: 0x0002DC68
			private bool TryUpgradeEntitySources(List<EntitySource> from, out Dictionary<string, QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding> entityBindings)
			{
				if (from.IsNullOrEmpty<EntitySource>())
				{
					entityBindings = null;
					return true;
				}
				entityBindings = new Dictionary<string, QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding>(from.Count);
				foreach (EntitySource entitySource in from)
				{
					if (!string.IsNullOrEmpty(entitySource.Entity))
					{
						IConceptualEntity conceptualEntity;
						if (this.FederatedSchema == null)
						{
							entityBindings.Add(entitySource.Name, default(QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding));
						}
						else if (this.FederatedSchema.TryGetEntity(entitySource.Entity, entitySource.Schema, out conceptualEntity))
						{
							entityBindings.Add(entitySource.Name, new QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding(conceptualEntity, false));
						}
					}
					else if (!string.IsNullOrEmpty(entitySource.EntitySet))
					{
						if (this.FederatedSchema == null)
						{
							this.ErrorContext.RegisterError(QueryDefinitionUpgradeMessages.EntitySetReferenceWithNoSchema(), new object[0]);
							return false;
						}
						IConceptualEntity conceptualEntity2;
						if (this.FederatedSchema.TryGetEntityByEdmName(entitySource.EntitySet, entitySource.Schema, out conceptualEntity2))
						{
							entitySource.Entity = conceptualEntity2.Name;
							entitySource.EntitySet = null;
							entityBindings.Add(entitySource.Name, new QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding(conceptualEntity2, true));
						}
						else
						{
							entitySource.Entity = QueryDefinitionUpgrader.V0ToV1.StripEntityContainer(entitySource.EntitySet);
							entitySource.EntitySet = null;
						}
					}
					else if (entitySource.Expression != null && entitySource.Expression.Subquery != null)
					{
						QuerySubqueryExpression subquery = entitySource.Expression.Subquery;
						if (!this.TryUpgrade(subquery.Query))
						{
							return false;
						}
					}
					else
					{
						entitySource.Expression == null;
					}
				}
				return true;
			}

			// Token: 0x06001AAC RID: 6828 RVA: 0x0002FC2C File Offset: 0x0002DE2C
			private bool TryUpgradePropertyRefs<T>(T item, Dictionary<string, QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding> entityBindings, Action<T, QueryDefinitionUpgrader.QueryExpressionRewriter> rewrite)
			{
				QueryDefinitionUpgrader.V0ToV1.PropertyRefUpgrader propertyRefUpgrader = new QueryDefinitionUpgrader.V0ToV1.PropertyRefUpgrader(this.ErrorContext, entityBindings);
				rewrite(item, propertyRefUpgrader);
				return !propertyRefUpgrader.HasErrors;
			}

			// Token: 0x06001AAD RID: 6829 RVA: 0x0002FC58 File Offset: 0x0002DE58
			private void UpgradeFilterTargets(List<QueryFilter> where)
			{
				if (!where.IsNullOrEmpty<QueryFilter>())
				{
					for (int i = 0; i < where.Count; i++)
					{
						QueryFilter queryFilter = where[i];
						if (queryFilter.Condition.Exists == null)
						{
							queryFilter.Target = null;
						}
					}
				}
			}

			// Token: 0x06001AAE RID: 6830 RVA: 0x0002FCA0 File Offset: 0x0002DEA0
			private static string StripEntityContainer(string name)
			{
				int num = name.IndexOf('.');
				if (num <= -1)
				{
					return name;
				}
				return name.Substring(num + 1);
			}

			// Token: 0x02000365 RID: 869
			private struct ConceptualEntityBinding
			{
				// Token: 0x06001ABA RID: 6842 RVA: 0x0002FDED File Offset: 0x0002DFED
				internal ConceptualEntityBinding(IConceptualEntity entity, bool convertEdmNames)
				{
					this.Entity = entity;
					this.ConvertEdmNames = convertEdmNames;
				}

				// Token: 0x040009FC RID: 2556
				internal readonly IConceptualEntity Entity;

				// Token: 0x040009FD RID: 2557
				internal readonly bool ConvertEdmNames;
			}

			// Token: 0x02000366 RID: 870
			private sealed class PropertyRefUpgrader : QueryDefinitionUpgrader.QueryExpressionRewriter
			{
				// Token: 0x06001ABB RID: 6843 RVA: 0x0002FDFD File Offset: 0x0002DFFD
				internal PropertyRefUpgrader(IErrorContext errorContext, IReadOnlyDictionary<string, QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding> entityBindings)
				{
					this._errorContext = errorContext;
					this._entityBindings = entityBindings ?? Util.EmptyReadOnlyDictionary<string, QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding>();
				}

				// Token: 0x17000555 RID: 1365
				// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0002FE1C File Offset: 0x0002E01C
				internal bool HasErrors
				{
					get
					{
						return this._hasErrors;
					}
				}

				// Token: 0x06001ABD RID: 6845 RVA: 0x0002FE24 File Offset: 0x0002E024
				protected internal override QueryExpression Visit(QueryPropertyExpression expression)
				{
					return this.VisitPropertyExpression(expression);
				}

				// Token: 0x06001ABE RID: 6846 RVA: 0x0002FE2D File Offset: 0x0002E02D
				protected internal override QueryExpression Visit(QueryColumnExpression expression)
				{
					return this.VisitPropertyExpression(expression);
				}

				// Token: 0x06001ABF RID: 6847 RVA: 0x0002FE36 File Offset: 0x0002E036
				protected internal override QueryExpression Visit(QueryMeasureExpression expression)
				{
					return this.VisitPropertyExpression(expression);
				}

				// Token: 0x06001AC0 RID: 6848 RVA: 0x0002FE40 File Offset: 0x0002E040
				private QueryExpression VisitPropertyExpression(QueryPropertyExpression expression)
				{
					QuerySourceRefExpression sourceRef = expression.Expression.SourceRef;
					if (sourceRef == null)
					{
						return expression;
					}
					IConceptualProperty conceptualProperty = null;
					QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding conceptualEntityBinding;
					if (this._entityBindings.TryGetValue(sourceRef.Source, out conceptualEntityBinding))
					{
						if (conceptualEntityBinding.Entity == null)
						{
							if (!this._hasErrors)
							{
								this._errorContext.RegisterError(QueryDefinitionUpgradeMessages.PropertyExpressionWithNoSchema(), new object[0]);
								this._hasErrors = true;
							}
							return expression;
						}
						if (conceptualEntityBinding.ConvertEdmNames)
						{
							if (conceptualEntityBinding.Entity.TryGetPropertyByEdmName(expression.Property, out conceptualProperty))
							{
								expression.Property = conceptualProperty.Name;
							}
							else
							{
								conceptualProperty = null;
							}
						}
						else if (!conceptualEntityBinding.Entity.TryGetProperty(expression.Property, out conceptualProperty))
						{
							conceptualProperty = null;
						}
					}
					if (expression is QueryMeasureExpression)
					{
						return new QueryMeasureExpression
						{
							Expression = expression.Expression,
							Property = expression.Property
						};
					}
					if (expression is QueryColumnExpression)
					{
						return new QueryColumnExpression
						{
							Expression = expression.Expression,
							Property = expression.Property
						};
					}
					if (conceptualProperty != null && conceptualProperty is IConceptualMeasure)
					{
						return new QueryMeasureExpression
						{
							Expression = expression.Expression,
							Property = expression.Property
						};
					}
					return new QueryColumnExpression
					{
						Expression = expression.Expression,
						Property = expression.Property
					};
				}

				// Token: 0x040009FE RID: 2558
				private readonly IErrorContext _errorContext;

				// Token: 0x040009FF RID: 2559
				private readonly IReadOnlyDictionary<string, QueryDefinitionUpgrader.V0ToV1.ConceptualEntityBinding> _entityBindings;

				// Token: 0x04000A00 RID: 2560
				private bool _hasErrors;
			}
		}

		// Token: 0x02000361 RID: 865
		private sealed class V1ToV2 : QueryDefinitionUpgrader
		{
			// Token: 0x06001AAF RID: 6831 RVA: 0x0002FCC5 File Offset: 0x0002DEC5
			internal V1ToV2(IErrorContext errorContext)
				: base(errorContext, null)
			{
			}

			// Token: 0x06001AB0 RID: 6832 RVA: 0x0002FCD0 File Offset: 0x0002DED0
			internal void Upgrade(QueryDefinition query)
			{
				QueryDefinitionUpgrader.Rewrite(query, new QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader());
				query.Version = new int?(2);
				if (!query.From.IsNullOrEmpty<EntitySource>())
				{
					foreach (EntitySource entitySource in query.From)
					{
						if (entitySource.Expression != null && entitySource.Expression.Subquery != null)
						{
							this.Upgrade(entitySource.Expression.Subquery.Query);
						}
					}
				}
			}

			// Token: 0x06001AB1 RID: 6833 RVA: 0x0002FD78 File Offset: 0x0002DF78
			internal void Upgrade(FilterDefinition filterDef)
			{
				QueryDefinitionUpgrader.Rewrite(filterDef, new QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader());
				filterDef.Version = new int?(2);
			}

			// Token: 0x02000367 RID: 871
			private sealed class ConstantUpgrader : QueryDefinitionUpgrader.QueryExpressionRewriter
			{
				// Token: 0x06001AC1 RID: 6849 RVA: 0x0002FF82 File Offset: 0x0002E182
				protected internal override QueryExpression Visit(QueryNullConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(PrimitiveValue.Null);
				}

				// Token: 0x06001AC2 RID: 6850 RVA: 0x0002FF8E File Offset: 0x0002E18E
				protected internal override QueryExpression Visit(QueryStringConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(expression.Value);
				}

				// Token: 0x06001AC3 RID: 6851 RVA: 0x0002FFA0 File Offset: 0x0002E1A0
				protected internal override QueryExpression Visit(QueryIntegerConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(expression.Value);
				}

				// Token: 0x06001AC4 RID: 6852 RVA: 0x0002FFB2 File Offset: 0x0002E1B2
				protected internal override QueryExpression Visit(QueryDecimalConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(expression.Value);
				}

				// Token: 0x06001AC5 RID: 6853 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
				protected internal override QueryExpression Visit(QueryNumberConstantExpression expression)
				{
					return new QueryLiteralExpression
					{
						Value = expression.Value
					};
				}

				// Token: 0x06001AC6 RID: 6854 RVA: 0x0002FFD7 File Offset: 0x0002E1D7
				protected internal override QueryExpression Visit(QueryBooleanConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(expression.Value);
				}

				// Token: 0x06001AC7 RID: 6855 RVA: 0x0002FFE9 File Offset: 0x0002E1E9
				protected internal override QueryExpression Visit(QueryDateTimeConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(expression.Value);
				}

				// Token: 0x06001AC8 RID: 6856 RVA: 0x0002FFF8 File Offset: 0x0002E1F8
				protected internal override QueryExpression Visit(QueryDateConstantExpression expression)
				{
					QueryLiteralExpression queryLiteralExpression = QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(expression.Value);
					if (this._dateConstantAsLiteralOnly > 0)
					{
						return queryLiteralExpression;
					}
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Day, queryLiteralExpression);
				}

				// Token: 0x06001AC9 RID: 6857 RVA: 0x00030028 File Offset: 0x0002E228
				protected internal override QueryExpression Visit(QueryDateTimeSecondConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Second, QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(expression.Value));
				}

				// Token: 0x06001ACA RID: 6858 RVA: 0x00030040 File Offset: 0x0002E240
				protected internal override QueryExpression Visit(QueryYearConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Year, QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(new DateTime(expression.Value, 1, 1)));
				}

				// Token: 0x06001ACB RID: 6859 RVA: 0x0003005F File Offset: 0x0002E25F
				protected internal override QueryExpression Visit(QueryYearAndMonthConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Month, QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(new DateTime(expression.Year, expression.Month, 1)));
				}

				// Token: 0x06001ACC RID: 6860 RVA: 0x00030083 File Offset: 0x0002E283
				protected internal override QueryExpression Visit(QueryYearAndWeekConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Week, QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(expression.Value));
				}

				// Token: 0x06001ACD RID: 6861 RVA: 0x0003009B File Offset: 0x0002E29B
				protected internal override QueryExpression Visit(QueryDecadeConstantExpression expression)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Decade, QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(new DateTime(expression.Value, 1, 1)));
				}

				// Token: 0x06001ACE RID: 6862 RVA: 0x000300BC File Offset: 0x0002E2BC
				protected internal override QueryExpression Visit(QueryDatePartExpression expression)
				{
					this._dateConstantAsLiteralOnly++;
					base.Visit(expression);
					this._dateConstantAsLiteralOnly--;
					switch (expression.Function)
					{
					case QueryDatePartFunction.Date:
						return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Day, expression.Expression);
					case QueryDatePartFunction.YearAndWeek:
						return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Week, expression.Expression);
					case QueryDatePartFunction.Year:
						return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Year, expression.Expression);
					case QueryDatePartFunction.YearAndMonth:
						return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Month, expression.Expression);
					case QueryDatePartFunction.Decade:
						return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.DateSpan(TimeUnit.Decade, expression.Expression);
					}
					return expression;
				}

				// Token: 0x06001ACF RID: 6863 RVA: 0x00030156 File Offset: 0x0002E356
				protected internal override QueryExpression Visit(QueryDateSpanExpression expression)
				{
					this._dateConstantAsLiteralOnly++;
					base.Visit(expression);
					this._dateConstantAsLiteralOnly--;
					return expression;
				}

				// Token: 0x06001AD0 RID: 6864 RVA: 0x0003017D File Offset: 0x0002E37D
				protected internal override QueryExpression Visit(QueryDateAddExpression expression)
				{
					this._dateConstantAsLiteralOnly++;
					base.Visit(expression);
					this._dateConstantAsLiteralOnly--;
					return expression;
				}

				// Token: 0x06001AD1 RID: 6865 RVA: 0x000301A4 File Offset: 0x0002E3A4
				private static QueryDateSpanExpression DateSpan(TimeUnit timeUnit, QueryExpressionContainer expression)
				{
					return new QueryDateSpanExpression
					{
						Expression = expression,
						TimeUnit = timeUnit
					};
				}

				// Token: 0x06001AD2 RID: 6866 RVA: 0x000301B9 File Offset: 0x0002E3B9
				private static QueryLiteralExpression Literal(DateTime value)
				{
					return QueryDefinitionUpgrader.V1ToV2.ConstantUpgrader.Literal(DateTime.SpecifyKind(value, DateTimeKind.Unspecified));
				}

				// Token: 0x06001AD3 RID: 6867 RVA: 0x000301CC File Offset: 0x0002E3CC
				private static QueryLiteralExpression Literal(PrimitiveValue value)
				{
					return new QueryLiteralExpression
					{
						Value = PrimitiveValueEncoding.ToTypeEncodedString(value)
					};
				}

				// Token: 0x04000A01 RID: 2561
				private int _dateConstantAsLiteralOnly;
			}
		}
	}
}
