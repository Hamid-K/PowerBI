using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001E9 RID: 489
	internal static class CacheHelper
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x00020D6B File Offset: 0x0001EF6B
		internal static object BoxedBool(bool value)
		{
			if (!value)
			{
				return CacheHelper.BoxedFalse;
			}
			return CacheHelper.BoxedTrue;
		}

		// Token: 0x04000535 RID: 1333
		internal static readonly object Unknown = new object();

		// Token: 0x04000536 RID: 1334
		internal static readonly object CycleSentinel = new object();

		// Token: 0x04000537 RID: 1335
		internal static readonly object SecondPassCycleSentinel = new object();

		// Token: 0x04000538 RID: 1336
		private static readonly object BoxedTrue = true;

		// Token: 0x04000539 RID: 1337
		private static readonly object BoxedFalse = false;
	}
}
