using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000200 RID: 512
	[DataContract]
	internal class BaseEnumeratorState : EnumeratorState
	{
		// Token: 0x060010A2 RID: 4258 RVA: 0x00037568 File Offset: 0x00035768
		internal BaseEnumeratorState(object parent, long compactionEpoch)
		{
			this._parentID = parent.GetHashCode();
			this._creationCompactionEpoch = compactionEpoch;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00037583 File Offset: 0x00035783
		internal BaseEnumeratorState(uint path, int index, bool exhaused, long creationCompactionEpoch, int parentId)
		{
			this._path = path;
			this._index = index;
			this._exhausted = exhaused;
			this._creationCompactionEpoch = creationCompactionEpoch;
			this._parentID = parentId;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x000375B0 File Offset: 0x000357B0
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x000375B8 File Offset: 0x000357B8
		internal uint Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x000375C1 File Offset: 0x000357C1
		// (set) Token: 0x060010A7 RID: 4263 RVA: 0x000375C9 File Offset: 0x000357C9
		internal int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x000375D2 File Offset: 0x000357D2
		// (set) Token: 0x060010A9 RID: 4265 RVA: 0x000375DA File Offset: 0x000357DA
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

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x000375E3 File Offset: 0x000357E3
		internal long CreationCompactionEpoch
		{
			get
			{
				return this._creationCompactionEpoch;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x000375EB File Offset: 0x000357EB
		internal int ParentId
		{
			get
			{
				return this._parentID;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override EnumeratorStateType EnumeratorStateType
		{
			get
			{
				return EnumeratorStateType.BaseEnumeratorState;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000375F3 File Offset: 0x000357F3
		internal bool IsValidState(long lastCompactionEpoch)
		{
			return this._creationCompactionEpoch == lastCompactionEpoch;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000375FE File Offset: 0x000357FE
		internal bool IsValidState(object parentHashTable)
		{
			return parentHashTable.GetHashCode() == this._parentID;
		}

		// Token: 0x04000AD0 RID: 2768
		[DataMember]
		private uint _path;

		// Token: 0x04000AD1 RID: 2769
		[DataMember]
		private bool _exhausted;

		// Token: 0x04000AD2 RID: 2770
		[DataMember]
		private int _index;

		// Token: 0x04000AD3 RID: 2771
		[DataMember]
		private readonly long _creationCompactionEpoch;

		// Token: 0x04000AD4 RID: 2772
		[DataMember]
		private readonly int _parentID;
	}
}
