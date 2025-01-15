using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000036 RID: 54
	[DataContract]
	public class CustomConnectorsMetadata
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003363 File Offset: 0x00001563
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000336B File Offset: 0x0000156B
		[DataMember(Name = "metadata", Order = 0)]
		public IList<CustomConnectorMetadata> Metadata { get; set; }
	}
}
