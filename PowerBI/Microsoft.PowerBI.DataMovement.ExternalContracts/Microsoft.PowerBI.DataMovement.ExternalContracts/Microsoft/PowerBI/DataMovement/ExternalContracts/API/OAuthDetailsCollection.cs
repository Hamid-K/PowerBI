using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000066 RID: 102
	[DataContract]
	public class OAuthDetailsCollection
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000045B6 File Offset: 0x000027B6
		// (set) Token: 0x060002EF RID: 751 RVA: 0x000045BE File Offset: 0x000027BE
		[DataMember(Name = "oAuthDetailsForDataSourceKind", Order = 10)]
		public IList<OAuthDetails> OAuthDetailsForDataSourceKind { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000045C7 File Offset: 0x000027C7
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x000045CF File Offset: 0x000027CF
		[DataMember(Name = "oAuthDetailsForAAD", Order = 20)]
		public OAuthDetails OAuthDetailsForAAD { get; set; }
	}
}
