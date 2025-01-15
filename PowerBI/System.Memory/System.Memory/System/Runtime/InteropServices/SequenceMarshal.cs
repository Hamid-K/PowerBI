using System;
using System.Buffers;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200001A RID: 26
	public static class SequenceMarshal
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00009DD5 File Offset: 0x00007FD5
		public static bool TryGetReadOnlySequenceSegment<T>(ReadOnlySequence<T> sequence, out ReadOnlySequenceSegment<T> startSegment, out int startIndex, out ReadOnlySequenceSegment<T> endSegment, out int endIndex)
		{
			return sequence.TryGetReadOnlySequenceSegment(out startSegment, out startIndex, out endSegment, out endIndex);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009DE3 File Offset: 0x00007FE3
		public static bool TryGetArray<T>(ReadOnlySequence<T> sequence, out ArraySegment<T> segment)
		{
			return sequence.TryGetArray(out segment);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00009DED File Offset: 0x00007FED
		public static bool TryGetReadOnlyMemory<T>(ReadOnlySequence<T> sequence, out ReadOnlyMemory<T> memory)
		{
			if (!sequence.IsSingleSegment)
			{
				memory = default(ReadOnlyMemory<T>);
				return false;
			}
			memory = sequence.First;
			return true;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009E0F File Offset: 0x0000800F
		internal static bool TryGetString(ReadOnlySequence<char> sequence, out string text, out int start, out int length)
		{
			return sequence.TryGetString(out text, out start, out length);
		}
	}
}
