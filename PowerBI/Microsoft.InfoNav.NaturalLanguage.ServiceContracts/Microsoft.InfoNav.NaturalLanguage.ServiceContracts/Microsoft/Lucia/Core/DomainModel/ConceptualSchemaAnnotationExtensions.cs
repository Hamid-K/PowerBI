using System;
using System.Collections.Immutable;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000182 RID: 386
	public static class ConceptualSchemaAnnotationExtensions
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0000E118 File Offset: 0x0000C318
		public static IImmutableSet<IConceptualEntity> LogicalEntities(this IConceptualMeasure measure)
		{
			IMeasureLogicalIdentityAnnotation measureLogicalIdentityAnnotation;
			IImmutableSet<IConceptualEntity> immutableSet = (measure.TryGetAnnotation(out measureLogicalIdentityAnnotation) ? ((measureLogicalIdentityAnnotation != null) ? measureLogicalIdentityAnnotation.LogicalEntities : null) : null);
			return immutableSet ?? ImmutableHashSet<IConceptualEntity>.Empty;
		}
	}
}
