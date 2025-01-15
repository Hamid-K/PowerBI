using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200008F RID: 143
	internal static class CacheHelper
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x00009E37 File Offset: 0x00008037
		internal static object BoxedBool(bool value)
		{
			if (!value)
			{
				return CacheHelper.BoxedFalse;
			}
			return CacheHelper.BoxedTrue;
		}

		// Token: 0x0400010B RID: 267
		internal static readonly object Unknown = new object();

		// Token: 0x0400010C RID: 268
		internal static readonly object CycleSentinel = new object();

		// Token: 0x0400010D RID: 269
		internal static readonly object SecondPassCycleSentinel = new object();

		// Token: 0x0400010E RID: 270
		private static readonly object BoxedTrue = true;

		// Token: 0x0400010F RID: 271
		private static readonly object BoxedFalse = false;
	}
}
