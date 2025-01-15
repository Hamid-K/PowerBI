using System;

namespace System.Numerics.Hashing
{
	// Token: 0x0200000A RID: 10
	internal static class HashHelpers
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00015164 File Offset: 0x00013364
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)((h1 << 5) | (int)((uint)h1 >> 27));
			return (int)((num + (uint)h1) ^ (uint)h2);
		}

		// Token: 0x0400004B RID: 75
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
