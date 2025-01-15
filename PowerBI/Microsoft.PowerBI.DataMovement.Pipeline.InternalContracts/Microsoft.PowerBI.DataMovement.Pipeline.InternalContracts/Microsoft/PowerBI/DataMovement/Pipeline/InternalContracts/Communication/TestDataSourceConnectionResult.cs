using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200006F RID: 111
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public class TestDataSourceConnectionResult : DatabaseResultBase
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00003308 File Offset: 0x00001508
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00003310 File Offset: 0x00001510
		[DataMember(Name = "serverAnnotation")]
		public ServerAnnotation ServerAnnotation { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00003319 File Offset: 0x00001519
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00003321 File Offset: 0x00001521
		[DataMember(Name = "gatewayStaticCapabilities", IsRequired = false)]
		public GatewayStaticCapabilities GatewayStaticCapabilities { get; set; }
	}
}
