using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200022A RID: 554
	[DataContract]
	internal class FixedDepthEnumeratorState : BaseEnumeratorState
	{
		// Token: 0x06001271 RID: 4721 RVA: 0x0003A244 File Offset: 0x00038444
		internal FixedDepthEnumeratorState(object parent, long compactionEpoch)
			: base(parent, compactionEpoch)
		{
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0003A24E File Offset: 0x0003844E
		internal FixedDepthEnumeratorState(int level, BaseEnumeratorState state)
			: base(state.Path, state.Index, state.Exhausted, state.CreationCompactionEpoch, state.ParentId)
		{
			this._level = level;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0003A27B File Offset: 0x0003847B
		// (set) Token: 0x06001274 RID: 4724 RVA: 0x0003A283 File Offset: 0x00038483
		internal int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00036AA6 File Offset: 0x00034CA6
		internal override EnumeratorStateType EnumeratorStateType
		{
			get
			{
				return EnumeratorStateType.FixedDepthEnumeratorState;
			}
		}

		// Token: 0x04000B35 RID: 2869
		[DataMember]
		private int _level;
	}
}
