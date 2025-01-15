using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200018D RID: 397
	internal sealed class InstanceFilterCollector
	{
		// Token: 0x06000DBA RID: 3514 RVA: 0x00038364 File Offset: 0x00036564
		private InstanceFilterCollector()
		{
			this.m_filterDeclarations = new Dictionary<FilterCondition, PlanOperationDeclarationReference>();
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00038378 File Offset: 0x00036578
		internal static IFilterDeclarationCollection Analyze(PlanDeclarationCollection declarations, DataShapeContext dataShapeContext, ExpressionTable expressionTable, IFederatedConceptualSchema schema, out HashSet<FilterCondition> instanceFiltersRequiringPostProcessing)
		{
			InstanceFilterCollector instanceFilterCollector = new InstanceFilterCollector();
			instanceFilterCollector.RegisterInstanceFilters(dataShapeContext.PrimaryDynamics, declarations, expressionTable, schema);
			instanceFilterCollector.RegisterInstanceFilters(dataShapeContext.SecondaryDynamics, declarations, expressionTable, schema);
			instanceFiltersRequiringPostProcessing = instanceFilterCollector.m_instanceFiltersRequiringPostProcessing;
			return new FilterDeclarationCollection(instanceFilterCollector.m_filterDeclarations);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000383C0 File Offset: 0x000365C0
		private void RegisterInstanceFilters(IReadOnlyList<DataMember> members, PlanDeclarationCollection declarations, ExpressionTable expressionTable, IFederatedConceptualSchema schema)
		{
			if (members.IsNullOrEmpty<DataMember>())
			{
				return;
			}
			foreach (DataMember dataMember in members)
			{
				if (!dataMember.InstanceFilters.IsNullOrEmpty<FilterCondition>())
				{
					foreach (FilterCondition filterCondition in dataMember.InstanceFilters)
					{
						this.CreateAndRegisterInstanceFilterTable(declarations, expressionTable, schema, filterCondition, dataMember.Id);
					}
				}
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00038464 File Offset: 0x00036664
		private void CreateAndRegisterInstanceFilterTable(PlanDeclarationCollection declarations, ExpressionTable expressionTable, IFederatedConceptualSchema schema, FilterCondition condition, Identifier memberId)
		{
			PlanOperationDeclarationReference planOperationDeclarationReference;
			if (!this.m_filterDeclarations.TryGetValue(condition, out planOperationDeclarationReference))
			{
				INegatableCondition negatableCondition = condition as INegatableCondition;
				if (negatableCondition != null && negatableCondition.IsNegated && FilterComplexityAnalyzer.IsComplexFilter(condition, expressionTable, false, false, schema))
				{
					Microsoft.DataShaping.Util.AddToLazySet<FilterCondition>(ref this.m_instanceFiltersRequiringPostProcessing, condition, null);
					return;
				}
				planOperationDeclarationReference = new PlanOperationCreateFilterContextTable(condition).DeclareIfNotDeclared(PlanNames.FilterTable(memberId, null), declarations, true, false, null, false);
				this.m_filterDeclarations.Add(condition, planOperationDeclarationReference);
			}
		}

		// Token: 0x040006B5 RID: 1717
		private readonly Dictionary<FilterCondition, PlanOperationDeclarationReference> m_filterDeclarations;

		// Token: 0x040006B6 RID: 1718
		private HashSet<FilterCondition> m_instanceFiltersRequiringPostProcessing;
	}
}
