using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001AC RID: 428
	public sealed class Instances
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000118C3 File Offset: 0x0000FAC3
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x000118CB File Offset: 0x0000FACB
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public InstanceSynonyms Synonyms { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000118D4 File Offset: 0x0000FAD4
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x000118DC File Offset: 0x0000FADC
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public InstanceWeights Weights { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000118E5 File Offset: 0x0000FAE5
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x000118ED File Offset: 0x0000FAED
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public EntityInstanceIndex Index { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x000118F6 File Offset: 0x0000FAF6
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x000118FE File Offset: 0x0000FAFE
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public EntityInstancePluralNormalization PluralNormalization { get; set; }
	}
}
