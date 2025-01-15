using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F81 RID: 3969
	internal class SplitPosition : SplitNode
	{
		// Token: 0x06006DFA RID: 28154 RVA: 0x00166E53 File Offset: 0x00165053
		public SplitPosition(int k)
		{
			this.K = k;
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06006DFB RID: 28155 RVA: 0x00166E62 File Offset: 0x00165062
		private int K { get; }

		// Token: 0x06006DFC RID: 28156 RVA: 0x00166E6A File Offset: 0x0016506A
		public override Record<StringRegion, StringRegion> Run(StringRegion row)
		{
			return Semantics.SplitPosition(row, this.K);
		}

		// Token: 0x06006DFD RID: 28157 RVA: 0x00166E78 File Offset: 0x00165078
		public override ProgramSetBuilder<split> Node(GrammarBuilders builder)
		{
			return builder.Set.Join.SplitPosition(ProgramSetBuilder.List<row>(builder.Symbol.row, new row[] { builder.Node.Variable.row }), ProgramSetBuilder.List<k>(builder.Symbol.k, new k[] { builder.Node.Rule.k(this.K) }));
		}
	}
}
