using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json
{
	// Token: 0x020019F9 RID: 6649
	public class Loader : SimpleProgramLoader<Program, JToken, JToken>
	{
		// Token: 0x0600D89E RID: 55454 RVA: 0x002DF480 File Offset: 0x002DD680
		private Loader()
		{
		}

		// Token: 0x1700243D RID: 9277
		// (get) Token: 0x0600D89F RID: 55455 RVA: 0x002DF488 File Offset: 0x002DD688
		public static Loader Instance { get; } = new Loader();

		// Token: 0x1700243E RID: 9278
		// (get) Token: 0x0600D8A0 RID: 55456 RVA: 0x002DF48F File Offset: 0x002DD68F
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x0600D8A1 RID: 55457 RVA: 0x002DF45B File Offset: 0x002DD65B
		public override Program Create(ProgramNode program)
		{
			return new Program(program);
		}
	}
}
