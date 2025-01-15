using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public struct ArraySegment32<T> : IArraySegment<T>, IReadWriteArraySegment<T>, IArray<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x00023357 File Offset: 0x00021557
		public ArraySegment32(ArraySegment<T> segment)
		{
			this.Array = segment.Array;
			this.Offset = segment.Offset;
			this.Count = segment.Count;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00023380 File Offset: 0x00021580
		public ArraySegment32(T[] block)
		{
			this.Array = block;
			this.Offset = 0;
			this.Count = block.Length;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00023399 File Offset: 0x00021599
		public ArraySegment32(T[] block, int start, int len)
		{
			this.Array = block;
			this.Offset = start;
			this.Count = len;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000233B0 File Offset: 0x000215B0
		public static implicit operator ArraySegment32<T>(ArraySegment<T> s)
		{
			return new ArraySegment32<T>(s.Array, s.Offset, s.Count);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000233CC File Offset: 0x000215CC
		public static implicit operator ArraySegment<T>(ArraySegment32<T> s)
		{
			return new ArraySegment<T>(s.Array, s.Offset, s.Count);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000233E8 File Offset: 0x000215E8
		public void CopyTo(ArraySegment32<T> dest)
		{
			if (this.Count != dest.Count)
			{
				throw new ArgumentException("ArraySegements must be the same Length.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				dest.Array[dest.Offset + i] = this.Array[this.Offset + i];
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00023448 File Offset: 0x00021648
		public void CopyFrom(T[] sourceArray, int sourceStart, int sourceLength)
		{
			if (this.Count != sourceLength)
			{
				throw new ArgumentException("ArraySegements must be the same Length.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				this.Array[this.Offset + i] = sourceArray[sourceStart + i];
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00023498 File Offset: 0x00021698
		public T[] ToArray()
		{
			T[] array;
			if (this.Count > 0)
			{
				array = new T[this.Count];
				global::System.Array.ConstrainedCopy(this.Array, this.Offset, array, 0, this.Count);
			}
			else
			{
				array = new T[0];
			}
			return array;
		}

		// Token: 0x17000103 RID: 259
		public T this[int i]
		{
			get
			{
				if (i >= this.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.Array[this.Offset + i];
			}
			set
			{
				if (i >= this.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.Array[this.Offset + i] = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00023526 File Offset: 0x00021726
		T[] IArraySegment<T>.Array
		{
			get
			{
				return this.Array;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0002352E File Offset: 0x0002172E
		int IArraySegment<T>.Offset
		{
			get
			{
				return this.Offset;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00023536 File Offset: 0x00021736
		int IArraySegment<T>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0002353E File Offset: 0x0002173E
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00023546 File Offset: 0x00021746
		T[] IReadWriteArraySegment<T>.Array
		{
			get
			{
				return this.Array;
			}
			set
			{
				this.Array = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0002354F File Offset: 0x0002174F
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00023557 File Offset: 0x00021757
		int IReadWriteArraySegment<T>.Offset
		{
			get
			{
				return this.Offset;
			}
			set
			{
				this.Offset = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00023560 File Offset: 0x00021760
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00023568 File Offset: 0x00021768
		int IReadWriteArraySegment<T>.Count
		{
			get
			{
				return this.Count;
			}
			set
			{
				this.Count = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00023571 File Offset: 0x00021771
		int IArray<T>.Length
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002357C File Offset: 0x0002177C
		public static T Read<T>(Stream s) where T : IReadWriteArraySegment<int>, new()
		{
			int num = StreamUtilities.ReadInt32(s);
			if (num == 0)
			{
				return default(T);
			}
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = StreamUtilities.ReadInt32(s);
			}
			T t = new T();
			t.Array = array;
			t.Offset = 0;
			t.Count = num;
			return t;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000235EC File Offset: 0x000217EC
		public void Set(T value)
		{
			for (int i = this.Offset; i < this.Offset + this.Count; i++)
			{
				this.Array[i] = value;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00023624 File Offset: 0x00021824
		public void Clear()
		{
			for (int i = this.Offset; i < this.Offset + this.Count; i++)
			{
				this.Array[i] = default(T);
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00023663 File Offset: 0x00021863
		public override string ToString()
		{
			return ArraySegment32<T>.ToString<T>(this);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00023678 File Offset: 0x00021878
		public static string ToString<T>(IArraySegment<T> array)
		{
			StringBuilder stringBuilder = new StringBuilder(array.Count * 4);
			for (int i = 0; i < array.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" ");
				}
				StringBuilder stringBuilder2 = stringBuilder;
				T t = array[i];
				stringBuilder2.Append(t.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000236D6 File Offset: 0x000218D6
		public IEnumerator<T> GetEnumerator()
		{
			return new ArraySegment32<T>.Enumerator(this);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000236E3 File Offset: 0x000218E3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ArraySegment32<T>.Enumerator(this);
		}

		// Token: 0x04000146 RID: 326
		public T[] Array;

		// Token: 0x04000147 RID: 327
		public int Offset;

		// Token: 0x04000148 RID: 328
		public int Count;

		// Token: 0x02000135 RID: 309
		[Serializable]
		public class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000A13 RID: 2579 RVA: 0x0002E916 File Offset: 0x0002CB16
			public Enumerator(ArraySegment32<T> segment)
			{
				this.m_segment = segment;
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x0002E92C File Offset: 0x0002CB2C
			public Enumerator(IArraySegment<T> segment)
			{
				this.m_segment.Array = segment.Array;
				this.m_segment.Offset = segment.Offset;
				this.m_segment.Count = segment.Count;
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002E979 File Offset: 0x0002CB79
			public T Current
			{
				get
				{
					return this.m_segment[this.m_idx];
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002E98C File Offset: 0x0002CB8C
			object IEnumerator.Current
			{
				get
				{
					return this.m_segment[this.m_idx];
				}
			}

			// Token: 0x06000A17 RID: 2583 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
			public bool MoveNext()
			{
				if (this.m_idx < 2147483647)
				{
					this.m_idx++;
				}
				return this.m_idx < this.m_segment.Count;
			}

			// Token: 0x06000A18 RID: 2584 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
			public void Reset()
			{
				this.m_idx = -1;
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x0002E9DD File Offset: 0x0002CBDD
			public void Dispose()
			{
			}

			// Token: 0x0400031C RID: 796
			public ArraySegment32<T> m_segment;

			// Token: 0x0400031D RID: 797
			private int m_idx = -1;
		}
	}
}
