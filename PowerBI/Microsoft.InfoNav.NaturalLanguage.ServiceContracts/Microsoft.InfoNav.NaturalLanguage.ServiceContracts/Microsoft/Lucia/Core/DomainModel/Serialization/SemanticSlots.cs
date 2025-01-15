using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C7 RID: 455
	public sealed class SemanticSlots
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0001248B File Offset: 0x0001068B
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x00012493 File Offset: 0x00010693
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Where { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001249C File Offset: 0x0001069C
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x000124A4 File Offset: 0x000106A4
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference When { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000124AD File Offset: 0x000106AD
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x000124B5 File Offset: 0x000106B5
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Duration { get; set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000124BE File Offset: 0x000106BE
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x000124C6 File Offset: 0x000106C6
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Occurrences { get; set; }
	}
}
