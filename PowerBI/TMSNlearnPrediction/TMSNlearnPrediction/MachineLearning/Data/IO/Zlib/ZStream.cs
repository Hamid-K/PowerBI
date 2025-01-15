using System;

namespace Microsoft.MachineLearning.Data.IO.Zlib
{
	// Token: 0x020000E5 RID: 229
	internal struct ZStream
	{
		// Token: 0x04000235 RID: 565
		public unsafe byte* next_in;

		// Token: 0x04000236 RID: 566
		public uint avail_in;

		// Token: 0x04000237 RID: 567
		public uint total_in;

		// Token: 0x04000238 RID: 568
		public unsafe byte* next_out;

		// Token: 0x04000239 RID: 569
		public uint avail_out;

		// Token: 0x0400023A RID: 570
		public uint total_out;

		// Token: 0x0400023B RID: 571
		public unsafe byte* msg;

		// Token: 0x0400023C RID: 572
		public IntPtr state;

		// Token: 0x0400023D RID: 573
		public IntPtr zalloc;

		// Token: 0x0400023E RID: 574
		public IntPtr zfree;

		// Token: 0x0400023F RID: 575
		public IntPtr opaque;

		// Token: 0x04000240 RID: 576
		public int data_type;

		// Token: 0x04000241 RID: 577
		public uint adler;

		// Token: 0x04000242 RID: 578
		public uint reserved;
	}
}
