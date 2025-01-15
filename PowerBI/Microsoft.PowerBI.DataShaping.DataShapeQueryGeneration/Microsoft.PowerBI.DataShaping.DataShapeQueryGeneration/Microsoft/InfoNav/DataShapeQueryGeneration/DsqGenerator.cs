using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ModelParameters;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.Data.PrimitiveValues;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;
using Microsoft.InfoNav.DataShapeQueryGeneration.DSQ;
using Microsoft.InfoNav.DataShapeQueryGeneration.Optimization;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;
using Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Query.Contracts;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000056 RID: 86
	internal class DsqGenerator : IDataShapeGenerator
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0000D320 File Offset: 0x0000B520
		public DsqGenerator(DataShapeGenerationErrorContext errorContext)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D330 File Offset: 0x0000B530
		public DataShapeGenerationResult GenerateDataShapeFromCommand(DataShapeGenerationContext context, SemanticQueryDataShapeCommand command, DataReductionConfiguration dataReductionConfig, DataReductionConfiguration dataReductionConfigForLegacyLimits)
		{
			SemanticQueryDataShapeCommandValidator.Validate(context, this._errorContext, command);
			DsqGenerator.TraceCommand(context.Tracer, command);
			DataShapeGenerationResult dataShapeGenerationResult = null;
			if (this._errorContext.HasError)
			{
				return dataShapeGenerationResult;
			}
			QueryExtensionSchemaContext queryExtensionSchemaContext;
			IFederatedConceptualSchema federatedConceptualSchema = FederatedConceptualSchemaExtensions.PrepareAndBuildFederatedSchema(context, this._errorContext, command, out queryExtensionSchemaContext);
			if (this._errorContext.HasError)
			{
				return dataShapeGenerationResult;
			}
			SemanticQueryDataShapeCommandUpgrader.Upgrade(this._errorContext, federatedConceptualSchema, context.Tracer, command);
			if (this._errorContext.HasError)
			{
				return dataShapeGenerationResult;
			}
			IDateTimeProvider dateTimeProvider = DsqGenerator.CreateDateTimeProvider(command.AnchorTime, context.DateTimeProviderFactoryInstance, this._errorContext);
			if (this._errorContext.HasError)
			{
				return dataShapeGenerationResult;
			}
			DataShapeGenerationInternalContext dataShapeGenerationInternalContext = new DataShapeGenerationInternalContext(context, federatedConceptualSchema, this._errorContext, dateTimeProvider);
			ResolvedSemanticQueryDataShapeCommand resolvedSemanticQueryDataShapeCommand;
			if (!SemanticQueryDataShapeCommandResolver.TryResolve(dataShapeGenerationInternalContext, command, queryExtensionSchemaContext, out resolvedSemanticQueryDataShapeCommand))
			{
				return dataShapeGenerationResult;
			}
			DsqGenerator.UpdateTelemetryFromCommand(dataShapeGenerationInternalContext.Telemetry, resolvedSemanticQueryDataShapeCommand);
			SparklineDataStatistics sparklineDataStatistics;
			if (!ExpressionToExtensionSchemaItemCommandRewriter.TryRewrite(dataShapeGenerationInternalContext, resolvedSemanticQueryDataShapeCommand, this._errorContext, context.ExpressionToExtensionSchemaItemQueryRewriter, out resolvedSemanticQueryDataShapeCommand, out dataShapeGenerationInternalContext, out sparklineDataStatistics))
			{
				return dataShapeGenerationResult;
			}
			federatedConceptualSchema = dataShapeGenerationInternalContext.FederatedConceptualSchema;
			SemanticQueryDataShapeAnnotations semanticQueryDataShapeAnnotations;
			if (!SemanticQueryDataShapeAnnotationAnalyzer.TryCreateAnnotations(resolvedSemanticQueryDataShapeCommand, out semanticQueryDataShapeAnnotations))
			{
				return dataShapeGenerationResult;
			}
			DsqGenerator.UpdateTelemetryFromAnnotations(context.TelemetryInfo, semanticQueryDataShapeAnnotations);
			new ResolvedSemanticQueryDataShapeCommandValidator(context.Tracer, context.FeatureSwitchProvider, this._errorContext).Validate(resolvedSemanticQueryDataShapeCommand, semanticQueryDataShapeAnnotations);
			if (this._errorContext.HasError)
			{
				return dataShapeGenerationResult;
			}
			if (!QueryOptimizer.TryOptimizeQuery(resolvedSemanticQueryDataShapeCommand, dataShapeGenerationInternalContext.FeatureSwitchProvider, semanticQueryDataShapeAnnotations, dataShapeGenerationInternalContext.Telemetry, this._errorContext, dataShapeGenerationInternalContext.Tracer, out resolvedSemanticQueryDataShapeCommand, out semanticQueryDataShapeAnnotations))
			{
				return dataShapeGenerationResult;
			}
			ResolvedQueryDefinition query = resolvedSemanticQueryDataShapeCommand.QueryDataShape.Query;
			QueryExtensionSchemaContext extension = resolvedSemanticQueryDataShapeCommand.Extension;
			DataShapeBuilderContext dataShapeBuilderContext = DsqGenerator.CreateTopLevelDataShapeBuilderContext(query, (extension != null) ? extension.QuerySchemaMapping : null);
			DataShapeBuilder dataShapeBuilder = DsqGenerator.CreateDataShapeBuilder(null, ResolvedCommandDataShapeBuilder.DetermineFilterEmptyGroups(resolvedSemanticQueryDataShapeCommand.QueryDataShape.Binding), dataShapeGenerationInternalContext.TopLevelDataShapeId);
			QueryParameterReferenceContext queryParameterReferenceContext;
			if (!QueryParametersClauseTranslator.TryTranslate(resolvedSemanticQueryDataShapeCommand.QueryDataShape.Query, dataShapeGenerationInternalContext, dataShapeBuilder, out queryParameterReferenceContext))
			{
				return dataShapeGenerationResult;
			}
			DataShapeBinding binding = resolvedSemanticQueryDataShapeCommand.QueryDataShape.Binding;
			IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> readOnlyList = ((binding != null) ? binding.SuppressedJoinPredicatesByName.AsReadOnlyList<DataShapeBindingSuppressedJoinPredicate>() : null);
			DataShapeBinding binding2 = resolvedSemanticQueryDataShapeCommand.QueryDataShape.Binding;
			IReadOnlyList<DataShapeBindingHiddenProjections> readOnlyList2 = ((binding2 != null) ? binding2.HiddenProjections.AsReadOnlyList<DataShapeBindingHiddenProjections>() : null);
			QueryLetReferenceContext empty = QueryLetReferenceContext.Empty;
			if (!QueryLetClauseTranslator.TryTranslate(resolvedSemanticQueryDataShapeCommand.QueryDataShape.Query, dataShapeGenerationInternalContext, semanticQueryDataShapeAnnotations, readOnlyList, readOnlyList2, ResolvedCommandDataShapeBuilder.DetermineFilterEmptyGroups(resolvedSemanticQueryDataShapeCommand.QueryDataShape.Binding), dataShapeBuilderContext, dataShapeBuilder, empty, queryParameterReferenceContext, out empty))
			{
				return dataShapeGenerationResult;
			}
			QuerySourceExpressionReferenceContext querySourceExpressionReferenceContext;
			if (!QueryFromClauseTranslator.TryTranslate(resolvedSemanticQueryDataShapeCommand.QueryDataShape.Query, dataShapeGenerationInternalContext, semanticQueryDataShapeAnnotations, readOnlyList, readOnlyList2, ResolvedCommandDataShapeBuilder.DetermineFilterEmptyGroups(resolvedSemanticQueryDataShapeCommand.QueryDataShape.Binding), dataShapeBuilderContext, dataShapeBuilder, in empty, queryParameterReferenceContext, out querySourceExpressionReferenceContext))
			{
				return dataShapeGenerationResult;
			}
			IntermediateQueryTransformGeneratorResult intermediateQueryTransformGeneratorResult;
			if (!IntermediateQueryTransformGenerator.TryGenerate(this._errorContext, resolvedSemanticQueryDataShapeCommand, querySourceExpressionReferenceContext, queryParameterReferenceContext, semanticQueryDataShapeAnnotations, dataShapeBuilderContext.DataShapeIdGenerator, out intermediateQueryTransformGeneratorResult))
			{
				return dataShapeGenerationResult;
			}
			QueryProjectionGeneratorResult queryProjectionGeneratorResult;
			if (!ResolvedCommandQueryProjectionGenerator.TryGenerate(dataShapeGenerationInternalContext, resolvedSemanticQueryDataShapeCommand, intermediateQueryTransformGeneratorResult, semanticQueryDataShapeAnnotations, querySourceExpressionReferenceContext, queryParameterReferenceContext, in empty, out queryProjectionGeneratorResult, out resolvedSemanticQueryDataShapeCommand, out dataShapeGenerationInternalContext))
			{
				return dataShapeGenerationResult;
			}
			QueryProjections projections = queryProjectionGeneratorResult.GetProjections(resolvedSemanticQueryDataShapeCommand.QueryDataShape);
			if (!IntermediateQueryValidator.Validate(dataShapeGenerationInternalContext, resolvedSemanticQueryDataShapeCommand.QueryDataShape.Query, resolvedSemanticQueryDataShapeCommand.QueryDataShape.Binding, projections, semanticQueryDataShapeAnnotations))
			{
				return dataShapeGenerationResult;
			}
			ParameterMappings parameterMappings = null;
			if (context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.MParameterColumnMapping) && federatedConceptualSchema.GetDefaultMParameterAnnotation().HasMappedParameters)
			{
				DataShapeGenerationErrorContextAdapter dataShapeGenerationErrorContextAdapter = new DataShapeGenerationErrorContextAdapter(this._errorContext, DataShapeGenerationErrorCode.FilterIncompatibleWithParameter, ErrorSource.User);
				parameterMappings = QueryModelParametersCollector.Collect(resolvedSemanticQueryDataShapeCommand.QueryDataShape.Query, dataShapeGenerationErrorContextAdapter);
				if (this._errorContext.HasError)
				{
					return dataShapeGenerationResult;
				}
			}
			DataShape dataShape;
			QueryBindingDescriptor queryBindingDescriptor;
			if (!ResolvedCommandDataShapeBuilder.TryBuild(dataShapeGenerationInternalContext, dataShapeBuilderContext, dataShapeBuilder, resolvedSemanticQueryDataShapeCommand, intermediateQueryTransformGeneratorResult, queryProjectionGeneratorResult, semanticQueryDataShapeAnnotations, dataReductionConfig, dataReductionConfigForLegacyLimits, this._errorContext.Messages, querySourceExpressionReferenceContext, queryParameterReferenceContext, parameterMappings, sparklineDataStatistics, in empty, out dataShape, out queryBindingDescriptor))
			{
				return dataShapeGenerationResult;
			}
			DsqGenerator.TraceGeneratedDataShape(context.Tracer, dataShape);
			IntermediateDataShapeTableSchema intermediateDataShapeTableSchema = null;
			IntermediateDataShapeReferenceSchema intermediateDataShapeReferenceSchema = null;
			if (context.ReturnInternalSchema)
			{
				intermediateDataShapeTableSchema = dataShapeBuilderContext.CreateIntermediateTableSchema(dataShape.Id);
			}
			if (context.TestOnlyReturnInternalDsqReferenceSchema)
			{
				intermediateDataShapeReferenceSchema = dataShapeBuilderContext.CreateReferenceSchema(dataShape.Id);
			}
			return new DataShapeGenerationResult(dataShape, queryBindingDescriptor, this._errorContext, federatedConceptualSchema, intermediateDataShapeTableSchema, intermediateDataShapeReferenceSchema);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D708 File Offset: 0x0000B908
		public DataShapeGenerationResult GenerateDataShapeFromQuery(DataShapeGenerationContext context, ResolvedQueryDefinition resolvedQuery, DataShapeGenerationOptions generationOptions)
		{
			DataShapeGenerationInternalContext dataShapeGenerationInternalContext = new DataShapeGenerationInternalContext(context, context.Model.ToFederatedSchema(), this._errorContext, context.DateTimeProviderFactoryInstance.CreateDateTimeProvider(null));
			if (generationOptions == null)
			{
				generationOptions = DataShapeGenerationOptions.Empty;
			}
			DsqGenerator.UpdateTelemetryFromQuery(context.TelemetryInfo, resolvedQuery);
			resolvedQuery = ResolvedQueryDsqRewriter.RewriteQuery(resolvedQuery);
			SemanticQueryDataShapeAnnotations semanticQueryDataShapeAnnotations;
			if (!SemanticQueryDataShapeAnnotationAnalyzer.TryCreateAnnotations(resolvedQuery, out semanticQueryDataShapeAnnotations))
			{
				return null;
			}
			DsqGenerator.UpdateTelemetryFromAnnotations(context.TelemetryInfo, semanticQueryDataShapeAnnotations);
			TransformQueryValidator.ValidateQuery(dataShapeGenerationInternalContext.ErrorContext, resolvedQuery, context.FeatureSwitchProvider, semanticQueryDataShapeAnnotations);
			VisualShapeValidator.ValidateQuery(dataShapeGenerationInternalContext.ErrorContext, resolvedQuery, semanticQueryDataShapeAnnotations);
			if (dataShapeGenerationInternalContext.ErrorContext.HasError)
			{
				return null;
			}
			if (!QueryOptimizer.TryOptimizeQuery(resolvedQuery, null, dataShapeGenerationInternalContext.FeatureSwitchProvider, semanticQueryDataShapeAnnotations, dataShapeGenerationInternalContext.Telemetry, this._errorContext, dataShapeGenerationInternalContext.Tracer, out resolvedQuery, out semanticQueryDataShapeAnnotations))
			{
				return null;
			}
			DataShapeBuilderContext dataShapeBuilderContext = DsqGenerator.CreateTopLevelDataShapeBuilderContext(resolvedQuery, null);
			DataShapeBuilder dataShapeBuilder = DsqGenerator.CreateDataShapeBuilder(null, true, dataShapeGenerationInternalContext.TopLevelDataShapeId);
			QueryParameterReferenceContext queryParameterReferenceContext;
			if (!QueryParametersClauseTranslator.TryTranslate(resolvedQuery, dataShapeGenerationInternalContext, dataShapeBuilder, out queryParameterReferenceContext))
			{
				return null;
			}
			QueryLetReferenceContext empty = QueryLetReferenceContext.Empty;
			if (!QueryLetClauseTranslator.TryTranslate(resolvedQuery, dataShapeGenerationInternalContext, semanticQueryDataShapeAnnotations, null, null, true, dataShapeBuilderContext, dataShapeBuilder, empty, queryParameterReferenceContext, out empty))
			{
				return null;
			}
			QuerySourceExpressionReferenceContext querySourceExpressionReferenceContext;
			if (!QueryFromClauseTranslator.TryTranslate(resolvedQuery, dataShapeGenerationInternalContext, semanticQueryDataShapeAnnotations, null, null, true, dataShapeBuilderContext, dataShapeBuilder, in empty, queryParameterReferenceContext, out querySourceExpressionReferenceContext))
			{
				return null;
			}
			IntermediateQueryTransformContext intermediateQueryTransformContext;
			if (!IntermediateQueryTransformGenerator.TryGenerate(dataShapeGenerationInternalContext.ErrorContext, resolvedQuery.Transform, querySourceExpressionReferenceContext, queryParameterReferenceContext, resolvedQuery.Name, semanticQueryDataShapeAnnotations, dataShapeBuilderContext.DataShapeIdGenerator, out intermediateQueryTransformContext))
			{
				return null;
			}
			DataShape dataShape;
			if (!DsqGenerator.TryGenerateDataShapeCoreFromQuery(dataShapeGenerationInternalContext, dataShapeBuilderContext, dataShapeBuilder, resolvedQuery, intermediateQueryTransformContext, semanticQueryDataShapeAnnotations, generationOptions, querySourceExpressionReferenceContext, queryParameterReferenceContext, null, null, ParameterMappings.Empty, SparklineDataStatistics.Empty, in empty, out dataShape))
			{
				return null;
			}
			DsqGenerator.TraceGeneratedDataShape(dataShapeGenerationInternalContext.Tracer, dataShape);
			QueryBindingDescriptor queryBindingDescriptor = dataShapeBuilderContext.CreateDescriptor(null, dataShapeGenerationInternalContext.FeatureSwitchProvider);
			IntermediateDataShapeTableSchema intermediateDataShapeTableSchema = null;
			if (context.ReturnInternalSchema)
			{
				intermediateDataShapeTableSchema = dataShapeBuilderContext.CreateIntermediateTableSchema(dataShape.Id);
			}
			return new DataShapeGenerationResult(dataShape, queryBindingDescriptor, dataShapeGenerationInternalContext.ErrorContext, dataShapeGenerationInternalContext.FederatedConceptualSchema, intermediateDataShapeTableSchema, null);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000D8CC File Offset: 0x0000BACC
		internal static bool TryGenerateSubqueryDataShapeForRegrouping(DataShapeGenerationInternalContext context, DataShapeBuilderContext parentBuilderContext, ResolvedQueryDefinition resolvedQuery, SemanticQueryDataShapeAnnotations annotations, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections, bool filterEmptyGroups, in QueryLetReferenceContext letContext, QueryParameterReferenceContext parameterRefContext, out DataShape dataShape, out IntermediateDataShapeTableSchema schema, out DataShapeBuilderContext builderContext, out IntermediateQueryTransformContext transformContext)
		{
			return DsqGenerator.TryGenerateSubqueryDataShape(context, parentBuilderContext, resolvedQuery, annotations, false, suppressedJoinPredicatesByName, hiddenProjections, filterEmptyGroups, letContext, parameterRefContext, out dataShape, out schema, out builderContext, out transformContext, new bool?(true), true);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000D904 File Offset: 0x0000BB04
		internal static bool TryGenerateSubqueryDataShapeForFiltering(DataShapeGenerationInternalContext context, DataShapeBuilderContext parentBuilderContext, ResolvedQueryDefinition resolvedQuery, SemanticQueryDataShapeAnnotations annotations, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections, in QueryLetReferenceContext letContext, QueryParameterReferenceContext parameterRefContext, bool? contextOnly, bool isIndependent, out DataShape dataShape, out IntermediateDataShapeTableSchema schema, out DataShapeBuilderContext builderContext, out IntermediateQueryTransformContext transformContext)
		{
			bool flag = true;
			bool flag2 = true;
			return DsqGenerator.TryGenerateSubqueryDataShape(context, parentBuilderContext, resolvedQuery, annotations, flag, suppressedJoinPredicatesByName, hiddenProjections, flag2, letContext, parameterRefContext, out dataShape, out schema, out builderContext, out transformContext, contextOnly, isIndependent);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000D93C File Offset: 0x0000BB3C
		private static bool TryGenerateSubqueryDataShape(DataShapeGenerationInternalContext context, DataShapeBuilderContext parentBuilderContext, ResolvedQueryDefinition resolvedQuery, SemanticQueryDataShapeAnnotations annotations, bool suppressAutomaticGroupSorts, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections, bool filterEmptyGroups, QueryLetReferenceContext letContext, QueryParameterReferenceContext parameterRefContext, out DataShape dataShape, out IntermediateDataShapeTableSchema schema, out DataShapeBuilderContext builderContext, out IntermediateQueryTransformContext transformContext, bool? contextOnly = null, bool isIndependent = false)
		{
			transformContext = null;
			DsqGenerator.UpdateTelemetryFromSubquery(context.Telemetry, resolvedQuery);
			builderContext = parentBuilderContext.CreateSubqueryBuilderContext(resolvedQuery.Select.Count, resolvedQuery.Select);
			DataShapeBuilder dataShapeBuilder = DsqGenerator.CreateDataShapeBuilder(contextOnly, filterEmptyGroups, parentBuilderContext.CreateSubqueryDataShapeId());
			if (!QueryLetClauseTranslator.TryTranslate(resolvedQuery, context, annotations, suppressedJoinPredicatesByName, hiddenProjections, filterEmptyGroups, builderContext, dataShapeBuilder, letContext, parameterRefContext, out letContext))
			{
				dataShape = null;
				schema = null;
				builderContext = null;
				return false;
			}
			QuerySourceExpressionReferenceContext querySourceExpressionReferenceContext;
			if (!QueryFromClauseTranslator.TryTranslate(resolvedQuery, context, annotations, suppressedJoinPredicatesByName, hiddenProjections, filterEmptyGroups, builderContext, dataShapeBuilder, in letContext, parameterRefContext, out querySourceExpressionReferenceContext))
			{
				dataShape = null;
				schema = null;
				builderContext = null;
				return false;
			}
			if (!IntermediateQueryTransformGenerator.TryGenerate(context.ErrorContext, resolvedQuery.Transform, querySourceExpressionReferenceContext, parameterRefContext, resolvedQuery.Name, annotations, parentBuilderContext.DataShapeIdGenerator, out transformContext))
			{
				dataShape = null;
				schema = null;
				builderContext = null;
				return false;
			}
			DataShapeGenerationOptions dataShapeGenerationOptions = new DataShapeGenerationOptions(null, false, false, suppressAutomaticGroupSorts, false, true, AllowedExpressionContent.SubquerySelect);
			if (!DsqGenerator.TryGenerateDataShapeCoreFromQuery(context, builderContext, dataShapeBuilder, resolvedQuery, transformContext, annotations, dataShapeGenerationOptions, querySourceExpressionReferenceContext, parameterRefContext, suppressedJoinPredicatesByName, hiddenProjections, ParameterMappings.Empty, SparklineDataStatistics.Empty, in letContext, out dataShape))
			{
				schema = null;
				return false;
			}
			schema = builderContext.CreateIntermediateTableSchema(dataShape.Id);
			dataShape.IsIndependent = isIndependent;
			return true;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000DA64 File Offset: 0x0000BC64
		private static bool TryGenerateDataShapeCoreFromQuery(DataShapeGenerationInternalContext context, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, ResolvedQueryDefinition resolvedQuery, IntermediateQueryTransformContext transformContext, SemanticQueryDataShapeAnnotations annotations, DataShapeGenerationOptions generationOptions, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections, ParameterMappings parameterMappings, SparklineDataStatistics sparklineDataStatistics, in QueryLetReferenceContext letContext, out DataShape dataShape)
		{
			QueryTranslationContext queryTranslationContext = new QueryTranslationContext(context, resolvedQuery, null, null, transformContext, null, annotations, sourceRefContext, parameterRefContext, suppressedJoinPredicatesByName, hiddenProjections, generationOptions.SuppressModelGrouping, in letContext);
			QueryProjections queryProjections;
			if (!QueryDefinitionProjectionGenerator.TryRun(queryTranslationContext, generationOptions, generationOptions.TrackGroupKeysAndSortKeysForReferencing, out queryProjections))
			{
				dataShape = null;
				return false;
			}
			if (!IntermediateQueryValidator.Validate(context, resolvedQuery, null, queryProjections, annotations))
			{
				dataShape = null;
				return false;
			}
			if (!IntermediateQueryTransformTrimmer.TryTrim(transformContext, generationOptions, resolvedQuery.Select, queryTranslationContext.Expressions))
			{
				dataShape = null;
				return false;
			}
			if (!SemanticQueryDataShapeBuilder.TryBuildDataShape(dataShapeBuilderContext, queryTranslationContext, queryProjections, dataShapeBuilder, transformContext.Transforms, sparklineDataStatistics, parameterMappings))
			{
				dataShape = null;
				return false;
			}
			dataShape = dataShapeBuilder.Parent();
			return true;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000DB08 File Offset: 0x0000BD08
		private static T ExecuteInTopLevelTryCatch<T>(Func<T> func, DataShapeGenerationContext context, DataShapeGenerationErrorContext errorContext)
		{
			T t;
			try
			{
				t = func();
			}
			catch (DataShapeEngineException ex)
			{
				string text = "DataShapeEngineException exception in DataShapeQueryGeneration";
				context.Tracer.TraceSanitizedError(ex, text);
				throw;
			}
			catch (Exception ex2) when (!ex2.IsStoppingException())
			{
				string text2 = "Unexpected exception in DataShapeQueryGeneration";
				context.Tracer.TraceSanitizedError(ex2, text2);
				context.Dumper.Dump(text2, ex2);
				throw DataShapeGenerationException.Create(ex2);
			}
			if (errorContext.HasError)
			{
				throw DataShapeGenerationException.Create(errorContext);
			}
			if (t == null)
			{
				throw DataShapeGenerationException.Create(new NullReferenceException("DataShapeGenerationResult should have value if ErrorContext has no error."));
			}
			return t;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		private static DataShapeBuilder CreateDataShapeBuilder(bool? contextOnly, bool filterEmptyGroups, string dataShapeId)
		{
			string text = null;
			bool? flag = contextOnly;
			return DataShapeBuilder.With(dataShapeId, text, filterEmptyGroups, (flag != null) ? flag.GetValueOrDefault() : null, DataShapeUsage.Query);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		private static DataShapeBuilderContext CreateTopLevelDataShapeBuilderContext(ResolvedQueryDefinition resolvedQuery, QuerySchemaMapping querySchemaMapping)
		{
			return new DataShapeBuilderContext(resolvedQuery.Select.Count, querySchemaMapping, resolvedQuery.Select);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		private static IDateTimeProvider CreateDateTimeProvider(string anchorTime, IDateTimeProviderFactory dateTimeProviderFactory, DataShapeGenerationErrorContext errorContext)
		{
			DateTime? dateTime = null;
			if (anchorTime != null)
			{
				PrimitiveValue primitiveValue = null;
				if (PrimitiveValueEncoding.TryParseTypeEncodedString(anchorTime, out primitiveValue))
				{
					object valueAsObject = primitiveValue.GetValueAsObject();
					if (!(valueAsObject is DateTime))
					{
						errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedAnchorTime(EngineMessageSeverity.Error));
					}
					dateTime = new DateTime?((DateTime)valueAsObject);
				}
				else
				{
					errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedAnchorTime(EngineMessageSeverity.Error));
				}
			}
			return dateTimeProviderFactory.CreateDateTimeProvider(dateTime);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000DC70 File Offset: 0x0000BE70
		private static void UpdateTelemetryFromCommand(DataShapeGenerationTelemetry telemetry, ResolvedSemanticQueryDataShapeCommand command)
		{
			if (command.QueryDataShape == null)
			{
				return;
			}
			DsqGenerator.UpdateTelemetryFromQuery(telemetry, command.QueryDataShape.Query);
			DataShapeBinding binding = command.QueryDataShape.Binding;
			int? num;
			if (binding == null)
			{
				num = null;
			}
			else
			{
				DataShapeBindingAxis primary = binding.Primary;
				if (primary == null)
				{
					num = null;
				}
				else
				{
					IList<DataShapeBindingAxisSynchronizedGroupingBlock> synchronization = primary.Synchronization;
					num = ((synchronization != null) ? new int?(synchronization.Count) : null);
				}
			}
			int? num2 = num;
			int valueOrDefault = num2.GetValueOrDefault();
			int? num3;
			if (binding == null)
			{
				num3 = null;
			}
			else
			{
				DataShapeBindingAxis secondary = binding.Secondary;
				if (secondary == null)
				{
					num3 = null;
				}
				else
				{
					IList<DataShapeBindingAxisSynchronizedGroupingBlock> synchronization2 = secondary.Synchronization;
					num3 = ((synchronization2 != null) ? new int?(synchronization2.Count) : null);
				}
			}
			num2 = num3;
			int valueOrDefault2 = num2.GetValueOrDefault();
			telemetry.NumGroupSynchronizationBlocks = valueOrDefault + valueOrDefault2;
			int? num4;
			if (binding == null)
			{
				num4 = null;
			}
			else
			{
				IList<DataShapeBindingHiddenProjections> hiddenProjections = binding.HiddenProjections;
				num4 = ((hiddenProjections != null) ? new int?(hiddenProjections.Count) : null);
			}
			num2 = num4;
			telemetry.NumHiddenProjections = num2.GetValueOrDefault();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		private static void UpdateTelemetryFromQuery(DataShapeGenerationTelemetry telemetry, ResolvedQueryDefinition query)
		{
			if (query == null)
			{
				return;
			}
			if (!query.Select.IsNullOrEmpty<ResolvedQuerySelect>())
			{
				telemetry.NumSelects = new int?(query.Select.Count);
			}
			if (!query.GroupBy.IsNullOrEmpty<ResolvedQueryExpression>())
			{
				telemetry.NumGroupBy = new int?(query.GroupBy.Count);
			}
			if (!query.Let.IsNullOrEmpty<ResolvedQueryLetBinding>())
			{
				telemetry.NumLets += query.Let.Count;
			}
			if (!query.Parameters.IsNullOrEmpty<ResolvedQueryParameterDeclaration>())
			{
				telemetry.NumQueryParameterDeclarations += query.Parameters.Count;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000DE1D File Offset: 0x0000C01D
		private static void UpdateTelemetryFromSubquery(DataShapeGenerationTelemetry telemetry, ResolvedQueryDefinition subquery)
		{
			if (!subquery.Let.IsNullOrEmpty<ResolvedQueryLetBinding>())
			{
				telemetry.NumLets += subquery.Let.Count;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000DE44 File Offset: 0x0000C044
		private static void UpdateTelemetryFromAnnotations(DataShapeGenerationTelemetry telemetry, SemanticQueryDataShapeAnnotations annotations)
		{
			telemetry.NumSubqueries = annotations.SubqueryCount;
			IntermediateTableUsage consolidatedExpressionSourceUsage = annotations.ConsolidatedExpressionSourceUsage;
			telemetry.HasSubqueryFiltering = consolidatedExpressionSourceUsage.HasFlag(IntermediateTableUsage.Filtering);
			telemetry.HasSubqueryRegrouping = consolidatedExpressionSourceUsage.HasFlag(IntermediateTableUsage.Regrouping);
			telemetry.NumVisualCalcExpressions = annotations.VisualCalculationExpressionCountByQueryName.Sum((KeyValuePair<string, int> kvp) => kvp.Value);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		private static void TraceCommand(ITracer tracer, SemanticQueryDataShapeCommand command)
		{
			string text = JsonConvert.SerializeObject(command);
			string text2 = text.MarkAsCustomerContent();
			tracer.SanitizedTrace(TraceLevel.Info, "Generating DataShape for: (length: {0}) {1}", text.Length, text2);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000DEF7 File Offset: 0x0000C0F7
		private static void TraceGeneratedDataShape(ITracer tracer, DataShape dataShape)
		{
			tracer.SanitizedTrace(TraceLevel.Info, "Generated DataShape: {0}", new string[] { DataShapeQueryWriter.ToJson(dataShape).MarkAsCustomerContent() });
		}

		// Token: 0x04000227 RID: 551
		private readonly DataShapeGenerationErrorContext _errorContext;
	}
}
