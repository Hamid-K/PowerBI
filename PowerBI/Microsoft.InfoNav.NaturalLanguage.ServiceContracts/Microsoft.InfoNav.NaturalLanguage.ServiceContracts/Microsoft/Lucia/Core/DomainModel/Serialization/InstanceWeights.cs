using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001AE RID: 430
	public sealed class InstanceWeights
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0001194A File Offset: 0x0000FB4A
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x00011952 File Offset: 0x0000FB52
		[JsonProperty(Required = Required.Always)]
		public ConceptualPropertyBinding Binding { get; set; }
	}
}
