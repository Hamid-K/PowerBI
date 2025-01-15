using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200021F RID: 543
	internal sealed class DMMultiLevelIntersectionEnumerator : IEnumerator
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x000397E8 File Offset: 0x000379E8
		internal DMMultiLevelIntersectionEnumerator(List<IEnumerator> list, IDirectoryNodeFactory _directoryNodeFactory)
		{
			IBaseHashTable baseHashTable = DataStructureFactory.CreateBaseHashTable(_directoryNodeFactory);
			IEnumerator enumerator = list[0];
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				baseHashTable.Upsert(obj, DMGlobal.TempObject);
			}
			for (int i = 1; i < list.Count - 1; i++)
			{
				enumerator = list[i];
				IBaseHashTable baseHashTable2 = DataStructureFactory.CreateBaseHashTable(_directoryNodeFactory);
				while (enumerator.MoveNext())
				{
					if (baseHashTable.ContainsKey(enumerator.Current))
					{
						baseHashTable2.Upsert(enumerator.Current, DMGlobal.TempObject);
					}
				}
				baseHashTable = baseHashTable2;
			}
			this._hashTable = baseHashTable;
			this._lastEnumerator = list[list.Count - 1];
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00039893 File Offset: 0x00037A93
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

		// Token: 0x0600120A RID: 4618 RVA: 0x000398AC File Offset: 0x00037AAC
		public bool MoveNext()
		{
			IEnumerator lastEnumerator = this._lastEnumerator;
			this._current = null;
			while (lastEnumerator.MoveNext())
			{
				if (this._hashTable.ContainsKey(lastEnumerator.Current))
				{
					this._current = lastEnumerator.Current;
					return true;
				}
			}
			this._hashTable = null;
			return false;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B0C RID: 2828
		private readonly IEnumerator _lastEnumerator;

		// Token: 0x04000B0D RID: 2829
		private object _current;

		// Token: 0x04000B0E RID: 2830
		private IBaseHashTable _hashTable;
	}
}
