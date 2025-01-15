using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200008C RID: 140
	public sealed class GeneratePhrasingLabelsResult
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00005D82 File Offset: 0x00003F82
		public GeneratePhrasingLabelsResult(IEnumerable<PhrasingLabel> phrasingLabels)
		{
			this.PhrasingLabels = phrasingLabels.AsReadOnlyList<PhrasingLabel>();
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00005D96 File Offset: 0x00003F96
		public IReadOnlyList<PhrasingLabel> PhrasingLabels { get; }

		// Token: 0x040002F2 RID: 754
		public static readonly GeneratePhrasingLabelsResult Empty = new GeneratePhrasingLabelsResult(new List<PhrasingLabel>());
	}
}
