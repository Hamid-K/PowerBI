using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OAuthRenewTokenOnGatewayRequest : GatewayRequestBase
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002990 File Offset: 0x00000B90
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002998 File Offset: 0x00000B98
		[DataMember(Name = "dataSourceObjectId", IsRequired = true)]
		public Guid DataSourceObjectId { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000029A1 File Offset: 0x00000BA1
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000029A9 File Offset: 0x00000BA9
		[DataMember(Name = "dataSourceReference", IsRequired = true)]
		public string DataSourceReference { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000029B2 File Offset: 0x00000BB2
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000029BA File Offset: 0x00000BBA
		[DataMember(Name = "oAuthRedirectUrl", IsRequired = true)]
		public string OAuthRedirectUrl { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000029C3 File Offset: 0x00000BC3
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000029CB File Offset: 0x00000BCB
		[DataMember(Name = "oAuthCredentialDetails", IsRequired = true)]
		public CredentialDetails OAuthCredentialDetails { get; set; }

		// Token: 0x040000A6 RID: 166
		[DataMember(Name = "oAuthDetailsCollection", IsRequired = false)]
		public OAuthDetailsCollection OAuthDetailsCollection;
	}
}
