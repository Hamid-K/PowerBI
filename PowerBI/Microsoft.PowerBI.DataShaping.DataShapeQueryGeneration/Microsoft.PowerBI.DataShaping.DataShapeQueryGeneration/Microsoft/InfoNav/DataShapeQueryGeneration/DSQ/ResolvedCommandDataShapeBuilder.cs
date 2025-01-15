using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ModelParameters;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x02000110 RID: 272
	internal sealed class ResolvedCommandDataShapeBuilder
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x000238F0 File Offset: 0x00021AF0
		private ResolvedCommandDataShapeBuilder(DataShapeGenerationInternalContext context, IntermediateQueryTransformGeneratorResult transformResult, QueryProjectionGeneratorResult projectionsResult, SemanticQueryDataShapeAnnotations annotations)
		{
			this._context = context;
			this._transformResult = transformResult;
			this._projectionsResult = projectionsResult;
			this._annotations = annotations;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00023918 File Offset: 0x00021B18
		internal static bool TryBuild(DataShapeGenerationInternalContext context, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, ResolvedSemanticQueryDataShapeCommand command, IntermediateQueryTransformGeneratorResult transformResult, QueryProjectionGeneratorResult projectionsResult, SemanticQueryDataShapeAnnotations annotations, DataReductionConfiguration dataReductionConfig, DataReductionConfiguration dataReductionConfigForLegacyLimits, IReadOnlyList<DataShapeGenerationMessage> messages, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, ParameterMappings parameterMappings, SparklineDataStatistics sparklineStatistics, in QueryLetReferenceContext letContext, out DataShape dataShape, out QueryBindingDescriptor descriptor)
		{
			return new ResolvedCommandDataShapeBuilder(context, transformResult, projectionsResult, annotations).TryBuild(command, dataReductionConfig, dataReductionConfigForLegacyLimits, messages, sourceRefContext, parameterRefContext, dataShapeBuilderContext, dataShapeBuilder, parameterMappings, sparklineStatistics, in letContext, out dataShape, out descriptor);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00023950 File Offset: 0x00021B50
		private bool TryBuild(ResolvedSemanticQueryDataShapeCommand command, DataReductionConfiguration dataReductionConfig, DataReductionConfiguration dataReductionConfigForLegacyLimits, IReadOnlyList<DataShapeGenerationMessage> messages, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, ParameterMappings parameterMappings, SparklineDataStatistics sparklineStatistics, in QueryLetReferenceContext letContext, out DataShape dataShape, out QueryBindingDescriptor descriptor)
		{
			if (command.QueryDataShape.ResolvedDataReduction != null)
			{
				if (dataReductionConfig == null)
				{
					dataReductionConfig = DataReductionConfiguration.Default;
				}
				if (dataReductionConfigForLegacyLimits == null)
				{
					dataReductionConfigForLegacyLimits = DataReductionConfiguration.DefaultForLegacyLimits;
				}
			}
			return this.TryBuild(command.QueryDataShape, command.Extension, command.DataSourceVariables, dataReductionConfig, dataReductionConfigForLegacyLimits, messages, sourceRefContext, parameterRefContext, dataShapeBuilderContext, dataShapeBuilder, parameterMappings, sparklineStatistics, in letContext, out dataShape, out descriptor);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000239AC File Offset: 0x00021BAC
		private bool TryBuild(ResolvedSemanticQueryDataShape queryDataShape, QueryExtensionSchemaContext extension, string dataSourceVariables, DataReductionConfiguration dataReductionConfig, DataReductionConfiguration dataReductionConfigForLegacyLimits, IReadOnlyList<DataShapeGenerationMessage> messages, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, ParameterMappings parameterMappings, SparklineDataStatistics sparklineStatistics, in QueryLetReferenceContext letContext, out DataShape dataShape, out QueryBindingDescriptor descriptor)
		{
			DataShapeGenerationInternalContext context = this._context;
			ResolvedQueryDefinition query = queryDataShape.Query;
			DataShapeBinding binding = queryDataShape.Binding;
			ResolvedDataReduction resolvedDataReduction = queryDataShape.ResolvedDataReduction;
			IntermediateQueryTransformContext transformContext = this._transformResult.GetTransformContext(queryDataShape);
			int? maxRowCount = queryDataShape.MaxRowCount;
			SemanticQueryDataShapeAnnotations annotations = this._annotations;
			DataShapeBinding binding2 = queryDataShape.Binding;
			IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> readOnlyList = ((binding2 != null) ? binding2.SuppressedJoinPredicatesByName.AsReadOnlyList<DataShapeBindingSuppressedJoinPredicate>() : null);
			DataShapeBinding binding3 = queryDataShape.Binding;
			QueryTranslationContext queryTranslationContext = new QueryTranslationContext(context, query, binding, resolvedDataReduction, transformContext, maxRowCount, annotations, sourceRefContext, parameterRefContext, readOnlyList, (binding3 != null) ? binding3.HiddenProjections.AsReadOnlyList<DataShapeBindingHiddenProjections>() : null, false, in letContext);
			bool flag = false;
			IntermediateDataReduction intermediateDataReduction;
			if (!SemanticQueryDataShapeBuilder.TryBuildDataShape(dataShapeBuilderContext, queryTranslationContext, this._projectionsResult.GetProjections(queryDataShape), dataShapeBuilder, flag, queryTranslationContext.TransformContext.Transforms, extension, dataSourceVariables, dataReductionConfig, dataReductionConfigForLegacyLimits, parameterMappings, sparklineStatistics, out intermediateDataReduction))
			{
				dataShape = null;
				descriptor = null;
				return false;
			}
			descriptor = dataShapeBuilderContext.CreateDescriptor(intermediateDataReduction, queryTranslationContext.SharedContext.FeatureSwitchProvider);
			ResolvedCommandDataShapeBuilder.BuildMessages(dataShapeBuilder, messages);
			dataShape = dataShapeBuilder.Parent();
			return true;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00023A8A File Offset: 0x00021C8A
		internal static bool DetermineFilterEmptyGroups(DataShapeBinding binding)
		{
			return binding == null || !binding.IncludeEmptyGroups;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00023AA0 File Offset: 0x00021CA0
		private static void BuildMessages(DataShapeBuilder dataShapeBuilder, IReadOnlyList<DataShapeGenerationMessage> messages)
		{
			if (messages.IsNullOrEmpty<DataShapeGenerationMessage>())
			{
				return;
			}
			foreach (DataShapeGenerationMessage dataShapeGenerationMessage in messages)
			{
				dataShapeBuilder.WithMessage(dataShapeGenerationMessage.GetErrorCodeString(), dataShapeGenerationMessage.Severity.ToString(), dataShapeGenerationMessage.Message);
			}
		}

		// Token: 0x04000488 RID: 1160
		private readonly DataShapeGenerationInternalContext _context;

		// Token: 0x04000489 RID: 1161
		private readonly IntermediateQueryTransformGeneratorResult _transformResult;

		// Token: 0x0400048A RID: 1162
		private readonly QueryProjectionGeneratorResult _projectionsResult;

		// Token: 0x0400048B RID: 1163
		private readonly SemanticQueryDataShapeAnnotations _annotations;
	}
}
