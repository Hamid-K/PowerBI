using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x0200012F RID: 303
	public static class EntityRowNumberAnnotationExtensions
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x000106E0 File Offset: 0x0000E8E0
		public static bool TryGetRowNumberName(this IConceptualEntity entity, out string rowNumberName)
		{
			IAnnotationProvider<EntityRowNumberAnnotation, IConceptualEntity> annotationProvider;
			EntityRowNumberAnnotation entityRowNumberAnnotation;
			if (!entity.Schema.TryGetAnnotationProvider<EntityRowNumberAnnotation, IConceptualEntity>(out annotationProvider) || !annotationProvider.TryGetAnnotation(entity, out entityRowNumberAnnotation))
			{
				rowNumberName = null;
				return false;
			}
			rowNumberName = entityRowNumberAnnotation.Name;
			return true;
		}
	}
}
