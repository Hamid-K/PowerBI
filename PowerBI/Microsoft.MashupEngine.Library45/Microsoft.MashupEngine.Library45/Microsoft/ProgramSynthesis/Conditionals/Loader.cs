using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Conditionals
{
	// Token: 0x02000A0D RID: 2573
	public class Loader : SimpleProgramLoader<Program, IEnumerable<string>, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x06003E0B RID: 15883 RVA: 0x000C14B0 File Offset: 0x000BF6B0
		private Loader()
		{
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06003E0C RID: 15884 RVA: 0x000C14B8 File Offset: 0x000BF6B8
		public static Loader Instance { get; } = new Loader();

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06003E0D RID: 15885 RVA: 0x000C14BF File Offset: 0x000BF6BF
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000C1494 File Offset: 0x000BF694
		public override Program Create(ProgramNode program)
		{
			return new Program(Language.Build.Node.Cast.output(program));
		}
	}
}
