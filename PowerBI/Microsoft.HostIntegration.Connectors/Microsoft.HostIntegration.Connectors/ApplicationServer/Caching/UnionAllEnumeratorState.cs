using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200024D RID: 589
	[DataContract]
	internal class UnionAllEnumeratorState : EnumeratorState
	{
		// Token: 0x060013B8 RID: 5048 RVA: 0x0003DDC9 File Offset: 0x0003BFC9
		internal UnionAllEnumeratorState(List<object[]> listKeys, DMMultiLevelHashTable hashTable)
		{
			if (!UnionAllEnumeratorState.VerifyListKeysType(listKeys))
			{
				throw new ArgumentException("keys must be DataCacheTags only");
			}
			this._listKeys = listKeys;
			this._currentState = (FindEnumeratorState)hashTable.FindWithStatelessEnumerator(this._listKeys[0]);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0003DE08 File Offset: 0x0003C008
		internal UnionAllEnumeratorState(int currentIndex, List<object[]> listKeys, FindEnumeratorState currentState, bool exhausted)
		{
			this._currentIndex = currentIndex;
			this._listKeys = listKeys;
			this._currentState = currentState;
			this._exhausted = exhausted;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0003DE2D File Offset: 0x0003C02D
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x0003DE35 File Offset: 0x0003C035
		internal bool Exhausted
		{
			get
			{
				return this._exhausted;
			}
			set
			{
				this._exhausted = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0003DE3E File Offset: 0x0003C03E
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x0003DE46 File Offset: 0x0003C046
		internal int CurrentIndex
		{
			get
			{
				return this._currentIndex;
			}
			set
			{
				this._currentIndex = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0003DE4F File Offset: 0x0003C04F
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x0003DE57 File Offset: 0x0003C057
		internal FindEnumeratorState CurrentState
		{
			get
			{
				return this._currentState;
			}
			set
			{
				this._currentState = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0003DE60 File Offset: 0x0003C060
		internal List<object[]> ListKeys
		{
			get
			{
				return this._listKeys;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0003DE68 File Offset: 0x0003C068
		internal override EnumeratorStateType EnumeratorStateType
		{
			get
			{
				return EnumeratorStateType.EnumeratorStateForUnionAll;
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0003DE6B File Offset: 0x0003C06B
		private static bool VerifyListKeysType(IEnumerable<object[]> listKeys)
		{
			if (listKeys == null)
			{
				return true;
			}
			return listKeys.All(delegate(object[] listKey)
			{
				if (listKey != null)
				{
					return listKey.All((object key) => key == null || key is DataCacheTag);
				}
				return true;
			});
		}

		// Token: 0x04000BE3 RID: 3043
		[DataMember]
		private bool _exhausted;

		// Token: 0x04000BE4 RID: 3044
		[DataMember]
		private List<object[]> _listKeys;

		// Token: 0x04000BE5 RID: 3045
		[DataMember]
		private int _currentIndex;

		// Token: 0x04000BE6 RID: 3046
		[DataMember]
		private FindEnumeratorState _currentState;
	}
}
