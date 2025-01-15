using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000071 RID: 113
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class TestGatewayConnectionResult : GatewayResultBase
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000333A File Offset: 0x0000153A
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00003342 File Offset: 0x00001542
		[DataMember(Name = "version", IsRequired = false)]
		public string Version { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000334B File Offset: 0x0000154B
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00003353 File Offset: 0x00001553
		[DataMember(Name = "staticCapabilities", IsRequired = false)]
		public GatewayStaticCapabilities StaticCapabilities { get; set; }
	}
}
