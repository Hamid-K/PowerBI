using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x020000FD RID: 253
	internal sealed class CommonSubqueryEliminationResult
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x00021BCD File Offset: 0x0001FDCD
		internal CommonSubqueryEliminationResult(ResolvedQueryDefinition query, long duration, int consideredSubqueryCount, int comparedSubqueryCount, int eliminatedSubqueryCount, int newLetBindingCount)
		{
			this.Query = query;
			this.Duration = duration;
			this.ConsideredSubqueryCount = consideredSubqueryCount;
			this.ComparedSubqueryCount = comparedSubqueryCount;
			this.EliminatedSubqueryCount = eliminatedSubqueryCount;
			this.NewLetBindingCount = newLetBindingCount;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00021C02 File Offset: 0x0001FE02
		public ResolvedQueryDefinition Query { get; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00021C0A File Offset: 0x0001FE0A
		public long Duration { get; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00021C12 File Offset: 0x0001FE12
		public int ConsideredSubqueryCount { get; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00021C1A File Offset: 0x0001FE1A
		internal int ComparedSubqueryCount { get; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00021C22 File Offset: 0x0001FE22
		public int EliminatedSubqueryCount { get; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00021C2A File Offset: 0x0001FE2A
		public int NewLetBindingCount { get; }
	}
}
