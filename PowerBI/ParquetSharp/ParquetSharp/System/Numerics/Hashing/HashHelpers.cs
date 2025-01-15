using System;

namespace System.Numerics.Hashing
{
	// Token: 0x020000BB RID: 187
	internal static class HashHelpers
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x000187D0 File Offset: 0x000169D0
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)((h1 << 5) | (int)((uint)h1 >> 27));
			return (int)((num + (uint)h1) ^ (uint)h2);
		}

		// Token: 0x040001D7 RID: 471
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
