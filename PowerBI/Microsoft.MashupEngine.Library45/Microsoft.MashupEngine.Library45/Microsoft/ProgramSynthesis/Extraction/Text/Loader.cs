using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Text
{
	// Token: 0x02000EFA RID: 3834
	public class Loader : SimpleProgramLoader<Program, StringRegion, ITable<StringRegion>>
	{
		// Token: 0x06006846 RID: 26694 RVA: 0x00153D85 File Offset: 0x00151F85
		private Loader()
		{
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06006847 RID: 26695 RVA: 0x00153D8D File Offset: 0x00151F8D
		public static Loader Instance { get; } = new Loader();

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06006848 RID: 26696 RVA: 0x00153D94 File Offset: 0x00151F94
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x00153D44 File Offset: 0x00151F44
		public override Program Create(ProgramNode program)
		{
			return new Program(program);
		}
	}
}
