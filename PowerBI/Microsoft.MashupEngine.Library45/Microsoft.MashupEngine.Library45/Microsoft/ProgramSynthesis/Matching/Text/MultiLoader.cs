using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011B7 RID: 4535
	public class MultiLoader : SimpleProgramLoader<MultiProgram, IEnumerable<string>, IEnumerable<bool>>
	{
		// Token: 0x06008700 RID: 34560 RVA: 0x001C5799 File Offset: 0x001C3999
		private MultiLoader()
		{
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06008701 RID: 34561 RVA: 0x001C57A1 File Offset: 0x001C39A1
		public static MultiLoader Instance { get; } = new MultiLoader();

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06008702 RID: 34562 RVA: 0x001C57A8 File Offset: 0x001C39A8
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x06008703 RID: 34563 RVA: 0x001C5674 File Offset: 0x001C3874
		public override MultiProgram Create(ProgramNode program)
		{
			return new MultiProgram(program);
		}
	}
}
