using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001C1 RID: 449
	internal static class CollectionHelpers
	{
		// Token: 0x060013F5 RID: 5109 RVA: 0x00043CE2 File Offset: 0x00041EE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReadOnlyList<T> GetEmptyReadOnlyList<T>()
		{
			return Array.Empty<T>();
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00043CE9 File Offset: 0x00041EE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static List<T> GetEmptyList<T>()
		{
			return new List<T>();
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00043CF0 File Offset: 0x00041EF0
		public static IReadOnlyDictionary<TKey, TValue> GetEmptyDictionary<TKey, TValue>()
		{
			return new Dictionary<TKey, TValue>();
		}
	}
}
