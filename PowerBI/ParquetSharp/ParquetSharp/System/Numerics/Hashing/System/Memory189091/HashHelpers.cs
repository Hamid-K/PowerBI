using System;

namespace System.Numerics.Hashing
{
	// Token: 0x020000D7 RID: 215
	internal static class System.Memory189091.HashHelpers
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x0002128C File Offset: 0x0001F48C
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)((h1 << 5) | (int)((uint)h1 >> 27));
			return (int)((num + (uint)h1) ^ (uint)h2);
		}

		// Token: 0x04000244 RID: 580
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
