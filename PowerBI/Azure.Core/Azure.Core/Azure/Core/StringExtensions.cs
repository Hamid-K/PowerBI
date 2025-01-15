using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200004B RID: 75
	internal static class StringExtensions
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00006DAF File Offset: 0x00004FAF
		[NullableContext(1)]
		public static int IndexOfOrdinal(this string s, char c)
		{
			return s.IndexOf(c);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006DB8 File Offset: 0x00004FB8
		[NullableContext(2)]
		public static int GetHashCodeOrdinal(this string s)
		{
			if (s == null)
			{
				return 0;
			}
			return StringComparer.Ordinal.GetHashCode(s);
		}
	}
}
