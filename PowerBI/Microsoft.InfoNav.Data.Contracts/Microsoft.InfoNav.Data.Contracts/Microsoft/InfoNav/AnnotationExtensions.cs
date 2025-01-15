using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000012 RID: 18
	public static class AnnotationExtensions
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000218C File Offset: 0x0000038C
		public static bool TryGetAnnotation<TAnnotation>(this IConceptualEntity entity, out TAnnotation annotation)
		{
			return AnnotationExtensions.TryGetAnnotationCore<TAnnotation, IConceptualEntity>(entity, (entity != null) ? entity.Schema : null, out annotation);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000021A1 File Offset: 0x000003A1
		public static bool TryGetAnnotation<TAnnotation>(this IConceptualProperty property, out TAnnotation annotation)
		{
			IConceptualSchema conceptualSchema;
			if (property == null)
			{
				conceptualSchema = null;
			}
			else
			{
				IConceptualEntity entity = property.Entity;
				conceptualSchema = ((entity != null) ? entity.Schema : null);
			}
			return AnnotationExtensions.TryGetAnnotationCore<TAnnotation, IConceptualProperty>(property, conceptualSchema, out annotation);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000021C4 File Offset: 0x000003C4
		public static bool TryGetAnnotation<TAnnotation>(this IConceptualColumn column, out TAnnotation annotation)
		{
			IConceptualSchema conceptualSchema;
			if (column == null)
			{
				conceptualSchema = null;
			}
			else
			{
				IConceptualEntity entity = column.Entity;
				conceptualSchema = ((entity != null) ? entity.Schema : null);
			}
			IConceptualSchema conceptualSchema2 = conceptualSchema;
			if (conceptualSchema2 == null)
			{
				annotation = default(TAnnotation);
				return false;
			}
			return AnnotationExtensions.TryGetAnnotationCore<TAnnotation, IConceptualColumn>(column, conceptualSchema2, out annotation) || AnnotationExtensions.TryGetAnnotationCore<TAnnotation, IConceptualProperty>(column, conceptualSchema2, out annotation);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000220C File Offset: 0x0000040C
		public static bool TryGetAnnotation<TAnnotation>(this IConceptualMeasure measure, out TAnnotation annotation)
		{
			IConceptualSchema conceptualSchema;
			if (measure == null)
			{
				conceptualSchema = null;
			}
			else
			{
				IConceptualEntity entity = measure.Entity;
				conceptualSchema = ((entity != null) ? entity.Schema : null);
			}
			IConceptualSchema conceptualSchema2 = conceptualSchema;
			if (conceptualSchema2 == null)
			{
				annotation = default(TAnnotation);
				return false;
			}
			return AnnotationExtensions.TryGetAnnotationCore<TAnnotation, IConceptualMeasure>(measure, conceptualSchema2, out annotation) || AnnotationExtensions.TryGetAnnotationCore<TAnnotation, IConceptualProperty>(measure, conceptualSchema2, out annotation);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002254 File Offset: 0x00000454
		private static bool TryGetAnnotationCore<TAnnotation, TTarget>(TTarget item, IConceptualSchema schema, out TAnnotation annotation)
		{
			Contract.CheckValue<IConceptualSchema>(schema, "schema");
			IAnnotationProvider<TAnnotation, TTarget> annotationProvider;
			if (!schema.TryGetAnnotationProvider<TAnnotation, TTarget>(out annotationProvider) || !annotationProvider.TryGetAnnotation(item, out annotation))
			{
				annotation = default(TAnnotation);
				return false;
			}
			return true;
		}
	}
}
