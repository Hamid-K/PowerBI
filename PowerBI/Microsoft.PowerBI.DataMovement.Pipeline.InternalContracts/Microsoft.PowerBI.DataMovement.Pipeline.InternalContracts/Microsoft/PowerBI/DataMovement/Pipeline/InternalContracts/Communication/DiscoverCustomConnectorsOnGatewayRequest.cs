using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class DiscoverCustomConnectorsOnGatewayRequest : GatewayRequestBase
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000025F5 File Offset: 0x000007F5
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000025FD File Offset: 0x000007FD
		[DataMember(Name = "locale", IsRequired = true)]
		public string Locale { get; set; }
	}
}
