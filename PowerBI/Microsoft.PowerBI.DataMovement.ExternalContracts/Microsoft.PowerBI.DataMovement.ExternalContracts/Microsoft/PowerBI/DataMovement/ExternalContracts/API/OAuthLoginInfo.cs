using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000067 RID: 103
	[DataContract]
	public class OAuthLoginInfo
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000045E0 File Offset: 0x000027E0
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x000045E8 File Offset: 0x000027E8
		[DataMember]
		public string OAuthEndpoint { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000045F1 File Offset: 0x000027F1
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x000045F9 File Offset: 0x000027F9
		[DataMember]
		public string OAuthNonce { get; set; }
	}
}
