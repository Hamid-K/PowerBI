using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000050 RID: 80
	[CLSCompliant(false)]
	public sealed class RemoteArrayWrapper<T> : MarshalByRefObject, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00002D07 File Offset: 0x00000F07
		public RemoteArrayWrapper(params T[] array)
		{
			this.m_array = array;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00002D16 File Offset: 0x00000F16
		public int IndexOf(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00002D1D File Offset: 0x00000F1D
		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000F1 RID: 241
		public T this[int index]
		{
			get
			{
				return this.m_array[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00002D39 File Offset: 0x00000F39
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002D40 File Offset: 0x00000F40
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00002D47 File Offset: 0x00000F47
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00002D4E File Offset: 0x00000F4E
		public bool Contains(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00002D55 File Offset: 0x00000F55
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_array.CopyTo(array, arrayIndex);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00002D64 File Offset: 0x00000F64
		public int Count
		{
			get
			{
				return this.m_array.Length;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00002D6E File Offset: 0x00000F6E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002D71 File Offset: 0x00000F71
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00002D78 File Offset: 0x00000F78
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.m_array.Length; i = num)
			{
				yield return this.m_array[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00002D87 File Offset: 0x00000F87
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.m_array.Length; i = num)
			{
				yield return this.m_array[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x0400007C RID: 124
		private readonly T[] m_array;
	}
}
