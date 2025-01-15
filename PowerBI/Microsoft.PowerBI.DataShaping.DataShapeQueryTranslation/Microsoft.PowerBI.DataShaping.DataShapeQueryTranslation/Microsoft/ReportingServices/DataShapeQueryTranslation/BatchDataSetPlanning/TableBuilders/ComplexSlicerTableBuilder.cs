using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001E1 RID: 481
	internal sealed class ComplexSlicerTableBuilder
	{
		// Token: 0x060010A1 RID: 4257 RVA: 0x00045554 File Offset: 0x00043754
		private ComplexSlicerTableBuilder(ExpressionTable expressionTable, PlanOperationGroupAndJoin groupAndJoin, bool allowHierarchicalDisjunction, IFederatedConceptualSchema schema, PlanDeclarationCollection declarations, Identifier slicerTargetId, TranslationErrorContext errorContext)
		{
			this.m_expressionTable = expressionTable;
			this.m_schema = schema;
			this.m_groupAndJoin = groupAndJoin;
			this.m_allowHierarchicalDisjunction = allowHierarchicalDisjunction;
			this.m_declarations = declarations;
			this.m_slicerTargetId = slicerTargetId;
			this.m_errorContext = errorContext;
			this.m_filterDeclarationIndex = 0;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000455A3 File Offset: 0x000437A3
		internal static IEnumerable<PlanOperation> BuildSlicerTables(ExpressionTable expressionTable, PlanOperationGroupAndJoin groupAndJoin, FilterCondition filterCondition, bool precomputedToRequireSubqueries, bool allowHierarchicalDisjunction, IFederatedConceptualSchema schema, PlanDeclarationCollection declarations, Identifier slicerTargetId, TranslationErrorContext errorContext)
		{
			return new ComplexSlicerTableBuilder(expressionTable, groupAndJoin, allowHierarchicalDisjunction, schema, declarations, slicerTargetId, errorContext).ToSlicerTables(filterCondition, precomputedToRequireSubqueries);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000455C0 File Offset: 0x000437C0
		private IEnumerable<PlanOperation> ToSlicerTables(FilterCondition filterCondition, bool precomputedToRequireSubqueries)
		{
			List<FilterCondition> list;
			bool flag;
			if (!this.TryGetFilterConditionsForJoinPredicateSubQuery(filterCondition, this.m_expressionTable, precomputedToRequireSubqueries, out list, out flag))
			{
				return new PlanOperation[] { filterCondition.CreateFilterContextTable() };
			}
			if (flag)
			{
				this.m_errorContext.Register(TranslationMessages.ComplexSlicerNotAllowed(EngineMessageSeverity.Error, filterCondition.Id));
				return Enumerable.Empty<PlanOperation>();
			}
			Microsoft.DataShaping.Contract.RetailAssert(this.m_groupAndJoin.PrimaryGroupingBucket.Count > 0 || this.m_groupAndJoin.SecondaryGroupingBucket.Count > 0 || this.m_groupAndJoin.GroupingTransformColumns.Count > 0, "Complex filters can only be applied to groups");
			CompoundFilterCondition compoundFilterCondition = (CompoundFilterCondition)filterCondition;
			if (compoundFilterCondition.Operator == CompoundFilterOperator.Any)
			{
				IEnumerable<IEnumerable<PlanOperation>> enumerable = list.Select((FilterCondition c) => this.ToSlicerTables(c, false));
				List<PlanOperation> list2 = new List<PlanOperation>();
				foreach (IEnumerable<PlanOperation> enumerable2 in enumerable)
				{
					IList<PlanOperation> list3 = enumerable2.Evaluate<PlanOperation>();
					if (list3.Count == 1 && list3[0] is PlanOperationUnion)
					{
						list2.Add(list3[0]);
					}
					else
					{
						PlanOperationGroupAndJoin planOperationGroupAndJoin = this.CreateGroupAndJoin(list3);
						if (this.m_schema.GetDefaultSchemaDaxCapabilitiesAnnotation().SupportsKeepFiltersOverTableVariable())
						{
							PlanOperationDeclarationReference planOperationDeclarationReference = planOperationGroupAndJoin.DeclareIfNotDeclared(PlanNames.FilterTable(this.m_slicerTargetId, new int?(this.m_filterDeclarationIndex)), this.m_declarations, false, false, null, false);
							this.m_filterDeclarationIndex++;
							list2.Add(planOperationDeclarationReference);
						}
						else
						{
							list2.Add(planOperationGroupAndJoin);
						}
					}
				}
				PlanOperation planOperation = new PlanOperationUnion(list2);
				return new PlanOperation[] { planOperation };
			}
			if (compoundFilterCondition.Operator == CompoundFilterOperator.NotAny)
			{
				Microsoft.DataShaping.Contract.RetailAssert(compoundFilterCondition.Conditions.Single("DSQGen should push all filter negations to the leaf conditions besides individual IN Conditions", Array.Empty<string>()) is InFilterCondition, "DSQGen should push all filter negations to the leaf conditions besides individual IN Conditions");
			}
			else
			{
				Microsoft.DataShaping.Contract.RetailAssert(compoundFilterCondition.Operator == CompoundFilterOperator.All, "Unsupported CompounCondition operator for ComplexSlicerTableBuilder");
			}
			return list.SelectMany((FilterCondition c) => this.ToSlicerTables(c, false)).ToList<PlanOperation>();
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000457E4 File Offset: 0x000439E4
		private bool TryGetFilterConditionsForJoinPredicateSubQuery(FilterCondition filterCondition, ExpressionTable expressionTable, bool precomputedToRequireSubqueries, out List<FilterCondition> filterConditions, out bool foundUnsplittableDisjunction)
		{
			if (precomputedToRequireSubqueries)
			{
				filterConditions = FilterComplexityAnalyzer.SplitUpDisjunctions(filterCondition, expressionTable, this.m_allowHierarchicalDisjunction, true, this.m_schema, out foundUnsplittableDisjunction);
				return true;
			}
			return FilterComplexityAnalyzer.TryGetFilterConditionsForJoinPredicateSubQuery(filterCondition, this.m_schema, expressionTable, this.m_allowHierarchicalDisjunction, true, out filterConditions, out foundUnsplittableDisjunction);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0004581C File Offset: 0x00043A1C
		private PlanOperationGroupAndJoin CreateGroupAndJoin(IEnumerable<PlanOperation> contextTables)
		{
			return this.m_groupAndJoin.AddContextTables(contextTables);
		}

		// Token: 0x040007CB RID: 1995
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040007CC RID: 1996
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x040007CD RID: 1997
		private readonly PlanOperationGroupAndJoin m_groupAndJoin;

		// Token: 0x040007CE RID: 1998
		private readonly bool m_allowHierarchicalDisjunction;

		// Token: 0x040007CF RID: 1999
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x040007D0 RID: 2000
		private readonly Identifier m_slicerTargetId;

		// Token: 0x040007D1 RID: 2001
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040007D2 RID: 2002
		private int m_filterDeclarationIndex;
	}
}
