using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table
{
	// Token: 0x02001A80 RID: 6784
	public class Loader : SimpleProgramLoader<Program, ITable<object>, ITable<object>>
	{
		// Token: 0x0600DF41 RID: 57153 RVA: 0x002F5F85 File Offset: 0x002F4185
		private Loader()
		{
		}

		// Token: 0x1700253F RID: 9535
		// (get) Token: 0x0600DF42 RID: 57154 RVA: 0x002F5F8D File Offset: 0x002F418D
		public static Loader Instance { get; } = new Loader();

		// Token: 0x17002540 RID: 9536
		// (get) Token: 0x0600DF43 RID: 57155 RVA: 0x002F5F94 File Offset: 0x002F4194
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x0600DF44 RID: 57156 RVA: 0x002F5F9B File Offset: 0x002F419B
		public override Program Create(ProgramNode program)
		{
			return this.Create(program, 0.0);
		}

		// Token: 0x0600DF45 RID: 57157 RVA: 0x002F5FAD File Offset: 0x002F41AD
		public Program Create(ProgramNode program, double score)
		{
			return new Program(Language.Build.Node.Cast.@out(program), score);
		}
	}
}
