using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000025 RID: 37
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class DiscoverDataSourcesOnGatewayRequest : GatewayRequestBase
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002566 File Offset: 0x00000766
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000256E File Offset: 0x0000076E
		[DataMember(Name = "mashup", IsRequired = true)]
		public string Mashup { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002577 File Offset: 0x00000777
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000257F File Offset: 0x0000077F
		[DataMember(Name = "isMashupPlainText")]
		public bool isMashupPlainText { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002588 File Offset: 0x00000788
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002590 File Offset: 0x00000790
		[DataMember(Name = "outputPowerBIV3Format", IsRequired = false)]
		public bool outputPowerBIV3Format { get; set; }
	}
}
