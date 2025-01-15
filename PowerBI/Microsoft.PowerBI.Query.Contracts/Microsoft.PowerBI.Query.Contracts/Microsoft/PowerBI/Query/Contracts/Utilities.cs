using System;
using System.Globalization;

namespace Microsoft.PowerBI.Query.Contracts
{
	// Token: 0x02000009 RID: 9
	internal static class Utilities
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000227E File Offset: 0x0000047E
		internal static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}
	}
}
