using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class DiscoverDataSourcesOnGatewayUsingMashupDSRRequest : GatewayRequestBase
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000025A1 File Offset: 0x000007A1
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000025A9 File Offset: 0x000007A9
		[DataMember(Name = "mashupDSR", IsRequired = true)]
		public string MashupDSR { get; set; }
	}
}
