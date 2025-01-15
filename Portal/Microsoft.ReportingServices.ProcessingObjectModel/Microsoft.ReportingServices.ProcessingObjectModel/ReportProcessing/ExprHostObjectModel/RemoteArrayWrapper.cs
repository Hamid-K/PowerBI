using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000024 RID: 36
	[CLSCompliant(false)]
	public sealed class RemoteArrayWrapper<T> : MarshalByRefObject, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000235C File Offset: 0x0000055C
		public RemoteArrayWrapper(params T[] array)
		{
			this.m_array = array;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000236B File Offset: 0x0000056B
		public int IndexOf(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002372 File Offset: 0x00000572
		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000062 RID: 98
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

		// Token: 0x0600009E RID: 158 RVA: 0x0000238E File Offset: 0x0000058E
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002395 File Offset: 0x00000595
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000239C File Offset: 0x0000059C
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000023A3 File Offset: 0x000005A3
		public bool Contains(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000023AA File Offset: 0x000005AA
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_array.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000023B9 File Offset: 0x000005B9
		public int Count
		{
			get
			{
				return this.m_array.Length;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000023C3 File Offset: 0x000005C3
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000023C6 File Offset: 0x000005C6
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000023CD File Offset: 0x000005CD
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

		// Token: 0x060000A7 RID: 167 RVA: 0x000023DC File Offset: 0x000005DC
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

		// Token: 0x04000005 RID: 5
		private readonly T[] m_array;
	}
}
