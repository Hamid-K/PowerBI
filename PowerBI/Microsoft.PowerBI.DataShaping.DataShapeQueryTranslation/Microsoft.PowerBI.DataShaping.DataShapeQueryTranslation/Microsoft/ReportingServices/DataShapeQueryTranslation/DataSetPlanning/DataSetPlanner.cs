using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000ED RID: 237
	internal sealed class DataSetPlanner
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x00024758 File Offset: 0x00022958
		private DataSetPlanner(ExpressionTable expressionTable, DataShapeAnnotations annotations, ScopeTree scopeTree, IFederatedConceptualSchema schema, TranslationErrorContext errorContext, ContextGraph contextGraph, ContextWeights contextWeights, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable)
		{
			this.m_inputExpressionTable = expressionTable;
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_schema = schema;
			this.m_contextGraph = contextGraph;
			this.m_contextWeights = contextWeights;
			this.m_translatedFilterTable = translatedFilterTable;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000247A8 File Offset: 0x000229A8
		public static DataSetPlanningResult DeterminePlans(DataShape dataShape, ExpressionTable expressionTable, DataShapeAnnotations annotations, ScopeTree scopeTree, IFederatedConceptualSchema schema, TranslationErrorContext errorContext)
		{
			ContextFilterTranslationResult contextFilterTranslationResult = DataSetPlannerContextFilterTranslator.Translate(dataShape, scopeTree, expressionTable, annotations);
			ContextGraph contextGraph = ContextGraphBuilder.BuildGraph(contextFilterTranslationResult.ScopeTree, annotations, contextFilterTranslationResult.OutputExpressionTable, contextFilterTranslationResult.OutputDataShape);
			ContextWeights contextWeights = ContextWeights.DetermineWeights(contextFilterTranslationResult.OutputDataShape, annotations, contextFilterTranslationResult.ScopeTree);
			return new DataSetPlanner(contextFilterTranslationResult.OutputExpressionTable, annotations, scopeTree, schema, errorContext, contextGraph, contextWeights, contextFilterTranslationResult.TranslatedFilterTable).InternalDeterminePlans(contextFilterTranslationResult.OutputDataShape, null);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00024810 File Offset: 0x00022A10
		private DataSetPlanningResult InternalDeterminePlans(DataShape dataShape, Func<DataSetPlanGeneratorResult, DataSetPlanGeneratorResult> planInfoSelector)
		{
			DataSetPlanGeneratorResult dataSetPlanGeneratorResult = DataSetPlanGenerator.Generate(this.m_annotations, this.m_scopeTree, this.m_errorContext, this.m_inputExpressionTable, this.m_contextGraph, this.m_contextWeights, dataShape);
			if (planInfoSelector != null)
			{
				dataSetPlanGeneratorResult = planInfoSelector(dataSetPlanGeneratorResult);
			}
			List<DataSetPlanInfo> list = DataSetPlannerLimitTranslator.Translate(dataSetPlanGeneratorResult.DataSetPlanInfos, this.m_scopeTree, this.m_annotations);
			OutputPlanMapping outputPlanMapping = dataSetPlanGeneratorResult.OutputPlanMapping;
			ReadOnlyCollection<DataSetPlan> readOnlyCollection = DataSetPlanTranslator.Translate(this.m_inputExpressionTable, this.m_scopeTree, this.m_schema, this.m_annotations, this.m_errorContext, list, outputPlanMapping, dataShape.HasFilterEmptyGroups(), null, this.m_translatedFilterTable, dataShape.ExtensionSchema, dataShape.DataSourceVariables, dataShape.ModelParameters, dataShape.QueryParameters);
			DataBindingMapping dataBindingMapping = null;
			if (!this.m_errorContext.HasError && !dataShape.ContextOnly.GetValueOrDefault<bool>())
			{
				dataBindingMapping = DsdDataBindingGenerator.GenerateDataBindings(dataShape, this.m_scopeTree, this.m_annotations, this.m_inputExpressionTable, list, outputPlanMapping);
			}
			bool flag = false;
			DataTransformReferenceMap empty = DataTransformReferenceMap.Empty;
			DataSetPlannerExpressionTranslationResult dataSetPlannerExpressionTranslationResult = DataSetPlannerExpressionTranslator.Translate(this.m_scopeTree, this.m_schema, this.m_annotations, this.m_errorContext, this.m_inputExpressionTable, this.m_contextGraph, this.m_contextWeights, dataShape, this.m_translatedFilterTable, empty, flag);
			ReadOnlyExpressionTable readOnlyExpressionTable = dataSetPlannerExpressionTranslationResult.OutputExpressionTable;
			List<DataSetPlan> subQueryPlans = dataSetPlannerExpressionTranslationResult.SubQueryPlans;
			if (!this.m_errorContext.HasError)
			{
				readOnlyCollection = DataSetPlannerFilterTranslator.Translate(readOnlyCollection, this.m_annotations, this.m_scopeTree, this.m_errorContext, readOnlyExpressionTable, this.m_schema).DataSetPlans;
			}
			if (!this.m_errorContext.HasError)
			{
				RollupTranslatorResult rollupTranslatorResult = RollupTranslator.Translate(dataShape, this.m_annotations, readOnlyCollection, this.m_scopeTree, this.m_errorContext, readOnlyExpressionTable);
				readOnlyExpressionTable = rollupTranslatorResult.ExpressionTable;
				readOnlyCollection = rollupTranslatorResult.DataSetPlans;
			}
			return new DataSetPlanningResult(readOnlyCollection, dataBindingMapping, outputPlanMapping, readOnlyExpressionTable, subQueryPlans, dataShape);
		}

		// Token: 0x04000484 RID: 1156
		private readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x04000485 RID: 1157
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000486 RID: 1158
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000487 RID: 1159
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x04000488 RID: 1160
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000489 RID: 1161
		private readonly IReadOnlyDictionary<IIdentifiable, List<Filter>> m_translatedFilterTable;

		// Token: 0x0400048A RID: 1162
		private ContextGraph m_contextGraph;

		// Token: 0x0400048B RID: 1163
		private ContextWeights m_contextWeights;
	}
}
