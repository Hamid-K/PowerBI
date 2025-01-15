using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000065 RID: 101
	internal static class TypeSemantics
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x00014518 File Offset: 0x00012718
		internal static bool IsNullable(TypeUsage type)
		{
			Facet facet;
			return !type.Facets.TryGetValue("Nullable", false, out facet) || (bool)facet.Value;
		}
	}
}
