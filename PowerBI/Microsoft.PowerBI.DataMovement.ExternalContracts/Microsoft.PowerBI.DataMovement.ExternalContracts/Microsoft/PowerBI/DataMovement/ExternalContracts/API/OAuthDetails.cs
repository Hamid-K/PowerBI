using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000065 RID: 101
	[DataContract]
	public class OAuthDetails
	{
		// Token: 0x0400024B RID: 587
		[DataMember(Name = "dataSourceKinds", Order = 10, IsRequired = true)]
		public IList<string> DataSourceKinds;

		// Token: 0x0400024C RID: 588
		[DataMember(Name = "clientId", Order = 20, IsRequired = true)]
		public string ClientId;

		// Token: 0x0400024D RID: 589
		[DataMember(Name = "secret", Order = 30, IsRequired = true)]
		public string Secret;
	}
}
