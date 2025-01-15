using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000033 RID: 51
	[DataContract]
	public sealed class CredentialNameValuePair
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000325B File Offset: 0x0000145B
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00003263 File Offset: 0x00001463
		[DataMember(Order = 10, Name = "name")]
		public string Name { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000326C File Offset: 0x0000146C
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00003274 File Offset: 0x00001474
		[DataMember(Order = 20, Name = "value")]
		public string Value { get; set; }
	}
}
