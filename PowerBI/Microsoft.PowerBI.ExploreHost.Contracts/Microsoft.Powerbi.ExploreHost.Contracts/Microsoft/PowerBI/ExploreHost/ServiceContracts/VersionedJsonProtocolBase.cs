using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExploreHost.ServiceContracts
{
	// Token: 0x02000008 RID: 8
	[DataContract(Name = "VersionedJsonProtocol")]
	public abstract class VersionedJsonProtocolBase
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023FF File Offset: 0x000005FF
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002407 File Offset: 0x00000607
		[DataMember(IsRequired = true, Order = 0)]
		public string Version { get; set; }
	}
}
