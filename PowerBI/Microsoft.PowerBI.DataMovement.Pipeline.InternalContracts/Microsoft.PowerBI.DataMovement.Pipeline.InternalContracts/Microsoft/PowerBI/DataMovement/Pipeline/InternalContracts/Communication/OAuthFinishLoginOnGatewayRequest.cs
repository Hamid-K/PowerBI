using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OAuthFinishLoginOnGatewayRequest : GatewayRequestBase
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000028C6 File Offset: 0x00000AC6
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000028CE File Offset: 0x00000ACE
		[DataMember(Name = "oAuthCredentialDetails", IsRequired = true)]
		public CredentialDetails OAuthCredentialDetails { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000028D7 File Offset: 0x00000AD7
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000028DF File Offset: 0x00000ADF
		[DataMember(Name = "dataSourceReference", IsRequired = true)]
		public string DataSourceReference { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000028F0 File Offset: 0x00000AF0
		[DataMember(Name = "oAuthRedirectUrl", IsRequired = true)]
		public string OAuthRedirectUrl { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000028F9 File Offset: 0x00000AF9
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00002901 File Offset: 0x00000B01
		[DataMember(Name = "dataSourceObjectId", IsRequired = false)]
		public Guid? DataSourceObjectId { get; set; }

		// Token: 0x04000099 RID: 153
		[DataMember(Name = "oAuthDetailsCollection", IsRequired = false)]
		public OAuthDetailsCollection OAuthDetailsCollection;
	}
}
