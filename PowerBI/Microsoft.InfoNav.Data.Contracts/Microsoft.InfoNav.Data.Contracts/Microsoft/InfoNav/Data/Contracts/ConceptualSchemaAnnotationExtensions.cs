using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200007D RID: 125
	public static class ConceptualSchemaAnnotationExtensions
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x000079D8 File Offset: 0x00005BD8
		public static IReadOnlyList<IReadOnlyList<IConceptualColumn>> InferredUniqueKeys(this IConceptualEntity entity)
		{
			IUniqueKeyAnnotation uniqueKeyAnnotation;
			IReadOnlyList<IReadOnlyList<IConceptualColumn>> readOnlyList = (entity.TryGetAnnotation(out uniqueKeyAnnotation) ? ((uniqueKeyAnnotation != null) ? uniqueKeyAnnotation.UniqueKeys : null) : null);
			return readOnlyList ?? Util.EmptyReadOnlyCollection<IReadOnlyList<IConceptualColumn>>();
		}
	}
}
