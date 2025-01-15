using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OAuthInitiateLoginOnGatewayRequest : GatewayRequestBase
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000293C File Offset: 0x00000B3C
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00002944 File Offset: 0x00000B44
		[DataMember(Name = "dataSourceReference", IsRequired = true)]
		public string DataSourceReference { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000294D File Offset: 0x00000B4D
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00002955 File Offset: 0x00000B55
		[DataMember(Name = "oAuthRedirectUrl", IsRequired = true)]
		public string OAuthRedirectUrl { get; set; }

		// Token: 0x0400009F RID: 159
		[DataMember(Name = "oAuthDetailsCollection", IsRequired = false)]
		public OAuthDetailsCollection OAuthDetailsCollection;
	}
}
