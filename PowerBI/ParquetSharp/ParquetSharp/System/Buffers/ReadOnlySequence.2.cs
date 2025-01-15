using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x020000E0 RID: 224
	internal static class ReadOnlySequence
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x00022BE4 File Offset: 0x00020DE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00022BEC File Offset: 0x00020DEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00022BF4 File Offset: 0x00020DF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00022BFC File Offset: 0x00020DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00022C08 File Offset: 0x00020E08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00022C14 File Offset: 0x00020E14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00022C1C File Offset: 0x00020E1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00022C28 File Offset: 0x00020E28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x0400024F RID: 591
		public const int FlagBitMask = -2147483648;

		// Token: 0x04000250 RID: 592
		public const int IndexBitMask = 2147483647;

		// Token: 0x04000251 RID: 593
		public const int SegmentStartMask = 0;

		// Token: 0x04000252 RID: 594
		public const int SegmentEndMask = 0;

		// Token: 0x04000253 RID: 595
		public const int ArrayStartMask = 0;

		// Token: 0x04000254 RID: 596
		public const int ArrayEndMask = -2147483648;

		// Token: 0x04000255 RID: 597
		public const int MemoryManagerStartMask = -2147483648;

		// Token: 0x04000256 RID: 598
		public const int MemoryManagerEndMask = 0;

		// Token: 0x04000257 RID: 599
		public const int StringStartMask = -2147483648;

		// Token: 0x04000258 RID: 600
		public const int StringEndMask = -2147483648;
	}
}
