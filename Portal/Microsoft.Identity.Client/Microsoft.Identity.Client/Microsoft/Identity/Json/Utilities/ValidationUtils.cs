using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000071 RID: 113
	internal static class ValidationUtils
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x00019568 File Offset: 0x00017768
		public static void ArgumentNotNull([Nullable(2)] [NotNull] object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}
	}
}
