using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C3 RID: 451
	public sealed class PrepPhrase
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00012092 File Offset: 0x00010292
		[JsonProperty(Required = Required.Always)]
		public List<Term> Prepositions { get; } = new TermList();

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0001209A File Offset: 0x0001029A
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x000120A2 File Offset: 0x000102A2
		[JsonProperty(Required = Required.Always)]
		public RoleReference Object { get; set; }
	}
}
