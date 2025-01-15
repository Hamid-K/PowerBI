using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema;
using Microsoft.ProgramSynthesis.Wrangling.Schema.Element;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TreeOutput;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x0200009D RID: 157
	public abstract class CompoundExtractionProgram<TRegion> : Program<TRegion, ITreeOutput<TRegion>> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x0000D79B File Offset: 0x0000B99B
		protected CompoundExtractionProgram(ProgramNode programNode, double score)
			: base(programNode, score, null)
		{
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003BA RID: 954
		public abstract ISchemaElement<TRegion> Schema { get; }

		// Token: 0x060003BB RID: 955
		public abstract ITreeOutput<TRegion> Run(string inputText);

		// Token: 0x060003BC RID: 956
		public abstract IEnumerable<IReadOnlyList<TRegion>> RunTable(TRegion input, TreeToTableSemantics semantics = TreeToTableSemantics.OuterJoin);

		// Token: 0x060003BD RID: 957
		public abstract IEnumerable<IReadOnlyList<TRegion>> RunTable(string inputText, TreeToTableSemantics semantics = TreeToTableSemantics.OuterJoin);
	}
}
