using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000229 RID: 553
	[DataContract]
	internal class FindEnumeratorState : EnumeratorState
	{
		// Token: 0x06001264 RID: 4708 RVA: 0x0003A140 File Offset: 0x00038340
		internal FindEnumeratorState(object parent, object[] keys, List<FixedDepthEnumeratorState> list, BaseEnumeratorState state)
		{
			if (keys != null)
			{
				if (keys.Any((object key) => key != null && !(key is DataCacheTag)))
				{
					throw new ArgumentException("All keys must be of type DataCacheTag");
				}
			}
			this._keys = keys;
			this._parentID = parent.GetHashCode();
			this._currentEnumeratorState = state;
			this._list = list;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0003A1A8 File Offset: 0x000383A8
		internal FindEnumeratorState()
		{
			this._exhausted = true;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0003A1B7 File Offset: 0x000383B7
		internal FindEnumeratorState(int parentId, object[] keys, List<FixedDepthEnumeratorState> list, BaseEnumeratorState state, bool exhausted)
		{
			this._parentID = parentId;
			this._keys = keys;
			this._list = list;
			this._currentEnumeratorState = state;
			this._exhausted = exhausted;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0003A1E4 File Offset: 0x000383E4
		internal bool IsValidState(object parent)
		{
			return parent.GetHashCode() == this._parentID;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0003A1F4 File Offset: 0x000383F4
		internal object[] Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0003A1FC File Offset: 0x000383FC
		internal List<FixedDepthEnumeratorState> List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0003A204 File Offset: 0x00038404
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x0003A20C File Offset: 0x0003840C
		internal BaseEnumeratorState CurrentState
		{
			get
			{
				return this._currentEnumeratorState;
			}
			set
			{
				this._currentEnumeratorState = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0003A215 File Offset: 0x00038415
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x0003A21D File Offset: 0x0003841D
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

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0003A226 File Offset: 0x00038426
		internal int ParentId
		{
			get
			{
				return this._parentID;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0003A22E File Offset: 0x0003842E
		internal override EnumeratorStateType EnumeratorStateType
		{
			get
			{
				return EnumeratorStateType.FindEnumeratorState;
			}
		}

		// Token: 0x04000B2F RID: 2863
		[DataMember]
		private readonly object[] _keys;

		// Token: 0x04000B30 RID: 2864
		[DataMember]
		private readonly List<FixedDepthEnumeratorState> _list;

		// Token: 0x04000B31 RID: 2865
		[DataMember]
		private BaseEnumeratorState _currentEnumeratorState;

		// Token: 0x04000B32 RID: 2866
		[DataMember]
		private readonly int _parentID;

		// Token: 0x04000B33 RID: 2867
		[DataMember]
		private bool _exhausted;
	}
}
