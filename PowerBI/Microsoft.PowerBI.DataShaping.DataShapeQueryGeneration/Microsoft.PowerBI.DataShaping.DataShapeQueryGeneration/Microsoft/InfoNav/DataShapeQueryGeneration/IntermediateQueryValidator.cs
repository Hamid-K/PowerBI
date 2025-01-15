using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000E3 RID: 227
	internal sealed class IntermediateQueryValidator
	{
		// Token: 0x060007CA RID: 1994 RVA: 0x0001D584 File Offset: 0x0001B784
		internal static bool Validate(DataShapeGenerationInternalContext internalContext, ResolvedQueryDefinition query, DataShapeBinding binding, QueryProjections queryProjections, SemanticQueryDataShapeAnnotations annotations)
		{
			return IntermediateQueryValidator.ValidateProjections(internalContext, query, queryProjections, binding, annotations) && IntermediateQueryValidator.ValidateSubqueryReference(internalContext, query, binding, queryProjections, annotations);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		internal static bool ValidateProjections(DataShapeGenerationInternalContext context, ResolvedQueryDefinition query, QueryProjections projections, DataShapeBinding binding, SemanticQueryDataShapeAnnotations annotations)
		{
			if (IntermediateQueryValidator.HasDetailGroupAndSubtotal(projections))
			{
				context.ErrorContext.Register(DataShapeGenerationMessages.GroupByAndSubtotal(EngineMessageSeverity.Error));
				return false;
			}
			if (IntermediateQueryValidator.HasUnsupportedSortByMeasure(context, projections, query, binding, annotations))
			{
				return false;
			}
			if (annotations.QueryHasVisualCalculationsExpressions(query.Name) && binding != null)
			{
				if (binding.Primary.HasSuppressedProjections() || binding.Secondary.HasSuppressedProjections())
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedSuppressedProjectionWithVisualCalculations(EngineMessageSeverity.Error));
					return false;
				}
				if (binding.Primary.HasShowItemsWithNoData() || binding.Secondary.HasShowItemsWithNoData())
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedShowItemsWithNoDataWithVisualCalculations(EngineMessageSeverity.Error));
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001D654 File Offset: 0x0001B854
		internal static bool ValidateSubqueryReference(DataShapeGenerationInternalContext context, ResolvedQueryDefinition query, DataShapeBinding binding, QueryProjections projections, SemanticQueryDataShapeAnnotations annotations)
		{
			int expressionSourceForRegroupingCount = annotations.GetExpressionSourceForRegroupingCount(query);
			if (expressionSourceForRegroupingCount == 0)
			{
				return true;
			}
			if (!query.GroupBy.IsNullOrEmpty<ResolvedQueryExpression>())
			{
				context.ErrorContext.Register(DataShapeGenerationMessages.GroupByAndSubqueryReferenceInSelect(EngineMessageSeverity.Error));
				return false;
			}
			if (projections.HasGroups)
			{
				if (!context.FederatedConceptualSchema.GetCapabilities().SupportsSubqueryRegrouping)
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedSubqueryRegrouping(EngineMessageSeverity.Error));
					return false;
				}
				if (expressionSourceForRegroupingCount > 1)
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.InvalidNumberOfSubqueries(EngineMessageSeverity.Error));
					return false;
				}
				if (!IntermediateQueryValidator.IsValidQueryForSubqueryReferences(query, binding, context.ErrorContext, projections, false))
				{
					return false;
				}
				if (!IntermediateQueryValidator.IsValidBindingForSubqueryRegrouping(binding, context.ErrorContext))
				{
					return false;
				}
			}
			else if (expressionSourceForRegroupingCount == 1)
			{
				if (!IntermediateQueryValidator.IsValidQueryForSubqueryReferences(query, binding, context.ErrorContext, projections, true))
				{
					return false;
				}
				if (!IntermediateQueryValidator.IsValidBindingForSubqueryReferences(binding, context.ErrorContext))
				{
					return false;
				}
			}
			else if (expressionSourceForRegroupingCount > 1)
			{
				if (!IntermediateQueryValidator.IsValidQueryForSubqueryReferences(query, binding, context.ErrorContext, projections, false))
				{
					return false;
				}
				if (!IntermediateQueryValidator.IsValidBindingForSubqueryReferences(binding, context.ErrorContext))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001D748 File Offset: 0x0001B948
		private static bool HasDetailGroupAndSubtotal(QueryProjections projections)
		{
			bool flag = false;
			bool flag2 = false;
			IntermediateQueryValidator.TraverseGroupingAxis(projections.PrimaryMembers, ref flag, ref flag2);
			IntermediateQueryValidator.TraverseGroupingAxis(projections.SecondaryMembers, ref flag, ref flag2);
			return flag && flag2;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001D77C File Offset: 0x0001B97C
		private static bool HasUnsupportedSortByMeasure(DataShapeGenerationInternalContext context, QueryProjections projections, ResolvedQueryDefinition query, DataShapeBinding binding, SemanticQueryDataShapeAnnotations annotations)
		{
			if (projections.HasSortByMeasure)
			{
				IList<ScopedDataReduction> list;
				if (binding == null)
				{
					list = null;
				}
				else
				{
					DataReduction dataReduction = binding.DataReduction;
					list = ((dataReduction != null) ? dataReduction.Scoped : null);
				}
				IList<ScopedDataReduction> list2 = list;
				Func<int, bool> <>9__3;
				if (!list2.IsNullOrEmptyCollection<ScopedDataReduction>() && list2.Any(delegate(ScopedDataReduction limit)
				{
					IList<int> primary2 = limit.Scope.Primary;
					if (primary2 == null)
					{
						return false;
					}
					Func<int, bool> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = (int index) => index != 0 && projections.PrimaryMembers[index].Group.HasSortByMeasure());
					}
					return primary2.Any(func);
				}))
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedSortByMeasure("DataReduction.Scoped", false));
					return true;
				}
				IList<DataShapeBindingAxisSynchronizedGroupingBlock> list3;
				if (binding == null)
				{
					list3 = null;
				}
				else
				{
					DataShapeBindingAxis primary = binding.Primary;
					list3 = ((primary != null) ? primary.Synchronization : null);
				}
				IList<DataShapeBindingAxisSynchronizedGroupingBlock> list4 = list3;
				Func<int, bool> <>9__4;
				if (!list4.IsNullOrEmptyCollection<DataShapeBindingAxisSynchronizedGroupingBlock>() && list4.Any(delegate(DataShapeBindingAxisSynchronizedGroupingBlock sync)
				{
					IEnumerable<int> groupings = sync.Groupings;
					Func<int, bool> func2;
					if ((func2 = <>9__4) == null)
					{
						func2 = (<>9__4 = (int index) => projections.PrimaryMembers[index].Group.HasSortByMeasure());
					}
					return groupings.Any(func2);
				}))
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedSortByMeasure("DataShapeBindingAxis.Synchronization", false));
					return true;
				}
				if (annotations.QueryHasVisualCalculationsExpressions(query.Name))
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedSortByMeasure("Visual Calculations", true));
					return true;
				}
				if (binding != null && !binding.HiddenProjections.IsNullOrEmptyCollection<DataShapeBindingHiddenProjections>())
				{
					if (projections.PrimaryMembers.Any(delegate(QueryMember m)
					{
						if (m.Group.HasSortByMeasure())
						{
							return m.Group.SortKeys.Any(delegate(DsqSortKey s)
							{
								if (s.IsMeasure)
								{
									DsqSortKeyProjection dsqSortKeyProjection = s as DsqSortKeyProjection;
									if (dsqSortKeyProjection != null)
									{
										return dsqSortKeyProjection.Projection.IsContextOnly;
									}
								}
								return false;
							});
						}
						return false;
					}))
					{
						context.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedSortByMeasure("Hidden Projections", true));
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001D8C4 File Offset: 0x0001BAC4
		private static void TraverseGroupingAxis(IReadOnlyList<QueryMember> members, ref bool hasSubtotal, ref bool hasDetailGroup)
		{
			if (members == null)
			{
				return;
			}
			for (int i = 0; i < members.Count; i++)
			{
				QueryGroup group = members[i].Group;
				hasSubtotal |= group.Subtotal > SubtotalType.None;
				hasDetailGroup |= group.DetailGroupIdentity != null;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001D910 File Offset: 0x0001BB10
		private static bool IsValidQueryForSubqueryReferences(ResolvedQueryDefinition query, DataShapeBinding binding, DataShapeGenerationErrorContext errorContext, QueryProjections projections, bool allowSingleValueForSelect = false)
		{
			foreach (ResolvedQuerySource resolvedQuerySource in query.From)
			{
				if (!(resolvedQuerySource is ResolvedEntitySource))
				{
					ResolvedExpressionSource resolvedExpressionSource = resolvedQuerySource as ResolvedExpressionSource;
					if (resolvedExpressionSource == null)
					{
						errorContext.Register(DataShapeGenerationMessages.InvalidFromSourceItemWithSubqueryRegrouping(EngineMessageSeverity.Error));
						return false;
					}
					if (!(resolvedExpressionSource.Expression is ResolvedQuerySubqueryExpression) && !(resolvedExpressionSource.Expression is ResolvedQueryLetRefExpression))
					{
						errorContext.Register(DataShapeGenerationMessages.InvalidFromSourceExpressionWithSubqueryRegrouping(EngineMessageSeverity.Error));
						return false;
					}
				}
			}
			IReadOnlyList<int> readOnlyList;
			if (binding == null)
			{
				readOnlyList = null;
			}
			else
			{
				IList<int> projections2 = binding.Projections;
				readOnlyList = ((projections2 != null) ? projections2.AsReadOnlyList<int>() : null);
			}
			IReadOnlyList<ProjectedDsqExpression> readOnlyList2 = (projections.HasGroups ? null : projections.Measures);
			HashSet<int> allTopLevelMeasureIndices = IntermediateQueryValidator.GetAllTopLevelMeasureIndices(readOnlyList, readOnlyList2);
			for (int i = 0; i < query.Select.Count; i++)
			{
				ResolvedQuerySelect resolvedQuerySelect = query.Select[i];
				ExpressionContext expressionContext = new ExpressionContext(query.Name, SemanticQueryObjectType.Select, i);
				if (allTopLevelMeasureIndices.Contains(i))
				{
					AllowedExpressionContentFlags allowedExpressionContentFlags = (allowSingleValueForSelect ? AllowedExpressionContent.TopLevelSelectWithSubqueryRegroupingWithSingleValue : AllowedExpressionContent.TopLevelSelectWithSubqueryRegrouping);
					if (!ResolvedQueryExpressionValidator.Validate(resolvedQuerySelect.Expression, errorContext, allowedExpressionContentFlags, expressionContext))
					{
						return false;
					}
				}
				else
				{
					AllowedExpressionContentFlags allowedExpressionContentFlags2 = (allowSingleValueForSelect ? AllowedExpressionContent.SelectSubqueryReferencesWithSingleValue : AllowedExpressionContent.SelectSubqueryReferences);
					if (!ResolvedQueryExpressionValidator.Validate(resolvedQuerySelect.Expression, errorContext, allowedExpressionContentFlags2, expressionContext))
					{
						return false;
					}
				}
			}
			AllowedExpressionContentFlags allowedExpressionContentFlags3 = (projections.HasGroups ? AllowedExpressionContent.WhereExpressionSubqueryRegrouping : AllowedExpressionContent.WhereExpression);
			HashSet<string> hashSet = null;
			for (int j = 0; j < query.Where.Count; j++)
			{
				HashSet<string> hashSet2;
				if (!ResolvedQueryExpressionValidator.Validate(query.Where[j].Condition, errorContext, allowedExpressionContentFlags3, new ExpressionContext(query.Name, SemanticQueryObjectType.Where, j), out hashSet2))
				{
					return false;
				}
				if (hashSet2.Count > 1)
				{
					errorContext.Register(DataShapeGenerationMessages.InvalidWhereItemWithSubqueryAggregation(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.UnsupportedWhereFilterWithSubqueriesAggregation(j, hashSet2.ToList<string>())));
					return false;
				}
				if (hashSet == null)
				{
					hashSet = hashSet2;
				}
				else
				{
					hashSet.UnionWith(hashSet2);
					if (hashSet.Count > 1)
					{
						errorContext.Register(DataShapeGenerationMessages.UnsupportedMultipleSubqueryPostFilters(EngineMessageSeverity.Error));
						return false;
					}
				}
			}
			for (int k = 0; k < query.OrderBy.Count; k++)
			{
				if (!ResolvedQueryExpressionValidator.Validate(query.OrderBy[k].Expression, errorContext, AllowedExpressionContent.OrderByExpressionSubqueryReferences, new ExpressionContext(query.Name, SemanticQueryObjectType.OrderBy, k)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001DB8C File Offset: 0x0001BD8C
		private static HashSet<int> GetAllTopLevelMeasureIndices(IReadOnlyList<int> topLevelProjections, IReadOnlyList<ProjectedDsqExpression> topLevelMeasureProjections)
		{
			HashSet<int> hashSet = new HashSet<int>();
			if (topLevelProjections != null)
			{
				foreach (int num in topLevelProjections)
				{
					hashSet.Add(num);
				}
			}
			foreach (int num2 in QueryUtils.GetAllProjectedExpressionIndices(topLevelMeasureProjections))
			{
				hashSet.Add(num2);
			}
			return hashSet;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001DC24 File Offset: 0x0001BE24
		private static bool IsValidBindingForSubqueryRegrouping(DataShapeBinding binding, DataShapeGenerationErrorContext errorContext)
		{
			if (binding == null)
			{
				return true;
			}
			if (!IntermediateQueryValidator.IsValidBindingForSubqueryReferences(binding, errorContext))
			{
				return false;
			}
			if (!IntermediateQueryValidator.IsValidBindingAxis(binding.Primary, false))
			{
				errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAxisWithSubqueryRegrouping(EngineMessageSeverity.Error));
				return false;
			}
			if (!IntermediateQueryValidator.IsValidBindingAxis(binding.Secondary, true))
			{
				errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingAxisWithSubqueryRegrouping(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001DC80 File Offset: 0x0001BE80
		private static bool IsValidBindingForSubqueryReferences(DataShapeBinding binding, DataShapeGenerationErrorContext errorContext)
		{
			if (binding == null)
			{
				return true;
			}
			if (!binding.Highlights.IsNullOrEmptyCollection<FilterDefinition>())
			{
				errorContext.Register(DataShapeGenerationMessages.InvalidDataShapeBindingWithSubqueryRegrouping(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001DCAC File Offset: 0x0001BEAC
		private static bool IsValidBindingAxis(DataShapeBindingAxis axis, bool blockSuppressedProjections = false)
		{
			return axis == null || (!(axis.Expansion != null) && (axis.Groupings == null || !axis.Groupings.Any((DataShapeBindingAxisGrouping g) => !IntermediateQueryValidator.IsValidBindingAxisGrouping(g, blockSuppressedProjections))));
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001DD04 File Offset: 0x0001BF04
		private static bool IsValidBindingAxisGrouping(DataShapeBindingAxisGrouping grouping, bool blockSuppressedProjections)
		{
			return grouping.InstanceFilters.IsNullOrEmptyCollection<FilterDefinition>() && grouping.ShowItemsWithNoData.IsNullOrEmptyCollection<int>() && (grouping.Subtotal == null || grouping.Subtotal.Value == SubtotalType.None) && (!blockSuppressedProjections || grouping.SuppressedProjections.IsNullOrEmptyCollection<int>());
		}
	}
}
