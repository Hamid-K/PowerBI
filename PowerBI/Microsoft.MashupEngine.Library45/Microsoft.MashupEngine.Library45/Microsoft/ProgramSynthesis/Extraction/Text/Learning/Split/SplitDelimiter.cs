using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F82 RID: 3970
	internal class SplitDelimiter : SplitNode
	{
		// Token: 0x06006DFE RID: 28158 RVA: 0x00166EF5 File Offset: 0x001650F5
		public SplitDelimiter(string str, int k)
		{
			this.Str = str;
			this.K = k;
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06006DFF RID: 28159 RVA: 0x00166F0B File Offset: 0x0016510B
		private string Str { get; }

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06006E00 RID: 28160 RVA: 0x00166F13 File Offset: 0x00165113
		private int K { get; }

		// Token: 0x06006E01 RID: 28161 RVA: 0x00166F1B File Offset: 0x0016511B
		public override Record<StringRegion, StringRegion> Run(StringRegion row)
		{
			return Semantics.SplitDelimiter(row, this.Str, this.K);
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x00166F30 File Offset: 0x00165130
		public override ProgramSetBuilder<split> Node(GrammarBuilders builder)
		{
			return builder.Set.Join.SplitDelimiter(ProgramSetBuilder.List<row>(builder.Symbol.row, new row[] { builder.Node.Variable.row }), ProgramSetBuilder.List<str>(builder.Symbol.str, new str[] { builder.Node.Rule.str(this.Str) }), ProgramSetBuilder.List<k>(builder.Symbol.k, new k[] { builder.Node.Rule.k(this.K) }));
		}
	}
}
