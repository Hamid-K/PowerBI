using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003DC RID: 988
	public static class HashHelpers
	{
		// Token: 0x0600161E RID: 5662 RVA: 0x0004112B File Offset: 0x0003F32B
		public static int Combine(int h1, int h2)
		{
			return (((h1 << 5) | (int)((uint)h1 >> 27)) + h1) ^ h2;
		}

		// Token: 0x04000AAC RID: 2732
		public static readonly int RandomSeed = new Random().Next(int.MinValue, int.MaxValue);
	}
}
