using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000086 RID: 134
	internal sealed class DsqTableExpressionGenerator
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x000134B8 File Offset: 0x000116B8
		internal DsqTableExpressionGenerator(DataShapeGenerationInternalContext internalContext, SemanticQueryDataShapeAnnotations annotations, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections, bool filterEmptyGroups, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, QueryParameterReferenceContext parameterRefContext)
		{
			this._internalContext = internalContext;
			this._annotations = annotations;
			this._suppressedJoinPredicatesByName = suppressedJoinPredicatesByName;
			this._hiddenProjections = hiddenProjections;
			this._filterEmptyGroups = filterEmptyGroups;
			this._dataShapeBuilderContext = dataShapeBuilderContext;
			this._dataShapeBuilder = dataShapeBuilder;
			this._parameterRefContext = parameterRefContext;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013508 File Offset: 0x00011708
		internal bool TryGenerate(ResolvedQueryExpression tableExpression, IntermediateTableUsage usage, in QueryLetReferenceContext letContext, string parentObjectType, string parentObjectName, out IIntermediateTableSchema schema)
		{
			ResolvedQuerySubqueryExpression resolvedQuerySubqueryExpression = tableExpression as ResolvedQuerySubqueryExpression;
			if (resolvedQuerySubqueryExpression != null)
			{
				return this.TryTranslateSubqueryExpression(resolvedQuerySubqueryExpression, usage, in letContext, parentObjectType, parentObjectName, out schema);
			}
			ResolvedQueryLetRefExpression resolvedQueryLetRefExpression = tableExpression as ResolvedQueryLetRefExpression;
			if (resolvedQueryLetRefExpression != null)
			{
				return this.TryTranslateLetRefExpression(resolvedQueryLetRefExpression, in letContext, out schema);
			}
			ResolvedQueryParameterRefExpression resolvedQueryParameterRefExpression = tableExpression as ResolvedQueryParameterRefExpression;
			if (resolvedQueryParameterRefExpression == null)
			{
				this._internalContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedTableExpression(EngineMessageSeverity.Error, parentObjectType, parentObjectName, tableExpression));
				schema = null;
				return false;
			}
			return this.TryTranslateParameterRefExpression(resolvedQueryParameterRefExpression, out schema);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001357C File Offset: 0x0001177C
		private bool TryTranslateSubqueryExpression(ResolvedQuerySubqueryExpression subqueryExpr, IntermediateTableUsage usage, in QueryLetReferenceContext letContext, string parentObjectType, string parentObjectName, out IIntermediateTableSchema sourceSchema)
		{
			DataShape dataShape;
			IntermediateDataShapeTableSchema intermediateDataShapeTableSchema;
			if (usage == IntermediateTableUsage.Regrouping)
			{
				DataShapeBuilderContext dataShapeBuilderContext;
				IntermediateQueryTransformContext intermediateQueryTransformContext;
				if (!DsqGenerator.TryGenerateSubqueryDataShapeForRegrouping(this._internalContext, this._dataShapeBuilderContext, subqueryExpr.Subquery, this._annotations, this._suppressedJoinPredicatesByName, this._hiddenProjections, this._filterEmptyGroups, in letContext, this._parameterRefContext, out dataShape, out intermediateDataShapeTableSchema, out dataShapeBuilderContext, out intermediateQueryTransformContext))
				{
					sourceSchema = null;
					return false;
				}
			}
			else
			{
				if (usage != IntermediateTableUsage.Filtering)
				{
					this._internalContext.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedSubqueryUsage(EngineMessageSeverity.Error, parentObjectType, parentObjectName));
					sourceSchema = null;
					return false;
				}
				DataShapeBuilderContext dataShapeBuilderContext;
				IntermediateQueryTransformContext intermediateQueryTransformContext;
				if (!DsqGenerator.TryGenerateSubqueryDataShapeForFiltering(this._internalContext, this._dataShapeBuilderContext, subqueryExpr.Subquery, this._annotations, this._suppressedJoinPredicatesByName, this._hiddenProjections, in letContext, this._parameterRefContext, new bool?(true), true, out dataShape, out intermediateDataShapeTableSchema, out dataShapeBuilderContext, out intermediateQueryTransformContext))
				{
					sourceSchema = null;
					return false;
				}
			}
			this._dataShapeBuilder.WithDataShape(dataShape);
			sourceSchema = intermediateDataShapeTableSchema;
			return true;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00013650 File Offset: 0x00011850
		private bool TryTranslateLetRefExpression(ResolvedQueryLetRefExpression letRefExpr, in QueryLetReferenceContext letContext, out IIntermediateTableSchema sourceSchema)
		{
			QueryLetReferenceContext queryLetReferenceContext = letContext;
			if (queryLetReferenceContext.TryGetLetSchema(letRefExpr.Binding.Name, out sourceSchema))
			{
				return true;
			}
			this._internalContext.ErrorContext.Register(DataShapeGenerationMessages.CouldNotResolveLetReference(EngineMessageSeverity.Error, letRefExpr.Binding.Name));
			sourceSchema = null;
			return false;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000136A0 File Offset: 0x000118A0
		private bool TryTranslateParameterRefExpression(ResolvedQueryParameterRefExpression parameterRefExpression, out IIntermediateTableSchema schema)
		{
			IntermediateQueryParameter intermediateQueryParameter;
			if (!this._parameterRefContext.TryGetParameter(parameterRefExpression, out intermediateQueryParameter))
			{
				this._internalContext.ErrorContext.Register(DataShapeGenerationMessages.CouldNotResolveQueryParameterReference(EngineMessageSeverity.Error, parameterRefExpression.Declaration.Name));
				schema = null;
				return false;
			}
			ConceptualTableType conceptualTableType = intermediateQueryParameter.Type as ConceptualTableType;
			if (conceptualTableType != null)
			{
				schema = new IntermediateExpressionTableSchema(intermediateQueryParameter.Name.QueryParameter(), conceptualTableType);
				return true;
			}
			this._internalContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidQueryParameterReferenceType(EngineMessageSeverity.Error, parameterRefExpression.Declaration.Name));
			schema = null;
			return false;
		}

		// Token: 0x040002E7 RID: 743
		private readonly DataShapeGenerationInternalContext _internalContext;

		// Token: 0x040002E8 RID: 744
		private readonly SemanticQueryDataShapeAnnotations _annotations;

		// Token: 0x040002E9 RID: 745
		private readonly IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> _suppressedJoinPredicatesByName;

		// Token: 0x040002EA RID: 746
		private readonly IReadOnlyList<DataShapeBindingHiddenProjections> _hiddenProjections;

		// Token: 0x040002EB RID: 747
		private readonly bool _filterEmptyGroups;

		// Token: 0x040002EC RID: 748
		private readonly DataShapeBuilderContext _dataShapeBuilderContext;

		// Token: 0x040002ED RID: 749
		private readonly DataShapeBuilder _dataShapeBuilder;

		// Token: 0x040002EE RID: 750
		private readonly QueryParameterReferenceContext _parameterRefContext;
	}
}
