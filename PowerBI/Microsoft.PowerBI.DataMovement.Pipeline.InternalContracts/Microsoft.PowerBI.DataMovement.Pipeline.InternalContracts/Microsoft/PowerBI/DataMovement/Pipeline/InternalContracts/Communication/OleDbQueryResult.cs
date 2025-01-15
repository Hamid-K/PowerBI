using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000047 RID: 71
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OleDbQueryResult : OleDbQueryResultBase
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00002B55 File Offset: 0x00000D55
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00002B5D File Offset: 0x00000D5D
		[IgnoreDataMember]
		internal IPageReader Reader { get; set; }
	}
}
