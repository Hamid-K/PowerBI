using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000022 RID: 34
	internal static class ReadOnlySequence
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000B432 File Offset: 0x00009632
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B432 File Offset: 0x00009632
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SegmentToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B432 File Offset: 0x00009632
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B437 File Offset: 0x00009637
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ArrayToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B437 File Offset: 0x00009637
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B432 File Offset: 0x00009632
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int MemoryManagerToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B437 File Offset: 0x00009637
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B437 File Offset: 0x00009637
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int StringToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		// Token: 0x04000077 RID: 119
		public const int FlagBitMask = -2147483648;

		// Token: 0x04000078 RID: 120
		public const int IndexBitMask = 2147483647;

		// Token: 0x04000079 RID: 121
		public const int SegmentStartMask = 0;

		// Token: 0x0400007A RID: 122
		public const int SegmentEndMask = 0;

		// Token: 0x0400007B RID: 123
		public const int ArrayStartMask = 0;

		// Token: 0x0400007C RID: 124
		public const int ArrayEndMask = -2147483648;

		// Token: 0x0400007D RID: 125
		public const int MemoryManagerStartMask = -2147483648;

		// Token: 0x0400007E RID: 126
		public const int MemoryManagerEndMask = 0;

		// Token: 0x0400007F RID: 127
		public const int StringStartMask = -2147483648;

		// Token: 0x04000080 RID: 128
		public const int StringEndMask = -2147483648;
	}
}
