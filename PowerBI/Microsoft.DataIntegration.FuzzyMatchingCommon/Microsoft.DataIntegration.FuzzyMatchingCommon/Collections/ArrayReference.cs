using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000094 RID: 148
	public struct ArrayReference<T> : IArray<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x000232FD File Offset: 0x000214FD
		public ArrayReference(T[] array)
		{
			this.Array = array;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00023306 File Offset: 0x00021506
		public int Length
		{
			get
			{
				return this.Array.Length;
			}
		}

		// Token: 0x170000FA RID: 250
		public T this[int i]
		{
			get
			{
				return this.Array[i];
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0002331E File Offset: 0x0002151E
		public IEnumerator<T> GetEnumerator()
		{
			return this.GetEnumerator<T>();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00023330 File Offset: 0x00021530
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator<T>();
		}

		// Token: 0x04000144 RID: 324
		public T[] Array;
	}
}
