using System;
using System.Collections;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000206 RID: 518
	internal sealed class BaseHashTableKeyCollection : ICollection, IEnumerable
	{
		// Token: 0x060010EC RID: 4332 RVA: 0x00037F72 File Offset: 0x00036172
		internal BaseHashTableKeyCollection(BaseHashTable parentHashTable)
		{
			this._parentHashTable = parentHashTable;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00037F84 File Offset: 0x00036184
		public int Count
		{
			get
			{
				int num = 0;
				IEnumerator keyEnumerator = this._parentHashTable.GetKeyEnumerator();
				while (keyEnumerator.MoveNext())
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00003CAB File Offset: 0x00001EAB
		public bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00003CAB File Offset: 0x00001EAB
		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00037FAE File Offset: 0x000361AE
		public IEnumerator GetEnumerator()
		{
			return this._parentHashTable.GetKeyEnumerator();
		}

		// Token: 0x04000ADC RID: 2780
		private BaseHashTable _parentHashTable;
	}
}
