using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x020000DB RID: 219
	internal static class BuffersExtensions
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x000218F8 File Offset: 0x0001FAF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SequencePosition? PositionOf<T>([System.Memory.IsReadOnly] [In] this ReadOnlySequence<T> source, T value) where T : object, IEquatable<T>
		{
			if (!source.IsSingleSegment)
			{
				return BuffersExtensions.PositionOfMultiSegment<T>(ref source, value);
			}
			int num = source.First.Span.IndexOf(value);
			if (num != -1)
			{
				return new SequencePosition?(source.GetPosition((long)num));
			}
			return null;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00021950 File Offset: 0x0001FB50
		private static SequencePosition? PositionOfMultiSegment<T>([System.Memory.IsReadOnly] [In] ref ReadOnlySequence<T> source, T value) where T : object, IEquatable<T>
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

		// Token: 0x060007BE RID: 1982 RVA: 0x000219B8 File Offset: 0x0001FBB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>([System.Memory.IsReadOnly] [In] this ReadOnlySequence<T> source, Span<T> destination)
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
			BuffersExtensions.CopyToMultiSegment<T>(ref source, destination);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00021A0C File Offset: 0x0001FC0C
		private static void CopyToMultiSegment<T>([System.Memory.IsReadOnly] [In] ref ReadOnlySequence<T> sequence, Span<T> destination)
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

		// Token: 0x060007C0 RID: 1984 RVA: 0x00021A64 File Offset: 0x0001FC64
		public static T[] ToArray<T>([System.Memory.IsReadOnly] [In] this ReadOnlySequence<T> sequence)
		{
			T[] array = new T[sequence.Length];
			(ref sequence).CopyTo(array);
			return array;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00021A90 File Offset: 0x0001FC90
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
			BuffersExtensions.WriteMultiSegment<T>(writer, ref value, span);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00021ADC File Offset: 0x0001FCDC
		private static void WriteMultiSegment<T>(IBufferWriter<T> writer, [System.Memory.IsReadOnly] [In] ref ReadOnlySpan<T> source, Span<T> destination)
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
