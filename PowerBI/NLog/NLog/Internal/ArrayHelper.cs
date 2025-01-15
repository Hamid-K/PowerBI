using System;

namespace NLog.Internal
{
	// Token: 0x0200010A RID: 266
	internal static class ArrayHelper
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x00023BED File Offset: 0x00021DED
		internal static T[] Empty<T>()
		{
			return ArrayHelper.EmptyArray<T>.Instance;
		}

		// Token: 0x02000263 RID: 611
		private static class EmptyArray<T>
		{
			// Token: 0x0400069A RID: 1690
			internal static readonly T[] Instance = new T[0];
		}
	}
}
