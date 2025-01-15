using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000044 RID: 68
	public readonly struct Int96 : IEquatable<Int96>
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000657C File Offset: 0x0000477C
		public Int96(int a, int b, int c)
		{
			this.A = a;
			this.B = b;
			this.C = c;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006594 File Offset: 0x00004794
		public bool Equals(Int96 other)
		{
			return this.A == other.A && this.B == other.B && this.C == other.C;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000065C8 File Offset: 0x000047C8
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format("{0:X8}{1:X8}{2:X8}", this.A, this.B, this.C);
		}

		// Token: 0x04000075 RID: 117
		public readonly int A;

		// Token: 0x04000076 RID: 118
		public readonly int B;

		// Token: 0x04000077 RID: 119
		public readonly int C;
	}
}
