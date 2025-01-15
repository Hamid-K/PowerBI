using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000013 RID: 19
	internal static class CacheHelper
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00006DC3 File Offset: 0x00004FC3
		internal static object BoxedBool(bool value)
		{
			if (!value)
			{
				return CacheHelper.BoxedFalse;
			}
			return CacheHelper.BoxedTrue;
		}

		// Token: 0x04000025 RID: 37
		internal static readonly object Unknown = new object();

		// Token: 0x04000026 RID: 38
		internal static readonly object CycleSentinel = new object();

		// Token: 0x04000027 RID: 39
		internal static readonly object SecondPassCycleSentinel = new object();

		// Token: 0x04000028 RID: 40
		private static readonly object BoxedTrue = true;

		// Token: 0x04000029 RID: 41
		private static readonly object BoxedFalse = false;
	}
}
