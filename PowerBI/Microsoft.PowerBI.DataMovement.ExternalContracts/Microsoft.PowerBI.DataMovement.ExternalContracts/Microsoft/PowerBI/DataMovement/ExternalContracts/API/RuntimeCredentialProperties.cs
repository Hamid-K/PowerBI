using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000078 RID: 120
	[DataContract]
	public class RuntimeCredentialProperties
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00004983 File Offset: 0x00002B83
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000498B File Offset: 0x00002B8B
		[DataMember(Order = 10, Name = "runtimeCredentialProperties")]
		public IList<CredentialNameValuePair> CredentialNameValuePairs { get; set; }
	}
}
