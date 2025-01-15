using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000019 RID: 25
	internal static class HashHelpers
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00009D94 File Offset: 0x00007F94
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)((h1 << 5) | (int)((uint)h1 >> 27));
			return (int)((num + (uint)h1) ^ (uint)h2);
		}

		// Token: 0x0400006C RID: 108
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
