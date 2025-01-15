using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200022A RID: 554
	public class EnableDynamicSoundnessCheck<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<DSLOptions>
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00024330 File Offset: 0x00022530
		public static EnableDynamicSoundnessCheck<TInput, TOutput> Instance { get; } = new EnableDynamicSoundnessCheck<TInput, TOutput>();

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00024337 File Offset: 0x00022537
		public void SetOptions(DSLOptions options)
		{
			options.UseDynamicSoundnessCheck = true;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00024340 File Offset: 0x00022540
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return other is EnableDynamicSoundnessCheck<TInput, TOutput>;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return false;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002434B File Offset: 0x0002254B
		public override int GetHashCode()
		{
			return 10011101;
		}
	}
}
