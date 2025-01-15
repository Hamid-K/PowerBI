using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000220 RID: 544
	internal sealed class CachedPrimitiveKeepInContentAnnotation
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x0003CE9B File Offset: 0x0003B09B
		internal CachedPrimitiveKeepInContentAnnotation(IEnumerable<string> keptInContentPropertyNames)
		{
			this.keptInContentPropertyNames = ((keptInContentPropertyNames == null) ? null : new HashSet<string>(keptInContentPropertyNames, StringComparer.Ordinal));
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0003CEBA File Offset: 0x0003B0BA
		internal bool IsKeptInContent(string propertyName)
		{
			return this.keptInContentPropertyNames != null && this.keptInContentPropertyNames.Contains(propertyName);
		}

		// Token: 0x04000634 RID: 1588
		private readonly HashSet<string> keptInContentPropertyNames;
	}
}
