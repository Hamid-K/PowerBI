using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000031 RID: 49
	[DataContract]
	public class CredentialData
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003063 File Offset: 0x00001263
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000306B File Offset: 0x0000126B
		[DataMember(Order = 10, Name = "credentialData")]
		public IList<CredentialNameValuePair> CredentialNameValuePairs { get; set; }
	}
}
