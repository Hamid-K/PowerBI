using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F80 RID: 3968
	internal abstract class SplitNode
	{
		// Token: 0x06006DF7 RID: 28151
		public abstract Record<StringRegion, StringRegion> Run(StringRegion row);

		// Token: 0x06006DF8 RID: 28152
		public abstract ProgramSetBuilder<split> Node(GrammarBuilders builder);
	}
}
