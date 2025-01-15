using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x02000017 RID: 23
	[NullableContext(2)]
	[Nullable(0)]
	[DataContract]
	public sealed class GatewayMashupStatus : AdoStatus
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002B5F File Offset: 0x00000D5F
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002B67 File Offset: 0x00000D67
		[Nullable(new byte[] { 2, 1 })]
		[DataMember(Name = "gatewayMashupDataSourceProgress")]
		public GatewayMashupDataSourceProgress[] MashupDataSourceProgress
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002B70 File Offset: 0x00000D70
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002B78 File Offset: 0x00000D78
		[DataMember(Name = "gatewayMashupCounters")]
		public GatewayMashupCounters MashupCounters { get; set; }
	}
}
