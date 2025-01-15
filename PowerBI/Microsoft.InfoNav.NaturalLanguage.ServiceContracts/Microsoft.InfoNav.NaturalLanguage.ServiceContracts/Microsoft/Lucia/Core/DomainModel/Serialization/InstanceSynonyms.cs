using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001AD RID: 429
	public sealed class InstanceSynonyms : IStateItem
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0001190F File Offset: 0x0000FB0F
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00011917 File Offset: 0x0000FB17
		[JsonProperty(Required = Required.Always)]
		public ConceptualPropertyBinding SynonymBinding { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00011920 File Offset: 0x0000FB20
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00011928 File Offset: 0x0000FB28
		[JsonProperty(Required = Required.Always)]
		public ConceptualPropertyBinding ValueBinding { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00011931 File Offset: 0x0000FB31
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00011939 File Offset: 0x0000FB39
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public State State { get; set; }
	}
}
