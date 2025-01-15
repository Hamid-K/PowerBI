using System;

namespace Microsoft.DI.RoaringBitmap.Utilities
{
	// Token: 0x0200000C RID: 12
	internal static class Pair
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003226 File Offset: 0x00001426
		public static Pair<T> New<T>(T high, T low) where T : struct
		{
			return new Pair<T>(high, low);
		}
	}
}
