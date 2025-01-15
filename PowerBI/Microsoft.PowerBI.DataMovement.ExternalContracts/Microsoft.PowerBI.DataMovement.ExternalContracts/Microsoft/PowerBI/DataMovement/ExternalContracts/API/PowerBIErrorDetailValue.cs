using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000070 RID: 112
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class PowerBIErrorDetailValue
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00004807 File Offset: 0x00002A07
		public PowerBIErrorDetailValue(PowerBIErrorResourceType resourceType, string resourceValue)
		{
			this.ResourceType = resourceType;
			this.ResourceValue = resourceValue;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000481D File Offset: 0x00002A1D
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00004825 File Offset: 0x00002A25
		[DataMember(IsRequired = true, Order = 10, Name = "type")]
		public PowerBIErrorResourceType ResourceType { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000482E File Offset: 0x00002A2E
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00004836 File Offset: 0x00002A36
		[DataMember(IsRequired = true, Order = 20, Name = "value")]
		public string ResourceValue { get; private set; }
	}
}
