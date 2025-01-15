using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C2 RID: 450
	public sealed class AdverbPhrase
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00012033 File Offset: 0x00010233
		[JsonProperty]
		public List<Term> Adverbs { get; } = new TermList();

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001203B File Offset: 0x0001023B
		[JsonProperty]
		public List<Term> Antonyms { get; } = new TermList();

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00012043 File Offset: 0x00010243
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0001204B File Offset: 0x0001024B
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Measurement { get; set; }

		// Token: 0x0600098B RID: 2443 RVA: 0x00012054 File Offset: 0x00010254
		public bool ShouldSerializeAdverbs()
		{
			return this.Adverbs.Count > 0;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00012064 File Offset: 0x00010264
		public bool ShouldSerializeAntonyms()
		{
			return this.Antonyms.Count > 0;
		}
	}
}
