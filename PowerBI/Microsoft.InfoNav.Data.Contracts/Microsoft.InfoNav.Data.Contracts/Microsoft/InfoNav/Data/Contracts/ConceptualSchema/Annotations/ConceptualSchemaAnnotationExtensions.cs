using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000127 RID: 295
	public static class ConceptualSchemaAnnotationExtensions
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00010014 File Offset: 0x0000E214
		public static DaxCapabilitiesAnnotation GetDaxCapabilitiesAnnotation(this IConceptualSchema schema)
		{
			IAnnotationProvider<DaxCapabilitiesAnnotation, IConceptualSchema> annotationProvider;
			DaxCapabilitiesAnnotation daxCapabilitiesAnnotation;
			if (schema.TryGetAnnotationProvider<DaxCapabilitiesAnnotation, IConceptualSchema>(out annotationProvider) && annotationProvider.TryGetAnnotation(schema, out daxCapabilitiesAnnotation))
			{
				return daxCapabilitiesAnnotation;
			}
			return null;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001003C File Offset: 0x0000E23C
		public static DaxCapabilitiesAnnotation GetDefaultSchemaDaxCapabilitiesAnnotation(this IFederatedConceptualSchema federatedSchema)
		{
			IConceptualSchema conceptualSchema;
			if (federatedSchema.TryGetDefaultSchema(out conceptualSchema))
			{
				return conceptualSchema.GetDaxCapabilitiesAnnotation();
			}
			return null;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001005C File Offset: 0x0000E25C
		public static NavigationPropertyGraphAnnotation GetNavigationPropertyGraphAnnotation(this IConceptualSchema schema)
		{
			IAnnotationProvider<NavigationPropertyGraphAnnotation, IConceptualSchema> annotationProvider;
			NavigationPropertyGraphAnnotation navigationPropertyGraphAnnotation;
			if (schema.TryGetAnnotationProvider<NavigationPropertyGraphAnnotation, IConceptualSchema>(out annotationProvider) && annotationProvider.TryGetAnnotation(schema, out navigationPropertyGraphAnnotation))
			{
				return navigationPropertyGraphAnnotation;
			}
			return null;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00010084 File Offset: 0x0000E284
		public static MParameterAnnotation GetMParameterAnnotation(this IConceptualSchema schema)
		{
			IAnnotationProvider<MParameterAnnotation, IConceptualSchema> annotationProvider;
			MParameterAnnotation mparameterAnnotation;
			if (schema.TryGetAnnotationProvider<MParameterAnnotation, IConceptualSchema>(out annotationProvider) && annotationProvider.TryGetAnnotation(schema, out mparameterAnnotation))
			{
				return mparameterAnnotation;
			}
			return null;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000100AC File Offset: 0x0000E2AC
		public static MParameterAnnotation GetDefaultMParameterAnnotation(this IFederatedConceptualSchema federatedSchema)
		{
			IConceptualSchema conceptualSchema;
			if (federatedSchema.TryGetDefaultSchema(out conceptualSchema))
			{
				return conceptualSchema.GetMParameterAnnotation();
			}
			return null;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000100CC File Offset: 0x0000E2CC
		public static FieldRelationshipAnnotations GetDefaultFieldRelationshipAnnotations(this IFederatedConceptualSchema federatedSchema)
		{
			IConceptualSchema conceptualSchema;
			if (federatedSchema.TryGetDefaultSchema(out conceptualSchema))
			{
				return conceptualSchema.GetFieldRelationshipAnnotations();
			}
			return null;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000100EC File Offset: 0x0000E2EC
		public static FieldRelationshipAnnotations GetFieldRelationshipAnnotations(this IConceptualSchema schema)
		{
			IAnnotationProvider<FieldRelationshipAnnotations, IConceptualSchema> annotationProvider;
			FieldRelationshipAnnotations fieldRelationshipAnnotations;
			if (schema.TryGetAnnotationProvider<FieldRelationshipAnnotations, IConceptualSchema>(out annotationProvider) && annotationProvider.TryGetAnnotation(schema, out fieldRelationshipAnnotations))
			{
				return fieldRelationshipAnnotations;
			}
			return null;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00010114 File Offset: 0x0000E314
		public static ColumnGroupingAnnotations GetDefaultColumnGroupingAnnotations(this IFederatedConceptualSchema federatedSchema)
		{
			IConceptualSchema conceptualSchema;
			if (federatedSchema.TryGetDefaultSchema(out conceptualSchema))
			{
				return conceptualSchema.GetColumnGroupingAnnotations();
			}
			return null;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00010134 File Offset: 0x0000E334
		public static ColumnGroupingAnnotations GetColumnGroupingAnnotations(this IConceptualSchema schema)
		{
			IAnnotationProvider<ColumnGroupingAnnotations, IConceptualSchema> annotationProvider;
			ColumnGroupingAnnotations columnGroupingAnnotations;
			if (schema.TryGetAnnotationProvider<ColumnGroupingAnnotations, IConceptualSchema>(out annotationProvider) && annotationProvider.TryGetAnnotation(schema, out columnGroupingAnnotations))
			{
				return columnGroupingAnnotations;
			}
			return null;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001015C File Offset: 0x0000E35C
		public static IDirectedGraph<IConceptualEntity> GetAssociationsFromOneGraph(this IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			NavigationPropertyGraphAnnotation navigationPropertyGraphAnnotation = schema.GetNavigationPropertyGraphAnnotation();
			if (!includeDirectManyToManyAssociations)
			{
				return navigationPropertyGraphAnnotation.AssociationsFromOneGraph;
			}
			return navigationPropertyGraphAnnotation.AssociationsFromOneAndDirectedOneHopManyToManyGraph;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00010180 File Offset: 0x0000E380
		public static IDirectedGraph<IConceptualEntity> GetAssociationsFromOneWithBidirCrossFilteringGraph(this IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			NavigationPropertyGraphAnnotation navigationPropertyGraphAnnotation = schema.GetNavigationPropertyGraphAnnotation();
			if (!includeDirectManyToManyAssociations)
			{
				return navigationPropertyGraphAnnotation.AssociationsFromOneWithBidirCrossFilteringGraph;
			}
			return navigationPropertyGraphAnnotation.AssociationsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000101A4 File Offset: 0x0000E3A4
		public static IReadOnlyList<ConceptualTranslation> GetTranslations(this IConceptualSchema schema, IConceptualDisplayItem displayItem)
		{
			IAnnotationProvider<TranslationsAnnotation, IConceptualDisplayItem> annotationProvider;
			TranslationsAnnotation translationsAnnotation;
			if (schema.TryGetAnnotationProvider<TranslationsAnnotation, IConceptualDisplayItem>(out annotationProvider) && annotationProvider.TryGetAnnotation(displayItem, out translationsAnnotation))
			{
				return translationsAnnotation.Translations;
			}
			return null;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000101D0 File Offset: 0x0000E3D0
		public static CsdlSchemaNamespaceAnnotation GetCsdlSchemaNamespaceAnnotation(this IConceptualSchema schema)
		{
			IAnnotationProvider<CsdlSchemaNamespaceAnnotation, IConceptualSchema> annotationProvider;
			CsdlSchemaNamespaceAnnotation csdlSchemaNamespaceAnnotation;
			if (schema.TryGetAnnotationProvider<CsdlSchemaNamespaceAnnotation, IConceptualSchema>(out annotationProvider) && annotationProvider.TryGetAnnotation(schema, out csdlSchemaNamespaceAnnotation))
			{
				return csdlSchemaNamespaceAnnotation;
			}
			return null;
		}
	}
}
