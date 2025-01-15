using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000220 RID: 544
	internal class DMMultiLevelUnionEnumerator : IEnumerator
	{
		// Token: 0x0600120C RID: 4620 RVA: 0x000398FA File Offset: 0x00037AFA
		internal DMMultiLevelUnionEnumerator(List<IEnumerator> list, IDirectoryNodeFactory directoryNodeFactory)
		{
			this._hashTable = DataStructureFactory.CreateBaseHashTable(directoryNodeFactory);
			this._list = list;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00039915 File Offset: 0x00037B15
		public object Current
		{
			get
			{
				if (this._current != null)
				{
					return this._current;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0003992C File Offset: 0x00037B2C
		public bool MoveNext()
		{
			this._current = null;
			while (this._currentIndex < this._list.Count)
			{
				IEnumerator enumerator = this._list[this._currentIndex];
				while (enumerator.MoveNext())
				{
					if (this._hashTable.Upsert(enumerator.Current, DMGlobal.TempObject) == null)
					{
						this._current = enumerator.Current;
						return true;
					}
				}
				this._currentIndex++;
			}
			this._hashTable = null;
			return false;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B0F RID: 2831
		private List<IEnumerator> _list;

		// Token: 0x04000B10 RID: 2832
		private int _currentIndex;

		// Token: 0x04000B11 RID: 2833
		private IBaseHashTable _hashTable;

		// Token: 0x04000B12 RID: 2834
		private object _current;
	}
}
