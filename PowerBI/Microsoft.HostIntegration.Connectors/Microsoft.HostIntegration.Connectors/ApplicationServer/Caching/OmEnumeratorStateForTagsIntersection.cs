using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000283 RID: 643
	[DataContract(Name = "OmEnumeratorStateForTagsIntersection", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class OmEnumeratorStateForTagsIntersection : EnumeratorState
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x000452CC File Offset: 0x000434CC
		public OmEnumeratorStateForTagsIntersection(EnumeratorState enumState, DataCacheTag[] tags)
		{
			this._enumState = enumState;
			this._tags = tags;
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000452E2 File Offset: 0x000434E2
		public EnumeratorState EnumState
		{
			get
			{
				return this._enumState;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x000452EA File Offset: 0x000434EA
		public DataCacheTag[] Tags
		{
			get
			{
				return this._tags;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x000373C9 File Offset: 0x000355C9
		internal override EnumeratorStateType EnumeratorStateType
		{
			get
			{
				return EnumeratorStateType.EnumeratorStateForTagsIntersection;
			}
		}

		// Token: 0x04000CBC RID: 3260
		[DataMember]
		private EnumeratorState _enumState;

		// Token: 0x04000CBD RID: 3261
		[DataMember]
		private DataCacheTag[] _tags;
	}
}
