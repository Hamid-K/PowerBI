using System;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text.Constraints
{
	// Token: 0x02001240 RID: 4672
	public sealed class UseLongConstantOptimization<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06008CB2 RID: 36018 RVA: 0x00024352 File Offset: 0x00022552
		private UseLongConstantOptimization()
		{
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06008CB3 RID: 36019 RVA: 0x001D8F80 File Offset: 0x001D7180
		public static UseLongConstantOptimization<TInput, TOutput> Instance { get; } = new UseLongConstantOptimization<TInput, TOutput>();

		// Token: 0x06008CB4 RID: 36020 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return this == other;
		}

		// Token: 0x06008CB5 RID: 36021 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return false;
		}

		// Token: 0x06008CB6 RID: 36022 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x06008CB7 RID: 36023 RVA: 0x001D8F87 File Offset: 0x001D7187
		public void SetOptions(Witnesses.Options options)
		{
			options.UseLongConstantOptimization = true;
		}

		// Token: 0x06008CB8 RID: 36024 RVA: 0x001D8F90 File Offset: 0x001D7190
		public override int GetHashCode()
		{
			return 666091;
		}
	}
}
