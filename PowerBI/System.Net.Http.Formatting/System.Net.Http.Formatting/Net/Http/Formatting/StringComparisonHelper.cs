using System;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004E RID: 78
	internal static class StringComparisonHelper
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000A165 File Offset: 0x00008365
		public static bool IsDefined(StringComparison value)
		{
			return value == StringComparison.CurrentCulture || value == StringComparison.CurrentCultureIgnoreCase || value == StringComparison.InvariantCulture || value == StringComparison.InvariantCultureIgnoreCase || value == StringComparison.Ordinal || value == StringComparison.OrdinalIgnoreCase;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A180 File Offset: 0x00008380
		public static void Validate(StringComparison value, string parameterName)
		{
			if (!StringComparisonHelper.IsDefined(value))
			{
				throw Error.InvalidEnumArgument(parameterName, (int)value, typeof(StringComparison));
			}
		}
	}
}
