using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x02000018 RID: 24
	[NullableContext(2)]
	[Nullable(0)]
	[DataContract]
	public sealed class GatewayMashupDataSourceProgress
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002B89 File Offset: 0x00000D89
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002B91 File Offset: 0x00000D91
		[DataMember(Name = "gatewayMashupDataSource")]
		public GatewayMashupDataSource MashupDataSource { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002B9A File Offset: 0x00000D9A
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002BA2 File Offset: 0x00000DA2
		[DataMember(Name = "rows")]
		public int? Rows { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002BAB File Offset: 0x00000DAB
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002BB3 File Offset: 0x00000DB3
		[DataMember(Name = "bytes")]
		public long? Bytes { get; set; }
	}
}
