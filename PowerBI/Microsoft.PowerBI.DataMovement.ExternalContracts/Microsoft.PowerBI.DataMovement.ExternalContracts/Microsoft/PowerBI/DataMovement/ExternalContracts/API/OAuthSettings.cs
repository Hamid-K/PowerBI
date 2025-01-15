using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000069 RID: 105
	[DataContract]
	public class OAuthSettings
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00004634 File Offset: 0x00002834
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000463C File Offset: 0x0000283C
		[DataMember]
		public string CallbackUrl { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00004645 File Offset: 0x00002845
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000464D File Offset: 0x0000284D
		[DataMember]
		public string ClientId { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00004656 File Offset: 0x00002856
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000465E File Offset: 0x0000285E
		[DataMember]
		public string ClientSecret { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00004667 File Offset: 0x00002867
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000466F File Offset: 0x0000286F
		[DataMember]
		public Collection<string> Scopes { get; set; }
	}
}
