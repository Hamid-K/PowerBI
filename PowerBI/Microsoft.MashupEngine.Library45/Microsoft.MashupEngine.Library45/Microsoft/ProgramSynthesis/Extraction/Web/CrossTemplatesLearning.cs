using System;
using Microsoft.ProgramSynthesis.Extraction.Web.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FAB RID: 4011
	public class CrossTemplatesLearning<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06006EDA RID: 28378 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x06006EDB RID: 28379 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return false;
		}

		// Token: 0x06006EDC RID: 28380 RVA: 0x0016AE4E File Offset: 0x0016904E
		public void SetOptions(Witnesses.Options options)
		{
			options.LearnCrossTemplates = true;
		}

		// Token: 0x06006EDD RID: 28381 RVA: 0x0016AE57 File Offset: 0x00169057
		public override int GetHashCode()
		{
			return 127;
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return this.Equals(other);
		}
	}
}
