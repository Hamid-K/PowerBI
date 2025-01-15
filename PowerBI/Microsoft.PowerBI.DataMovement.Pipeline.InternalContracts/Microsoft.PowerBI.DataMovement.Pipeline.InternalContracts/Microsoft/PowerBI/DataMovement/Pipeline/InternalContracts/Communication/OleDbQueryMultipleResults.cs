using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000048 RID: 72
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OleDbQueryMultipleResults : OleDbQueryResultBase
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002B6E File Offset: 0x00000D6E
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00002B76 File Offset: 0x00000D76
		[IgnoreDataMember]
		internal IMultipleResults MultipleResults { get; set; }
	}
}
