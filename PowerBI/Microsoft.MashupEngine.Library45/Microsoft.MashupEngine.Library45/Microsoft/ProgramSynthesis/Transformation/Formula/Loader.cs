using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014DE RID: 5342
	public class Loader : SimpleProgramLoader<Program, IRow, object>
	{
		// Token: 0x0600A387 RID: 41863 RVA: 0x0022A56D File Offset: 0x0022876D
		private Loader()
		{
		}

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x0600A388 RID: 41864 RVA: 0x0022A575 File Offset: 0x00228775
		public static Loader Instance { get; } = new Loader();

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x0600A389 RID: 41865 RVA: 0x0022A57C File Offset: 0x0022877C
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x0600A38A RID: 41866 RVA: 0x0022A584 File Offset: 0x00228784
		public override Program Create(ProgramNode program)
		{
			return new Program(program, null, null);
		}
	}
}
