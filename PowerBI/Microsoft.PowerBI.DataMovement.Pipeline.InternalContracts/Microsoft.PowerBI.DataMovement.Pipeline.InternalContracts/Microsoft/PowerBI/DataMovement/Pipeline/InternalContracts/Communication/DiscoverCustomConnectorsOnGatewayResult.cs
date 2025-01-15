using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class DiscoverCustomConnectorsOnGatewayResult : GatewayResultBase
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000260E File Offset: 0x0000080E
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00002616 File Offset: 0x00000816
		[DataMember(Name = "customConnectors", IsRequired = true)]
		public CustomConnectorsMetadata CustomConnectors { get; set; }
	}
}
