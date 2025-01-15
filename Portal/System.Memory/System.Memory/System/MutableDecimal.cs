using System;

namespace System
{
	// Token: 0x02000017 RID: 23
	internal struct MutableDecimal
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00009B6C File Offset: 0x00007D6C
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00009B7D File Offset: 0x00007D7D
		public bool IsNegative
		{
			get
			{
				return (this.Flags & 2147483648U) > 0U;
			}
			set
			{
				this.Flags = (this.Flags & 2147483647U) | (value ? 2147483648U : 0U);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00009B9D File Offset: 0x00007D9D
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00009BA9 File Offset: 0x00007DA9
		public int Scale
		{
			get
			{
				return (int)((byte)(this.Flags >> 16));
			}
			set
			{
				this.Flags = (this.Flags & 4278255615U) | (uint)((uint)value << 16);
			}
		}

		// Token: 0x04000063 RID: 99
		public uint Flags;

		// Token: 0x04000064 RID: 100
		public uint High;

		// Token: 0x04000065 RID: 101
		public uint Low;

		// Token: 0x04000066 RID: 102
		public uint Mid;

		// Token: 0x04000067 RID: 103
		private const uint SignMask = 2147483648U;

		// Token: 0x04000068 RID: 104
		private const uint ScaleMask = 16711680U;

		// Token: 0x04000069 RID: 105
		private const int ScaleShift = 16;
	}
}
