using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000051 RID: 81
	[CLSCompliant(false)]
	public sealed class RemoteMemberArrayWrapper<TMemberType> : MarshalByRefObject, IList<IMemberNode>, ICollection<IMemberNode>, IEnumerable<IMemberNode>, IEnumerable where TMemberType : IMemberNode
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00002D96 File Offset: 0x00000F96
		public RemoteMemberArrayWrapper(params TMemberType[] array)
		{
			this.m_array = array;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00002DA5 File Offset: 0x00000FA5
		public int IndexOf(IMemberNode item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00002DAC File Offset: 0x00000FAC
		public void Insert(int index, IMemberNode item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000F4 RID: 244
		public IMemberNode this[int index]
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

		// Token: 0x0600017E RID: 382 RVA: 0x00002DCD File Offset: 0x00000FCD
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public void Add(IMemberNode item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00002DDB File Offset: 0x00000FDB
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00002DE2 File Offset: 0x00000FE2
		public bool Contains(IMemberNode item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public void CopyTo(IMemberNode[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public int Count
		{
			get
			{
				return this.m_array.Length;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00002DFA File Offset: 0x00000FFA
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00002DFD File Offset: 0x00000FFD
		public bool Remove(IMemberNode item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00002E04 File Offset: 0x00001004
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

		// Token: 0x06000187 RID: 391 RVA: 0x00002E13 File Offset: 0x00001013
		public IEnumerator<IMemberNode> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.m_array.Length; i = num)
			{
				yield return this.m_array[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x0400007D RID: 125
		private readonly TMemberType[] m_array;
	}
}
