using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split
{
	// Token: 0x02000911 RID: 2321
	public class Loader : SimpleProgramLoader<Program, StringRegion, ITable<StringRegion>>
	{
		// Token: 0x060031F4 RID: 12788 RVA: 0x00093334 File Offset: 0x00091534
		private Loader()
		{
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x0009333C File Offset: 0x0009153C
		public static Loader Instance { get; } = new Loader();

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x00093343 File Offset: 0x00091543
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x0009334A File Offset: 0x0009154A
		public override Program Create(ProgramNode program)
		{
			return new Program(Language.Build.Node.Cast.topSplit(program));
		}
	}
}
