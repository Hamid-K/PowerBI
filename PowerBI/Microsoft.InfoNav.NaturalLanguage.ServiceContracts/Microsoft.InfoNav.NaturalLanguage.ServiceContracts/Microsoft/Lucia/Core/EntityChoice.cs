using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000C0 RID: 192
	public sealed class EntityChoice
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x00007083 File Offset: 0x00005283
		public EntityChoice(IEnumerable<ConceptualBinding> options)
		{
			this.Options = options.AsReadOnlyList<ConceptualBinding>();
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00007097 File Offset: 0x00005297
		public IReadOnlyList<ConceptualBinding> Options { get; }
	}
}
