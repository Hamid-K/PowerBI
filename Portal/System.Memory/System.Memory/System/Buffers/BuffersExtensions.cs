using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x0200001D RID: 29
	public static class BuffersExtensions
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000A32C File Offset: 0x0000852C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SequencePosition? PositionOf<T>(this ReadOnlySequence<T> source, T value) where T : IEquatable<T>
		{
			if (!source.IsSingleSegment)
			{
				return BuffersExtensions.PositionOfMultiSegment<T>(in source, value);
			}
			int num = source.First.Span.IndexOf(value);
			if (num != -1)
			{
				return new SequencePosition?(source.GetPosition((long)num));
			}
			return null;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000A37C File Offset: 0x0000857C
		private static SequencePosition? PositionOfMultiSegment<T>(in ReadOnlySequence<T> source, T value) where T : IEquatable<T>
		{
			SequencePosition start = source.Start;
			SequencePosition sequencePosition = start;
			ReadOnlyMemory<T> readOnlyMemory;
			while (source.TryGet(ref start, out readOnlyMemory, true))
			{
				int num = readOnlyMemory.Span.IndexOf(value);
				if (num != -1)
				{
					return new SequencePosition?(source.GetPosition((long)num, sequencePosition));
				}
				if (start.GetObject() == null)
				{
					break;
				}
				sequencePosition = start;
			}
			return null;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A3D8 File Offset: 0x000085D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this ReadOnlySequence<T> source, Span<T> destination)
		{
			if (source.Length > (long)destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.destination);
			}
			if (source.IsSingleSegment)
			{
				source.First.Span.CopyTo(destination);
				return;
			}
			BuffersExtensions.CopyToMultiSegment<T>(in source, destination);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000A424 File Offset: 0x00008624
		private static void CopyToMultiSegment<T>(in ReadOnlySequence<T> sequence, Span<T> destination)
		{
			SequencePosition start = sequence.Start;
			ReadOnlyMemory<T> readOnlyMemory;
			while (sequence.TryGet(ref start, out readOnlyMemory, true))
			{
				ReadOnlySpan<T> span = readOnlyMemory.Span;
				span.CopyTo(destination);
				if (start.GetObject() == null)
				{
					break;
				}
				destination = destination.Slice(span.Length);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000A470 File Offset: 0x00008670
		public static T[] ToArray<T>(this ReadOnlySequence<T> sequence)
		{
			T[] array = new T[sequence.Length];
			(in sequence).CopyTo(array);
			return array;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000A498 File Offset: 0x00008698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Write<T>(this IBufferWriter<T> writer, ReadOnlySpan<T> value)
		{
			Span<T> span = writer.GetSpan(0);
			if (value.Length <= span.Length)
			{
				value.CopyTo(span);
				writer.Advance(value.Length);
				return;
			}
			BuffersExtensions.WriteMultiSegment<T>(writer, in value, span);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000A4DC File Offset: 0x000086DC
		private static void WriteMultiSegment<T>(IBufferWriter<T> writer, in ReadOnlySpan<T> source, Span<T> destination)
		{
			ReadOnlySpan<T> readOnlySpan = source;
			for (;;)
			{
				int num = Math.Min(destination.Length, readOnlySpan.Length);
				readOnlySpan.Slice(0, num).CopyTo(destination);
				writer.Advance(num);
				readOnlySpan = readOnlySpan.Slice(num);
				if (readOnlySpan.Length <= 0)
				{
					break;
				}
				destination = writer.GetSpan(readOnlySpan.Length);
			}
		}
	}
}
