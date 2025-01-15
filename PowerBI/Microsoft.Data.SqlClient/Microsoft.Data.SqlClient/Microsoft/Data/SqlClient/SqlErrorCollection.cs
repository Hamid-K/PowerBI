using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200007E RID: 126
	[ListBindable(false)]
	[Serializable]
	public sealed class SqlErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x0001FEA7 File Offset: 0x0001E0A7
		internal SqlErrorCollection()
		{
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001FEBA File Offset: 0x0001E0BA
		public void CopyTo(Array array, int index)
		{
			((ICollection)this._errors).CopyTo(array, index);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001FECC File Offset: 0x0001E0CC
		public void CopyTo(SqlError[] array, int index)
		{
			this._errors.CopyTo(array, index);
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		public int Count
		{
			get
			{
				return this._errors.Count;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0001FEF5 File Offset: 0x0001E0F5
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0001996E File Offset: 0x00017B6E
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700072C RID: 1836
		public SqlError this[int index]
		{
			get
			{
				return (SqlError)this._errors[index];
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001FF0B File Offset: 0x0001E10B
		public IEnumerator GetEnumerator()
		{
			return this._errors.GetEnumerator();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001FF1D File Offset: 0x0001E11D
		internal void Add(SqlError error)
		{
			this._errors.Add(error);
		}

		// Token: 0x040002A1 RID: 673
		private readonly List<object> _errors = new List<object>();
	}
}
