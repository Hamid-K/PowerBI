using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x020005FB RID: 1531
	public class ClusteringResult<TNode> where TNode : IProgramNodeBuilder
	{
		// Token: 0x06002160 RID: 8544 RVA: 0x0005EE2E File Offset: 0x0005D02E
		public ClusteringResult(IReadOnlyList<Cluster<TNode>> clusters, ClusteringResult<TNode> outliers)
		{
			this.Clusters = clusters;
			this.Outliers = outliers;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x0005EE44 File Offset: 0x0005D044
		public IReadOnlyList<Cluster<TNode>> Clusters { get; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0005EE4C File Offset: 0x0005D04C
		public ClusteringResult<TNode> Outliers { get; }

		// Token: 0x06002163 RID: 8547 RVA: 0x0005EE54 File Offset: 0x0005D054
		public IEnumerable<Cluster<TNode>> InOrder()
		{
			IEnumerable<Cluster<TNode>> clusters = this.Clusters;
			ClusteringResult<TNode> outliers = this.Outliers;
			return clusters.Concat(((outliers != null) ? outliers.InOrder() : null) ?? Enumerable.Empty<Cluster<TNode>>());
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0005EE7C File Offset: 0x0005D07C
		public IEnumerable<State> SamplesInOrder()
		{
			return this.InOrder().SelectMany((Cluster<TNode> x) => x.Data);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
		private void ToString(CodeBuilder codeBuilder)
		{
			for (int i = 0; i < this.Clusters.Count; i++)
			{
				codeBuilder.AppendLine(string.Format("Cluster {0}: {1}", i, this.Clusters[i]));
			}
			if (this.Outliers != null)
			{
				codeBuilder.AppendLine("Outliers:");
				codeBuilder.PushIndent(1U);
				this.Outliers.ToString(codeBuilder);
				codeBuilder.PopIndent();
			}
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0005EF1C File Offset: 0x0005D11C
		public override string ToString()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			this.ToString(codeBuilder);
			return codeBuilder.GetCode();
		}
	}
}
