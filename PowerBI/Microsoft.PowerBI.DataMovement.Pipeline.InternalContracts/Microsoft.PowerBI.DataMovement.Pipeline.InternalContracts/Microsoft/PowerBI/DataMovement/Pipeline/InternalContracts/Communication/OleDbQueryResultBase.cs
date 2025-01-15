using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000046 RID: 70
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal abstract class OleDbQueryResultBase : OleDbResultBase
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00002B1A File Offset: 0x00000D1A
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00002B22 File Offset: 0x00000D22
		[IgnoreDataMember]
		internal DbCommand DbCommand { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002B2B File Offset: 0x00000D2B
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00002B33 File Offset: 0x00000D33
		[IgnoreDataMember]
		internal bool WriteColumnOrdinals { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002B3C File Offset: 0x00000D3C
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00002B44 File Offset: 0x00000D44
		[IgnoreDataMember]
		internal IDisposable PooledConnectionLifetimeManager { get; set; }
	}
}
