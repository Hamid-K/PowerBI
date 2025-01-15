using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x020006F1 RID: 1777
	public class LearnerState
	{
		// Token: 0x0600269B RID: 9883 RVA: 0x0006CC92 File Offset: 0x0006AE92
		public LearnerState(State state, ProgramNode program, bool usedInRanking, params object[] record)
		{
			this.State = state;
			this.Record = record;
			this.Program = program;
			this.UsedInRanking = usedInRanking;
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x0006CCBE File Offset: 0x0006AEBE
		public State State { get; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x0006CCC6 File Offset: 0x0006AEC6
		public object[] Record { get; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x0006CCCE File Offset: 0x0006AECE
		public ProgramNode Program { get; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x0006CCD6 File Offset: 0x0006AED6
		public bool UsedInRanking { get; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x0006CCDE File Offset: 0x0006AEDE
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x0006CCE6 File Offset: 0x0006AEE6
		public bool IsNew { get; set; } = true;

		// Token: 0x060026A2 RID: 9890 RVA: 0x0006CCF0 File Offset: 0x0006AEF0
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} => {1}", new object[]
			{
				this.Program,
				this.Record.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null)
			}));
		}
	}
}
