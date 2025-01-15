using System;

namespace System
{
	// Token: 0x020000D5 RID: 213
	internal struct MutableDecimal
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00021004 File Offset: 0x0001F204
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x00021018 File Offset: 0x0001F218
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00021040 File Offset: 0x0001F240
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0002104C File Offset: 0x0001F24C
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

		// Token: 0x0400023B RID: 571
		public uint Flags;

		// Token: 0x0400023C RID: 572
		public uint High;

		// Token: 0x0400023D RID: 573
		public uint Low;

		// Token: 0x0400023E RID: 574
		public uint Mid;

		// Token: 0x0400023F RID: 575
		private const uint SignMask = 2147483648U;

		// Token: 0x04000240 RID: 576
		private const uint ScaleMask = 16711680U;

		// Token: 0x04000241 RID: 577
		private const int ScaleShift = 16;
	}
}
