using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.LoadBalancer
{
	// Token: 0x02000010 RID: 16
	[DataContract]
	public sealed class LoadBalancingClusterSettings : LoadBalancingSettings<LoadBalancingClusterSettings>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000027D6 File Offset: 0x000009D6
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000027DE File Offset: 0x000009DE
		[DataMember(Name = "selector", IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public SelectorType Selector { get; set; }
	}
}
