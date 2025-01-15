using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000074 RID: 116
	[DataContract]
	public class ReEncryptCredentialResponse
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000483F File Offset: 0x00002A3F
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00004847 File Offset: 0x00002A47
		[DataMember(Name = "gatewayNameToErrorDictionary", Order = 10)]
		public Dictionary<string, string> GatewayNameToErrorDictionary { get; set; }
	}
}
