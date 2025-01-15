using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x0200130F RID: 4879
	public class SplitProgramLoader : SimpleProgramLoader<SplitProgram, StringRegion, SplitCell[]>
	{
		// Token: 0x060092D2 RID: 37586 RVA: 0x001EE31E File Offset: 0x001EC51E
		private SplitProgramLoader()
		{
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x060092D3 RID: 37587 RVA: 0x001EE326 File Offset: 0x001EC526
		public static SplitProgramLoader Instance { get; } = new SplitProgramLoader();

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x060092D4 RID: 37588 RVA: 0x001EE32D File Offset: 0x001EC52D
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x060092D5 RID: 37589 RVA: 0x001EE334 File Offset: 0x001EC534
		public override SplitProgram Create(ProgramNode program)
		{
			return new SplitProgram(Language.Build.Node.Cast.regionSplit(program));
		}
	}
}
