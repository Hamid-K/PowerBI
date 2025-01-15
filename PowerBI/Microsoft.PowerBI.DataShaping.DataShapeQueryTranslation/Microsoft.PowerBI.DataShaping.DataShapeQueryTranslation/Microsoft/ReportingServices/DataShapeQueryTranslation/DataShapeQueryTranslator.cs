using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDefinitionGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DefinitionGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Normalization;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection;
using Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000055 RID: 85
	internal sealed class DataShapeQueryTranslator : IDataShapeQueryTranslator
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000CE28 File Offset: 0x0000B028
		public DataShapeQueryTranslationResult Translate(DataShapeQueryTranslationContext context)
		{
			return context.TelemetryService.RunInActivity<DataShapeQueryTranslationResult>(ActivityKind.DataShapeQueryTranslation, () => this.TranslateImpl(context));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000CE68 File Offset: 0x0000B068
		private DataShapeQueryTranslationResult TranslateImpl(DataShapeQueryTranslationContext context)
		{
			TranslationErrorContext errorContext = new TranslationErrorContext();
			QueryPatternKind? queryPattern = null;
			DataShapeDefinition dataShapeDefinition = null;
			try
			{
				dataShapeDefinition = CancelUtilities.RunWithCancelTimer<DataShapeDefinition>(delegate(CancellationToken compositeCancelToken)
				{
					context = context.CloneWithOverrides(new CancellationToken?(compositeCancelToken));
					return DataShapeQueryTranslator.TranslateImplCore(context, errorContext, out queryPattern);
				}, DataShapeEngineConfig.MaximumDurationForDataShapeQueryTranslationInMs, delegate
				{
					context.TelemetryInfo.WasCancelledInternally = true;
				}, context.CancellationToken);
			}
			catch (OperationCanceledException ex)
			{
				if (context.TelemetryInfo.WasCancelledInternally)
				{
					string text = DsqtStrings.TranslationMaximumDurationExceeded(DataShapeEngineConfig.MaximumDurationForDataShapeQueryTranslationInMs);
					context.Tracer.TraceSanitizedError(ex, text);
					context.Dumper.Dump(text, ex);
				}
				DataShapeEngineCanceledException.ThrowForCancel(ex);
				throw;
			}
			catch (DataShapeEngineException ex2)
			{
				string text2 = "DataShapeEngineException exception in DataShapeQueryTranslation";
				context.Tracer.TraceSanitizedError(ex2, text2);
				throw;
			}
			catch (Exception ex3)
			{
				if (ErrorUtils.IsStoppingException(ex3))
				{
					throw;
				}
				context.Tracer.TraceSanitizedError(ex3, DsqtStrings.UnexpectedError);
				context.Dumper.Dump(DsqtStrings.UnexpectedError, ex3);
				throw DataShapeQueryTranslationException.Create(ex3);
			}
			finally
			{
				errorContext.TraceSanitizedMessages(context.Tracer);
			}
			if (errorContext.HasError)
			{
				throw DataShapeQueryTranslationException.Create(errorContext);
			}
			if (dataShapeDefinition == null)
			{
				throw DataShapeQueryTranslationException.Create(new NullReferenceException("DataShapeDefinition should have value if ErrorContext has no error."));
			}
			return new DataShapeQueryTranslationResult(dataShapeDefinition, queryPattern.Value, errorContext);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D008 File Offset: 0x0000B208
		private static DataShapeDefinition TranslateImplCore(DataShapeQueryTranslationContext context, TranslationErrorContext errorContext, out QueryPatternKind? queryPattern)
		{
			DataShapeQueryTranslationTelemetry telemetryInfo = context.TelemetryInfo;
			queryPattern = context.TestOnlyOverrideQueryPattern;
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape = context.DataShape;
			ScopeTree scopeTree = ScopeTreeBuilder.BuildScopeTree(dataShape);
			IdentifierValidator identifierValidator = new IdentifierValidator(errorContext);
			ReadOnlyExpressionTable readOnlyExpressionTable = DataShapeValidator.Validate(dataShape, scopeTree, identifierValidator, errorContext);
			bool flag = context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.ConceptualSchema);
			DataSourceContext dataSourceContext = DataShapeQueryTranslator.GetDataSourceContext(dataShape, context, errorContext, flag);
			FederatedEntityDataModel federatedEntityDataModel = null;
			if (!errorContext.HasError && !context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema))
			{
				Microsoft.DataShaping.Contract.RetailAssert(dataSourceContext.Model != null, "Expected EntityDataModel to be non null");
				Stopwatch stopwatch = Stopwatch.StartNew();
				EntityDataModel entityDataModel = dataSourceContext.Model.OverrideExtensionSchema(dataShape.ExtensionSchema);
				stopwatch.Stop();
				if (entityDataModel != dataSourceContext.Model)
				{
					telemetryInfo.ModelCloneDuration = stopwatch.ElapsedMilliseconds;
				}
				federatedEntityDataModel = FederatedEntityDataModelFactory.CreateModel(entityDataModel, dataShape.ExtensionSchema);
			}
			IFederatedConceptualSchema federatedConceptualSchema = ((dataSourceContext != null) ? dataSourceContext.FederatedConceptualSchema : null);
			ModelReconciliationResult modelReconciliationResult = null;
			if (!errorContext.HasError)
			{
				modelReconciliationResult = ModelReconciler.Reconcile(dataShape, readOnlyExpressionTable, federatedConceptualSchema, scopeTree, identifierValidator, errorContext, context.FeatureSwitchProvider);
				readOnlyExpressionTable = modelReconciliationResult.ExpressionTable;
			}
			DataTransformInliningResult dataTransformInliningResult = null;
			if (!errorContext.HasError)
			{
				dataTransformInliningResult = DataTransformInliner.InlineTransforms(dataShape, readOnlyExpressionTable, errorContext, context.Options.ApplyTransformsInQuery.Value);
				readOnlyExpressionTable = dataTransformInliningResult.ExpressionTable;
			}
			DataShapeNormalizationResult dataShapeNormalizationResult = null;
			if (!errorContext.HasError)
			{
				dataShapeNormalizationResult = DataShapeNormalizer.Normalize(dataShape, readOnlyExpressionTable, scopeTree, errorContext, modelReconciliationResult.SubtotalAnnotations);
				readOnlyExpressionTable = dataShapeNormalizationResult.ExpressionTable;
			}
			DataShapeAnnotations dataShapeAnnotations = null;
			if (!errorContext.HasError)
			{
				DataMemberAnnotations dataMemberAnnotations = DataMemberAnnotationAnalyzer.Analyze(dataShape, scopeTree, readOnlyExpressionTable, dataShapeNormalizationResult.SubtotalAnnotations);
				dataShapeAnnotations = DataShapeAnnotationAnalyzer.Analyze(dataShape, federatedConceptualSchema, dataMemberAnnotations, scopeTree, readOnlyExpressionTable, errorContext, dataShapeNormalizationResult.SubtotalAnnotations);
			}
			if (!errorContext.HasError)
			{
				readOnlyExpressionTable = DataShapeSemanticValidator.Validate(dataShape, federatedConceptualSchema, readOnlyExpressionTable, dataShapeAnnotations, scopeTree, errorContext, context.Options);
			}
			DataShapeContext dataShapeContext = null;
			if (!errorContext.HasError)
			{
				dataShapeContext = DataShapeContext.Create(dataShape, dataShapeAnnotations, scopeTree);
				if (queryPattern == null)
				{
					IFederatedConceptualSchema federatedConceptualSchema2 = federatedConceptualSchema;
					ScopeTree scopeTree2 = scopeTree;
					DataShapeAnnotations dataShapeAnnotations2 = dataShapeAnnotations;
					ExpressionTable expressionTable = readOnlyExpressionTable;
					bool value = context.Options.ApplyTransformsInQuery.Value;
					QueryPatternSelectionContext queryPatternSelectionContext = new QueryPatternSelectionContext(errorContext, federatedConceptualSchema2, scopeTree2, dataShapeAnnotations2, expressionTable, context.FeatureSwitchProvider, value);
					QueryPatternSelectionResult queryPatternSelectionResult = QueryPatternSelector.SelectPattern(dataShapeContext, queryPatternSelectionContext);
					telemetryInfo.QueryPattern = queryPatternSelectionResult.QueryPattern.ToString();
					telemetryInfo.Reasons = QueryPatternReasonCollection.CreateReasonsString<QueryPatternReason>(queryPatternSelectionResult.Reasons, "|");
					queryPattern = new QueryPatternKind?(queryPatternSelectionResult.QueryPattern);
				}
			}
			DataShapeDefinition dataShapeDefinition = null;
			if (!errorContext.HasError)
			{
				bool flag2 = !dataShapeContext.IsDictionaryEncodingNeeded();
				QueryPatternKind? queryPatternKind = queryPattern;
				QueryPatternKind queryPatternKind2 = QueryPatternKind.SuperDax;
				if ((queryPatternKind.GetValueOrDefault() == queryPatternKind2) & (queryPatternKind != null))
				{
					dataShapeDefinition = DataShapeQueryTranslator.TranslateWithSuperDaxQueryPattern(context, errorContext, dataShapeContext, scopeTree, readOnlyExpressionTable, dataSourceContext, federatedEntityDataModel, federatedConceptualSchema, dataShapeAnnotations, dataTransformInliningResult, telemetryInfo, flag2);
				}
				else
				{
					dataShapeDefinition = DataShapeQueryTranslator.TranslateWithRegularQueryPattern(context, errorContext, dataShapeContext, scopeTree, readOnlyExpressionTable, dataSourceContext, federatedEntityDataModel, federatedConceptualSchema, dataShapeAnnotations, dataTransformInliningResult, flag2);
				}
			}
			return dataShapeDefinition;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		private static DataShapeDefinition TranslateWithRegularQueryPattern(DataShapeQueryTranslationContext context, TranslationErrorContext errorContext, DataShapeContext dataShapeContext, ScopeTree scopeTree, ReadOnlyExpressionTable expressionTable, DataSourceContext dataSourceContext, FederatedEntityDataModel model, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, DataTransformInliningResult transformInliningResult, bool disableDictionaryEncoding)
		{
			Microsoft.DataShaping.Contract.RetailAssert(!context.Options.OmitOrderBy, "OmitOrderBy is not supported with the Regular QueryPattern.");
			Microsoft.DataShaping.Contract.RetailAssert(!context.Options.GenerateComposableQueryColumnNames, "GenerateComposableQueryColumnNames is not supported with the Regular QueryPattern.");
			Microsoft.DataShaping.Contract.RetailAssert(!dataShapeContext.HasInstanceFilters, "Instance filters are not supported in the Regular QueryPattern.");
			DataSetPlanningResult dataSetPlanningResult = null;
			if (!errorContext.HasError)
			{
				dataSetPlanningResult = DataSetPlanner.DeterminePlans(dataShapeContext.DataShape, expressionTable, annotations, scopeTree, schema, errorContext);
			}
			ReadOnlyCollection<QueryGenerationResult> readOnlyCollection = null;
			if (!errorContext.HasError)
			{
				readOnlyCollection = QueryGenerator.GenerateAll(new QueryGenerationContext(dataSetPlanningResult.DataShape, scopeTree, annotations, model, schema, errorContext, context.FeatureSwitchProvider, dataSetPlanningResult.ExpressionTable, context.Tracer, context.Options.SuppressModelGrouping, context.CancellationToken), dataSetPlanningResult);
			}
			DataTransformRestorationResult dataTransformRestorationResult = null;
			if (!errorContext.HasError)
			{
				dataTransformRestorationResult = DataTransformRestorer.RestoreTransforms(dataSetPlanningResult.DataShape, dataSetPlanningResult, readOnlyCollection, transformInliningResult);
			}
			DataShapeDefinition dataShapeDefinition = null;
			if (!errorContext.HasError)
			{
				dataShapeDefinition = DsdGenerator.Generate(dataSetPlanningResult.DataShape, dataSourceContext, dataSetPlanningResult, context.FeatureSwitchProvider, readOnlyCollection, errorContext, scopeTree, expressionTable, annotations, dataTransformRestorationResult, context.CancellationToken, context.Options.ApplyTransformsInQuery.Value, disableDictionaryEncoding);
			}
			return dataShapeDefinition;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		private static DataShapeDefinition TranslateWithSuperDaxQueryPattern(DataShapeQueryTranslationContext context, TranslationErrorContext errorContext, DataShapeContext dsContext, ScopeTree scopeTree, ReadOnlyExpressionTable expressionTable, DataSourceContext dataSourceContext, FederatedEntityDataModel model, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, DataTransformInliningResult transformInliningResult, DataShapeQueryTranslationTelemetry telemetryInfo, bool disableDictionaryEncoding)
		{
			BatchDataSetPlanningResult batchDataSetPlanningResult = null;
			if (!errorContext.HasError)
			{
				batchDataSetPlanningResult = BatchDataSetPlanner.DeterminePlans(dsContext, schema, expressionTable, annotations, scopeTree, errorContext, telemetryInfo, context.Options.EnhancedSamplingAdditionalKeyPointsRatio, context.Options.ApplyTransformsInQuery.Value, context.Options.OmitOrderBy, context.Options.GenerateComposableQueryColumnNames, context.FeatureSwitchProvider);
			}
			ReadOnlyCollection<BatchQueryGenerationResult> readOnlyCollection = null;
			if (!errorContext.HasError)
			{
				readOnlyCollection = BatchQueryGenerator.GenerateAll(new BatchQueryGenerationContext(dsContext.DataShape, scopeTree, annotations, model, schema, errorContext, context.FeatureSwitchProvider, batchDataSetPlanningResult.ExpressionTable, context.Tracer, batchDataSetPlanningResult.SortByMeasureExpressions, batchDataSetPlanningResult.GroupDetailMapping, batchDataSetPlanningResult.CalculationExpressionMapping, context.TransformMetadataFactory, telemetryInfo, context.Options.GenerateComposableQuery, context.Options.SuppressModelGrouping, context.CancellationToken), batchDataSetPlanningResult);
			}
			DataTransformRestorationResult dataTransformRestorationResult = null;
			if (!errorContext.HasError)
			{
				dataTransformRestorationResult = DataTransformRestorer.RestoreTransforms(dsContext.DataShape, batchDataSetPlanningResult, readOnlyCollection, transformInliningResult);
			}
			DataShapeDefinition dataShapeDefinition = null;
			if (!errorContext.HasError)
			{
				dataShapeDefinition = BatchDsdGenerator.Generate(dsContext.DataShape, dataSourceContext, batchDataSetPlanningResult, readOnlyCollection, annotations, scopeTree, errorContext, dataTransformRestorationResult, context.CancellationToken, context.Options.ApplyTransformsInQuery.Value, context.Options.GenerateComposableQueryColumnNames, disableDictionaryEncoding, context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema));
			}
			return dataShapeDefinition;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000D520 File Offset: 0x0000B720
		private static DataSourceContext GetDataSourceContext(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape, DataShapeQueryTranslationContext translationContext, TranslationErrorContext errorContext, bool allowMissingEntityDataModel)
		{
			if (translationContext.DataSourceContext == null || (translationContext.DataSourceContext.Model == null && !allowMissingEntityDataModel) || translationContext.DataSourceContext.FederatedConceptualSchema == null)
			{
				errorContext.Register(TranslationMessages.ModelUnavailable(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "DataSourceId", string.Empty));
				return null;
			}
			return translationContext.DataSourceContext;
		}
	}
}
