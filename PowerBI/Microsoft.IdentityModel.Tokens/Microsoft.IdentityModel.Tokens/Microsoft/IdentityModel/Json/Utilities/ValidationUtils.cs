using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Utilities
{
	// Token: 0x02000072 RID: 114
	internal static class ValidationUtils
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x00019B3C File Offset: 0x00017D3C
		[NullableContext(1)]
		public static void ArgumentNotNull([Nullable(2)] [NotNull] object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}
	}
}
