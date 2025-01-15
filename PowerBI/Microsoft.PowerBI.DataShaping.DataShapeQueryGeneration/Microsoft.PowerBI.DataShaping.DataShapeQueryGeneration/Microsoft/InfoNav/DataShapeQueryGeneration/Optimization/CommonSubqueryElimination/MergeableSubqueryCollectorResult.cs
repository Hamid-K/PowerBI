using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x02000103 RID: 259
	internal readonly struct MergeableSubqueryCollectorResult
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x00022194 File Offset: 0x00020394
		internal MergeableSubqueryCollectorResult(IReadOnlyList<EquivalentQueryGroup> groups, HashSet<string> usedLetNames, int consideredSubqueryCount, int comparedSubqueryCount)
		{
			this.Groups = groups;
			this.UsedLetNames = usedLetNames;
			this.ConsideredSubqueryCount = consideredSubqueryCount;
			this.ComparedSubqueryCount = comparedSubqueryCount;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x000221B3 File Offset: 0x000203B3
		internal IReadOnlyList<EquivalentQueryGroup> Groups { get; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000221BB File Offset: 0x000203BB
		internal HashSet<string> UsedLetNames { get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x000221C3 File Offset: 0x000203C3
		internal int ConsideredSubqueryCount { get; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000221CB File Offset: 0x000203CB
		internal int ComparedSubqueryCount { get; }
	}
}
