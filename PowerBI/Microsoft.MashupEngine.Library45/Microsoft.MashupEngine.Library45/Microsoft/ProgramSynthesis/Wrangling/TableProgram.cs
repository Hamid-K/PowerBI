using System;
using System.IO;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000D5 RID: 213
	public abstract class TableProgram<TRegion> : Program<TRegion, ITable<TRegion>>
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x000105BC File Offset: 0x0000E7BC
		protected TableProgram(ProgramNode programNode, double score)
			: base(programNode, score, null)
		{
		}

		// Token: 0x060004BA RID: 1210
		public abstract ITable<TRegion> Run(TextReader inputReader);
	}
}
