using System;
using System.Globalization;

namespace Microsoft.DataShaping.Common.DaxComparer
{
	// Token: 0x02000024 RID: 36
	internal static class CompareUtils
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00004B40 File Offset: 0x00002D40
		internal static CompareOptions CreateCompareOptions(bool ignoreCase, bool ignoreNonSpace, bool ignoreKanaType, bool ignoreWidth, bool useOrdinalComparison)
		{
			CompareOptions compareOptions = CompareOptions.None;
			if (useOrdinalComparison)
			{
				return CompareOptions.Ordinal;
			}
			if (ignoreCase)
			{
				compareOptions |= CompareOptions.IgnoreCase;
			}
			if (ignoreNonSpace)
			{
				compareOptions |= CompareOptions.IgnoreNonSpace;
			}
			if (ignoreKanaType)
			{
				compareOptions |= CompareOptions.IgnoreKanaType;
			}
			if (ignoreWidth)
			{
				compareOptions |= CompareOptions.IgnoreWidth;
			}
			return compareOptions;
		}
	}
}
