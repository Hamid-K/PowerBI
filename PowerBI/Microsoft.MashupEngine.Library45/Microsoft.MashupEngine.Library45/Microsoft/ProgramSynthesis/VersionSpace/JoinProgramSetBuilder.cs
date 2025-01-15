using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x0200027F RID: 639
	public class JoinProgramSetBuilder<T> : ProgramSetBuilder<T> where T : IProgramNodeBuilder
	{
		// Token: 0x06000DDD RID: 3549 RVA: 0x0002880B File Offset: 0x00026A0B
		private JoinProgramSetBuilder(JoinProgramSet set)
			: base(set)
		{
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00028814 File Offset: 0x00026A14
		public static JoinProgramSetBuilder<T> CreateUnsafe(JoinProgramSet set)
		{
			if (set == null)
			{
				return null;
			}
			return new JoinProgramSetBuilder<T>(set);
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00028821 File Offset: 0x00026A21
		public new JoinProgramSet Set
		{
			get
			{
				return (JoinProgramSet)base.Set;
			}
		}
	}
}
