using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.Metadata
{
	// Token: 0x02000129 RID: 297
	internal sealed class CachedPrimitiveKeepInContentAnnotation
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x000292B1 File Offset: 0x000274B1
		internal CachedPrimitiveKeepInContentAnnotation(IEnumerable<string> keptInContentPropertyNames)
		{
			this.keptInContentPropertyNames = ((keptInContentPropertyNames == null) ? null : new HashSet<string>(keptInContentPropertyNames, StringComparer.Ordinal));
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000292D0 File Offset: 0x000274D0
		internal bool IsKeptInContent(string propertyName)
		{
			return this.keptInContentPropertyNames != null && this.keptInContentPropertyNames.Contains(propertyName);
		}

		// Token: 0x04000483 RID: 1155
		private readonly HashSet<string> keptInContentPropertyNames;
	}
}
