using System;
using System.Collections.Generic;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000F9 RID: 249
	internal sealed class CachedPrimitiveKeepInContentAnnotation
	{
		// Token: 0x06000E99 RID: 3737 RVA: 0x0002301E File Offset: 0x0002121E
		internal CachedPrimitiveKeepInContentAnnotation(IEnumerable<string> keptInContentPropertyNames)
		{
			this.keptInContentPropertyNames = ((keptInContentPropertyNames == null) ? null : new HashSet<string>(keptInContentPropertyNames, StringComparer.Ordinal));
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002303D File Offset: 0x0002123D
		internal bool IsKeptInContent(string propertyName)
		{
			return this.keptInContentPropertyNames != null && this.keptInContentPropertyNames.Contains(propertyName);
		}

		// Token: 0x0400072E RID: 1838
		private readonly HashSet<string> keptInContentPropertyNames;
	}
}
