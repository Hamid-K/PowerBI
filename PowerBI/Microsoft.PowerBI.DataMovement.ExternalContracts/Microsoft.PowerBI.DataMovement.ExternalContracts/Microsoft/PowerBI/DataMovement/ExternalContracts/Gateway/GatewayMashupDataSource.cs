using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x02000019 RID: 25
	[NullableContext(2)]
	[Nullable(0)]
	[DataContract]
	public sealed class GatewayMashupDataSource
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002BC4 File Offset: 0x00000DC4
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002BCC File Offset: 0x00000DCC
		[DataMember(Name = "kind")]
		public string Kind { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002BD5 File Offset: 0x00000DD5
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002BDD File Offset: 0x00000DDD
		[DataMember(Name = "path")]
		public string Path { get; set; }
	}
}
