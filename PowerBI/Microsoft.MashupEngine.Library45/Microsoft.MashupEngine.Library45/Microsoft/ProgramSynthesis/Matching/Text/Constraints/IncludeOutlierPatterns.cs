using System;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text.Constraints
{
	// Token: 0x0200123F RID: 4671
	public sealed class IncludeOutlierPatterns<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06008CAA RID: 36010 RVA: 0x00024352 File Offset: 0x00022552
		private IncludeOutlierPatterns()
		{
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06008CAB RID: 36011 RVA: 0x001D8F5D File Offset: 0x001D715D
		public static IncludeOutlierPatterns<TInput, TOutput> Instance { get; } = new IncludeOutlierPatterns<TInput, TOutput>();

		// Token: 0x06008CAC RID: 36012 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return this == other;
		}

		// Token: 0x06008CAD RID: 36013 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return false;
		}

		// Token: 0x06008CAE RID: 36014 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x06008CAF RID: 36015 RVA: 0x001D8F64 File Offset: 0x001D7164
		public void SetOptions(Witnesses.Options options)
		{
			options.IncludeOutlierPatterns = true;
		}

		// Token: 0x06008CB0 RID: 36016 RVA: 0x001D8F6D File Offset: 0x001D716D
		public override int GetHashCode()
		{
			return 834131;
		}
	}
}
