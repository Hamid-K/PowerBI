using System;
using System.Collections.Generic;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001D1 RID: 465
	internal sealed class CachedPrimitiveKeepInContentAnnotation
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x00032F62 File Offset: 0x00031162
		internal CachedPrimitiveKeepInContentAnnotation(IEnumerable<string> keptInContentPropertyNames)
		{
			this.keptInContentPropertyNames = ((keptInContentPropertyNames == null) ? null : new HashSet<string>(keptInContentPropertyNames, StringComparer.Ordinal));
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00032F81 File Offset: 0x00031181
		internal bool IsKeptInContent(string propertyName)
		{
			return this.keptInContentPropertyNames != null && this.keptInContentPropertyNames.Contains(propertyName);
		}

		// Token: 0x0400093A RID: 2362
		private readonly HashSet<string> keptInContentPropertyNames;
	}
}
