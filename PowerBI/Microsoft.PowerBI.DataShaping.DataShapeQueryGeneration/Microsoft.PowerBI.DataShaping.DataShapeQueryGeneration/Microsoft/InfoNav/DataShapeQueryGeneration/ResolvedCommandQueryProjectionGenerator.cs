using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C5 RID: 197
	internal sealed class ResolvedCommandQueryProjectionGenerator
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0001B538 File Offset: 0x00019738
		private ResolvedCommandQueryProjectionGenerator(DataShapeGenerationInternalContext context, IntermediateQueryTransformGeneratorResult transformResult, SemanticQueryDataShapeAnnotations annotations, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, in QueryLetReferenceContext letContext)
		{
			this._context = context;
			this._transformResult = transformResult;
			this._mapping = new Dictionary<ResolvedSemanticQueryDataShape, QueryProjections>();
			this._annotations = annotations;
			this._sourceRefContext = sourceRefContext;
			this._parameterRefContext = parameterRefContext;
			this._letContext = letContext;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001B588 File Offset: 0x00019788
		internal static bool TryGenerate(DataShapeGenerationInternalContext context, ResolvedSemanticQueryDataShapeCommand command, IntermediateQueryTransformGeneratorResult transformResult, SemanticQueryDataShapeAnnotations annotations, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, in QueryLetReferenceContext letContext, out QueryProjectionGeneratorResult result, out ResolvedSemanticQueryDataShapeCommand newCommand, out DataShapeGenerationInternalContext newContext)
		{
			IFederatedConceptualSchema federatedConceptualSchema = context.FederatedConceptualSchema;
			DataShapeGenerationErrorContext errorContext = context.ErrorContext;
			QueryExtensionSchemaContext extension = command.Extension;
			QuerySchemaExtender querySchemaExtender = QuerySchemaExtender.Create(federatedConceptualSchema, errorContext, (extension != null) ? extension.NamingContext : null, null);
			ResolvedCommandQueryProjectionGenerator resolvedCommandQueryProjectionGenerator = new ResolvedCommandQueryProjectionGenerator(context, transformResult, annotations, sourceRefContext, parameterRefContext, in letContext);
			resolvedCommandQueryProjectionGenerator.Visit(command.QueryDataShape);
			global::System.ValueTuple<QueryExtensionSchemaContext, IFederatedConceptualSchema> valueTuple = querySchemaExtender.Extend(command.Extension, context.FederatedConceptualSchema);
			newCommand = command;
			if (valueTuple.Item1 != command.Extension)
			{
				newCommand = command.Clone(null, valueTuple.Item1);
			}
			newContext = context;
			if (valueTuple.Item2 != context.FederatedConceptualSchema)
			{
				newContext = context.Clone(valueTuple.Item2);
			}
			result = new QueryProjectionGeneratorResult(resolvedCommandQueryProjectionGenerator._mapping);
			return !resolvedCommandQueryProjectionGenerator._hasError;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001B640 File Offset: 0x00019840
		private void Visit(ResolvedSemanticQueryDataShape queryDataShape)
		{
			DataShapeGenerationInternalContext context = this._context;
			ResolvedQueryDefinition query = queryDataShape.Query;
			DataShapeBinding binding = queryDataShape.Binding;
			ResolvedDataReduction resolvedDataReduction = queryDataShape.ResolvedDataReduction;
			IntermediateQueryTransformContext transformContext = this._transformResult.GetTransformContext(queryDataShape);
			int? maxRowCount = queryDataShape.MaxRowCount;
			SemanticQueryDataShapeAnnotations annotations = this._annotations;
			QuerySourceExpressionReferenceContext sourceRefContext = this._sourceRefContext;
			QueryParameterReferenceContext parameterRefContext = this._parameterRefContext;
			DataShapeBinding binding2 = queryDataShape.Binding;
			IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> readOnlyList = ((binding2 != null) ? binding2.SuppressedJoinPredicatesByName.ToReadOnlyCollection<DataShapeBindingSuppressedJoinPredicate>() : null);
			DataShapeBinding binding3 = queryDataShape.Binding;
			QueryProjections queryProjections;
			if (!SemanticQueryProjectionGenerator.TryRun(new QueryTranslationContext(context, query, binding, resolvedDataReduction, transformContext, maxRowCount, annotations, sourceRefContext, parameterRefContext, readOnlyList, (binding3 != null) ? binding3.HiddenProjections.ToReadOnlyCollection<DataShapeBindingHiddenProjections>() : null, false, in this._letContext), out queryProjections))
			{
				this._hasError = true;
				return;
			}
			this._mapping.Add(queryDataShape, queryProjections);
		}

		// Token: 0x040003C2 RID: 962
		private readonly DataShapeGenerationInternalContext _context;

		// Token: 0x040003C3 RID: 963
		private readonly IntermediateQueryTransformGeneratorResult _transformResult;

		// Token: 0x040003C4 RID: 964
		private readonly Dictionary<ResolvedSemanticQueryDataShape, QueryProjections> _mapping;

		// Token: 0x040003C5 RID: 965
		private readonly SemanticQueryDataShapeAnnotations _annotations;

		// Token: 0x040003C6 RID: 966
		private readonly QuerySourceExpressionReferenceContext _sourceRefContext;

		// Token: 0x040003C7 RID: 967
		private readonly QueryParameterReferenceContext _parameterRefContext;

		// Token: 0x040003C8 RID: 968
		private readonly QueryLetReferenceContext _letContext;

		// Token: 0x040003C9 RID: 969
		private bool _hasError;
	}
}
