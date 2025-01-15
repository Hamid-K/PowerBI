using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DataShapeBindingResolution;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000CA RID: 202
	internal sealed class SemanticQueryDataShapeCommandResolver
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x0001BC06 File Offset: 0x00019E06
		internal SemanticQueryDataShapeCommandResolver(DataShapeGenerationInternalContext context)
		{
			this._context = context;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001BC15 File Offset: 0x00019E15
		internal static bool TryResolve(DataShapeGenerationInternalContext context, SemanticQueryDataShapeCommand command, QueryExtensionSchemaContext resolvedQueryExtensionSchema, out ResolvedSemanticQueryDataShapeCommand resolvedCommand)
		{
			return new SemanticQueryDataShapeCommandResolver(context).TryResolve(command, resolvedQueryExtensionSchema, out resolvedCommand);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001BC25 File Offset: 0x00019E25
		private bool TryResolve(SemanticQueryDataShapeCommand command, QueryExtensionSchemaContext resolvedQueryExtensionSchema, out ResolvedSemanticQueryDataShapeCommand resolvedCommand)
		{
			resolvedCommand = null;
			return this.TryResolveQueryCommand(command, resolvedQueryExtensionSchema, ref resolvedCommand);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001BC34 File Offset: 0x00019E34
		private bool TryResolveQueryCommand(SemanticQueryDataShapeCommand command, QueryExtensionSchemaContext resolvedQueryExtensionSchema, ref ResolvedSemanticQueryDataShapeCommand resolvedCommand)
		{
			ResolvedQueryDefinition resolvedQueryDefinition;
			if (!this.TryResolve(command.Query, command.AdditionalODataFilter, out resolvedQueryDefinition))
			{
				return false;
			}
			ResolvedDataReduction resolvedDataReduction;
			if (!this.TryResolve(command.Binding, resolvedQueryDefinition.Name, out resolvedDataReduction))
			{
				return false;
			}
			ResolvedSemanticQueryDataShape resolvedSemanticQueryDataShape = new ResolvedSemanticQueryDataShape(resolvedQueryDefinition, command.Binding, command.MaxRowCount, resolvedDataReduction);
			resolvedCommand = new ResolvedSemanticQueryDataShapeCommand(resolvedSemanticQueryDataShape, resolvedQueryExtensionSchema, command.DataSourceVariables);
			return true;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001BC95 File Offset: 0x00019E95
		private bool TryResolve(QueryDefinition query, string additionalODataFilter, out ResolvedQueryDefinition resolvedQuery)
		{
			if (!this.TryResolveQueryDefinition(query, out resolvedQuery))
			{
				return false;
			}
			resolvedQuery = ResolvedQueryDsqRewriter.RewriteQuery(resolvedQuery);
			return this.TryResolveODataAdditionalFilter(additionalODataFilter, resolvedQuery, out resolvedQuery);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001BCBC File Offset: 0x00019EBC
		private bool TryResolve(DataShapeBinding binding, string rootQueryName, out ResolvedDataReduction resolvedReduction)
		{
			resolvedReduction = null;
			if (binding == null)
			{
				return true;
			}
			DataShapeGenerationErrorContextAdapter dataShapeGenerationErrorContextAdapter = new DataShapeGenerationErrorContextAdapter(this._context.ErrorContext, DataShapeGenerationErrorCode.CouldNotResolveDataShapeBinding, ErrorSourceCategory.InputDoesNotMatchModel);
			if (!DataShapeBindingNormalizer.TryNormalize(dataShapeGenerationErrorContextAdapter, binding, rootQueryName))
			{
				if (!dataShapeGenerationErrorContextAdapter.HasError)
				{
					this._context.Tracer.SanitizedTrace(TraceLevel.Error, "DataShapeBinding Normalization failed without registering an error.");
				}
				this._context.ErrorContext.Register(DataShapeGenerationMessages.CouldNotNormalizeDataShapeBinding(EngineMessageSeverity.Error));
				return false;
			}
			QueryResolutionErrorContext queryResolutionErrorContext = new QueryResolutionErrorContext(dataShapeGenerationErrorContextAdapter);
			ResolvedDataShapeBinding resolvedDataShapeBinding;
			if (!DataShapeBindingResolver.TryResolve(binding, this._context.FederatedConceptualSchema, queryResolutionErrorContext, rootQueryName, out resolvedDataShapeBinding))
			{
				if (!dataShapeGenerationErrorContextAdapter.HasError)
				{
					this._context.Tracer.SanitizedTrace(TraceLevel.Error, "DataShapeBinding Resolution failed without registering an error.");
				}
				this._context.ErrorContext.Register(DataShapeGenerationMessages.CouldNotResolveDataShapeBinding(EngineMessageSeverity.Error));
				return false;
			}
			resolvedReduction = resolvedDataShapeBinding.DataReduction;
			return true;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001BD8C File Offset: 0x00019F8C
		private bool TryResolveODataAdditionalFilter(string additionalODataFilter, ResolvedQueryDefinition resolvedQuery, out ResolvedQueryDefinition rewrittenResolvedQuery)
		{
			rewrittenResolvedQuery = resolvedQuery;
			if (additionalODataFilter == null)
			{
				return true;
			}
			DataShapeGenerationErrorContextAdapter dataShapeGenerationErrorContextAdapter = new DataShapeGenerationErrorContextAdapter(this._context.ErrorContext, DataShapeGenerationErrorCode.CouldNotApplyAdditionalODataFilter, ErrorSourceCategory.UserInput);
			IConceptualSchema conceptualSchema;
			if (!this._context.FederatedConceptualSchema.TryGetDefaultSchema(out conceptualSchema) || !this.TryApplyAdditionalODataFilter(resolvedQuery, conceptualSchema, additionalODataFilter, dataShapeGenerationErrorContextAdapter, out rewrittenResolvedQuery))
			{
				if (!this._context.ErrorContext.HasError)
				{
					this._context.Tracer.SanitizedTrace(TraceLevel.Error, "OData Query Filter Processing failed without registering an error.");
					this._context.ErrorContext.Register(DataShapeGenerationMessages.CouldNotApplyAdditionalODataFilter(EngineMessageSeverity.Error));
				}
				this._context.Tracer.SanitizedTrace(TraceLevel.Error, "Failed to process additional OData filter \"{0}\" due to \"{1}\"", additionalODataFilter.MarkAsCustomerContent(), this._context.ErrorContext.Messages.FirstOrDefault<DataShapeGenerationMessage>());
				return false;
			}
			return true;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001BE4B File Offset: 0x0001A04B
		private bool TryApplyAdditionalODataFilter(ResolvedQueryDefinition query, IConceptualSchema schema, string odataFilter, IErrorContext errorContext, out ResolvedQueryDefinition filteredQuery)
		{
			return ODataQueryFilterProcessor.TryApplyFilter(query, schema, odataFilter, errorContext, out filteredQuery);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001BE5C File Offset: 0x0001A05C
		private bool TryResolveQueryDefinition(QueryDefinition query, out ResolvedQueryDefinition resolvedQuery)
		{
			QueryResolutionErrorContext queryResolutionErrorContext = new QueryResolutionErrorContext(new DataShapeGenerationErrorContextAdapter(this._context.ErrorContext, DataShapeGenerationErrorCode.CouldNotResolveSemanticQueryDefinition, ErrorSourceCategory.InputDoesNotMatchModel));
			QueryDefinitionNameRegistrar queryDefinitionNameRegistrar = new QueryDefinitionNameRegistrar();
			queryDefinitionNameRegistrar.PushName("Root", false);
			HashSet<string> hashSet = new HashSet<string>(QueryNameComparer.Instance);
			bool flag = QueryDefinitionResolver.TryResolveQuery(query, this._context.FederatedConceptualSchema, queryResolutionErrorContext, hashSet, queryDefinitionNameRegistrar, out resolvedQuery);
			if (queryResolutionErrorContext.UnresolvedModelReferences.Count > 0)
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.CouldNotResolveModelReferencesInSemanticQuery(EngineMessageSeverity.Error, queryResolutionErrorContext.UnresolvedModelReferences.ToArray()));
			}
			return flag;
		}

		// Token: 0x040003D1 RID: 977
		private const string CandidateNameForTopLevelQueryDefinition = "Root";

		// Token: 0x040003D2 RID: 978
		private readonly DataShapeGenerationInternalContext _context;
	}
}
