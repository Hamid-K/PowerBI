using System;
using System.Buffers;

namespace System.Runtime.InteropServices
{
	// Token: 0x020000D8 RID: 216
	internal static class SequenceMarshal
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x000212D8 File Offset: 0x0001F4D8
		public static bool TryGetReadOnlySequenceSegment<T>(ReadOnlySequence<T> sequence, out ReadOnlySequenceSegment<T> startSegment, out int startIndex, out ReadOnlySequenceSegment<T> endSegment, out int endIndex)
		{
			return sequence.TryGetReadOnlySequenceSegment(out startSegment, out startIndex, out endSegment, out endIndex);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000212E8 File Offset: 0x0001F4E8
		public static bool TryGetArray<T>(ReadOnlySequence<T> sequence, out ArraySegment<T> segment)
		{
			return sequence.TryGetArray(out segment);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000212F4 File Offset: 0x0001F4F4
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

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002131C File Offset: 0x0001F51C
		internal static bool TryGetString(ReadOnlySequence<char> sequence, out string text, out int start, out int length)
		{
			return sequence.TryGetString(out text, out start, out length);
		}
	}
}
