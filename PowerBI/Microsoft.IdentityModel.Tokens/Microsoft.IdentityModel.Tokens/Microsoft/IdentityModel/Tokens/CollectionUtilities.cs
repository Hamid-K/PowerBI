using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000124 RID: 292
	public static class CollectionUtilities
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x00039985 File Offset: 0x00037B85
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable == null || !enumerable.Any<T>();
		}
	}
}
