using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Conditionals
{
	// Token: 0x02000A0E RID: 2574
	public class PredicateLoader : SimpleProgramLoader<PredicateProgram, string, bool>
	{
		// Token: 0x06003E10 RID: 15888 RVA: 0x000C14D2 File Offset: 0x000BF6D2
		private PredicateLoader()
		{
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06003E11 RID: 15889 RVA: 0x000C14DA File Offset: 0x000BF6DA
		public static PredicateLoader Instance { get; } = new PredicateLoader();

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000C14BF File Offset: 0x000BF6BF
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000C14E1 File Offset: 0x000BF6E1
		public override PredicateProgram Create(ProgramNode program)
		{
			return new PredicateProgram(Language.Build.Node.Cast.conjunct(program));
		}
	}
}
