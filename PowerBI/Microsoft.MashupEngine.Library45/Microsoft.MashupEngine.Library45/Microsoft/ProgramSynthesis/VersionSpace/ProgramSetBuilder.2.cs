using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000290 RID: 656
	public class ProgramSetBuilder<T> : ProgramSetBuilder where T : IProgramNodeBuilder
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x00029BA3 File Offset: 0x00027DA3
		protected ProgramSetBuilder(ProgramSet set)
			: base(set)
		{
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00029BAC File Offset: 0x00027DAC
		public static ProgramSetBuilder<T> CreateUnsafe(ProgramSet set)
		{
			if (set == null)
			{
				return null;
			}
			return new ProgramSetBuilder<T>(set);
		}
	}
}
