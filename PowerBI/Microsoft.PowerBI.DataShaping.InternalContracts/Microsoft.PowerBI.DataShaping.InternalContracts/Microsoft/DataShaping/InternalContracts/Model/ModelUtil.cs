using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.DataShaping.InternalContracts.Model
{
	// Token: 0x02000027 RID: 39
	public static class ModelUtil
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000397C File Offset: 0x00001B7C
		internal static bool SupportsHierarchicalFilterDisjunction(this IFederatedConceptualSchema federatedSchema)
		{
			IConceptualSchema conceptualSchema;
			federatedSchema.TryGetDefaultSchema(out conceptualSchema);
			return conceptualSchema.Capabilities.SupportsHierarchicalFilterDisjunction();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000399D File Offset: 0x00001B9D
		internal static bool SupportsHierarchicalFilterDisjunction(this ConceptualCapabilities capabilities)
		{
			return capabilities.SupportsMultiTableTupleFilters;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000039A5 File Offset: 0x00001BA5
		internal static bool RequiresDistinctForNestedOuterJoins(this IFederatedConceptualSchema federatedSchema)
		{
			return !federatedSchema.GetDefaultSchemaDaxCapabilitiesAnnotation().DaxFunctions.SupportsTreatAs;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000039BC File Offset: 0x00001BBC
		internal static ConceptualTableType GetExtensionAwareResultType(this IConceptualEntity entity)
		{
			IExtensionConceptualEntity extensionConceptualEntity = entity as IExtensionConceptualEntity;
			if (extensionConceptualEntity != null && extensionConceptualEntity.ExtendedEntity != null)
			{
				return entity.GetExtensionAwareRowResultType().Table();
			}
			return entity.ConceptualResultType;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000039F0 File Offset: 0x00001BF0
		internal static ConceptualRowType GetExtensionAwareRowResultType(this IConceptualEntity entity)
		{
			IExtensionConceptualEntity extensionConceptualEntity = entity as IExtensionConceptualEntity;
			if (extensionConceptualEntity != null && extensionConceptualEntity.ExtendedEntity != null)
			{
				return extensionConceptualEntity.ExtendedEntity.GetExtensionAwareRowResultType().MergeRows(entity.ConceptualResultType.RowType, ColumnMergingBehavior.Disallow);
			}
			return entity.ConceptualResultType.RowType;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003A38 File Offset: 0x00001C38
		internal static IEnumerable<IConceptualProperty> GetExtensionAwareProperties(this IConceptualEntity entity)
		{
			IExtensionConceptualEntity extensionConceptualEntity = entity as IExtensionConceptualEntity;
			if (extensionConceptualEntity != null && extensionConceptualEntity.ExtendedEntity != null)
			{
				return extensionConceptualEntity.ExtendedEntity.GetExtensionAwareProperties().Concat(entity.Properties);
			}
			return entity.Properties;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003A74 File Offset: 0x00001C74
		internal static IDataComparer GetComparer(this IConceptualSchema schema)
		{
			ComparerAnnotation comparerAnnotation = null;
			IAnnotationProvider<ComparerAnnotation, IConceptualSchema> annotationProvider;
			if (schema.TryGetAnnotationProvider<ComparerAnnotation, IConceptualSchema>(out annotationProvider))
			{
				annotationProvider.TryGetAnnotation(schema, out comparerAnnotation);
			}
			return comparerAnnotation.Comparer;
		}
	}
}
