using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000068 RID: 104
	[DataContract]
	public class OAuthNonce
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000460A File Offset: 0x0000280A
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00004612 File Offset: 0x00002812
		[DataMember]
		public OAuthBrowserNavigationInfo NavigationInfo { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000461B File Offset: 0x0000281B
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00004623 File Offset: 0x00002823
		[DataMember]
		public OAuthSettings OAuthSettings { get; set; }
	}
}
