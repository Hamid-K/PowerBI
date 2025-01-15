using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	public class HeapSegmentAllocator<T> : ISegmentAllocator<T>
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x000236F0 File Offset: 0x000218F0
		public ArraySegment<T> New(int length)
		{
			return new ArraySegment<T>(new T[length], 0, length);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00023700 File Offset: 0x00021900
		public void Resize(ref ArraySegment<T> segment, int newLength)
		{
			if (segment.Offset != 0 || (segment.Array != null && segment.Array.Length != segment.Count))
			{
				throw new ArgumentException("Invalid segment.");
			}
			T[] array = segment.Array;
			Array.Resize<T>(ref array, newLength);
			segment = new ArraySegment<T>(array, 0, newLength);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00023758 File Offset: 0x00021958
		public static void Append(ref ArraySegment<T> segment, T item)
		{
			if (segment.Offset != 0)
			{
				throw new Exception("Segment offset must be zero.");
			}
			if (segment.Array == null)
			{
				segment = new ArraySegment<T>(new T[1], 0, 1);
			}
			else
			{
				T[] array = segment.Array;
				if (segment.Count + 1 >= segment.Array.Length)
				{
					Array.Resize<T>(ref array, segment.Count + 1);
				}
				segment = new ArraySegment<T>(array, 0, segment.Count + 1);
			}
			segment.Array[segment.Count - 1] = item;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000237E8 File Offset: 0x000219E8
		public static void Append(ref ArraySegment<T> segment, ArraySegment<T> items)
		{
			if (segment.Offset != 0)
			{
				throw new Exception("Segment offset must be zero.");
			}
			int count = segment.Count;
			if (segment.Array == null)
			{
				segment = new ArraySegment<T>(new T[items.Count], 0, items.Count);
			}
			else
			{
				T[] array = segment.Array;
				if (segment.Count + items.Count >= segment.Array.Length)
				{
					Array.Resize<T>(ref array, segment.Count + items.Count);
				}
				segment = new ArraySegment<T>(array, 0, segment.Count + items.Count);
			}
			if (items.Count > 0)
			{
				Array.ConstrainedCopy(items.Array, items.Offset, segment.Array, count, items.Count);
			}
		}

		// Token: 0x04000149 RID: 329
		public static readonly HeapSegmentAllocator<T> Instance = new HeapSegmentAllocator<T>();
	}
}
