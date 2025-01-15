using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Library;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200001B RID: 27
	internal static class FederatedConceptualSchemaExtensions
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x0000597C File Offset: 0x00003B7C
		internal static IFederatedConceptualSchema PrepareAndBuildFederatedSchema(DataShapeGenerationContext context, DataShapeGenerationErrorContext errorContext, SemanticQueryDataShapeCommand command, out QueryExtensionSchemaContext resolvedQueryExtensionSchema)
		{
			resolvedQueryExtensionSchema = null;
			FederatedConceptualSchemaExtensions.UpgradeExtensionSchema(context, errorContext, command.Extension);
			if (errorContext.HasError)
			{
				return null;
			}
			FederatedConceptualSchemaExtensions.RewriteExtensionSchema(command, context.Model, errorContext, out resolvedQueryExtensionSchema);
			if (errorContext.HasError)
			{
				return null;
			}
			return FederatedConceptualSchemaExtensions.BuildFederatedConceptualSchema(context, errorContext, command.Extension);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000059C8 File Offset: 0x00003BC8
		internal static IFederatedConceptualSchema Update(IFederatedConceptualSchema federatedConceptualSchema, IConceptualSchema schema)
		{
			return new FederatedConceptualSchema(new List<IConceptualSchema>(federatedConceptualSchema.Schemas.Where((IConceptualSchema es) => es.SchemaId != schema.SchemaId)) { schema });
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005A10 File Offset: 0x00003C10
		private static IFederatedConceptualSchema BuildFederatedConceptualSchema(DataShapeGenerationContext context, DataShapeGenerationErrorContext errorContext, QueryExtensionSchema queryExtensionSchema)
		{
			IConceptualSchema conceptualSchema = null;
			if (queryExtensionSchema != null)
			{
				QueryResolutionErrorContext queryResolutionErrorContext = new QueryResolutionErrorContext(new DataShapeGenerationErrorContextAdapter(errorContext, DataShapeGenerationErrorCode.CouldNotResolveQueryExtensionSchema, ErrorSourceCategory.InputDoesNotMatchModel));
				if (!ExtensionConceptualSchemaBuilder.TryCreateConceptualSchema(context.Model, queryExtensionSchema, queryResolutionErrorContext, out conceptualSchema) && !errorContext.HasError)
				{
					context.Tracer.SanitizedTrace(TraceLevel.Error, "ExtensionConceptualSchemaBuilder failed without registering an error.");
					errorContext.Register(DataShapeGenerationMessages.CouldNotResolveQueryExtensionSchema(EngineMessageSeverity.Error));
				}
				else if (queryResolutionErrorContext.UnresolvedModelReferences.Count > 0)
				{
					errorContext.Register(DataShapeGenerationMessages.CouldNotResolveModelReferencesInQueryExtensionSchema(EngineMessageSeverity.Error, queryResolutionErrorContext.UnresolvedModelReferences.ToArray()));
				}
			}
			IFederatedConceptualSchema federatedConceptualSchema = null;
			if (!errorContext.HasError)
			{
				if (conceptualSchema != null)
				{
					federatedConceptualSchema = new FederatedConceptualSchema(new IConceptualSchema[] { context.Model, conceptualSchema });
				}
				else
				{
					federatedConceptualSchema = context.Model.ToFederatedSchema();
				}
			}
			return federatedConceptualSchema;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005AD0 File Offset: 0x00003CD0
		private static void RewriteExtensionSchema(SemanticQueryDataShapeCommand command, IConceptualSchema model, DataShapeGenerationErrorContext errorContext, out QueryExtensionSchemaContext resolvedQueryExtensionSchema)
		{
			resolvedQueryExtensionSchema = QueryExtensionSchemaResolver.Resolve(command.Extension, model, errorContext);
			if (resolvedQueryExtensionSchema == null || resolvedQueryExtensionSchema.QuerySchemaMapping == null || resolvedQueryExtensionSchema.QuerySchemaMapping.IsEmpty || errorContext.HasError)
			{
				return;
			}
			QueryExtensionReferenceReplacer.Rewrite(command, resolvedQueryExtensionSchema.QuerySchemaMapping);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005B20 File Offset: 0x00003D20
		private static void UpgradeExtensionSchema(DataShapeGenerationContext context, DataShapeGenerationErrorContext errorContext, QueryExtensionSchema extensionSchema)
		{
			if (extensionSchema == null)
			{
				return;
			}
			DataShapeGenerationErrorContextAdapter dataShapeGenerationErrorContextAdapter = new DataShapeGenerationErrorContextAdapter(errorContext, DataShapeGenerationErrorCode.CouldNotUpgradeExtensionSchema, ErrorSourceCategory.MalformedExternalInput);
			QueryExtensionSchemaUpgrader.Upgrade(extensionSchema, dataShapeGenerationErrorContextAdapter);
		}
	}
}
