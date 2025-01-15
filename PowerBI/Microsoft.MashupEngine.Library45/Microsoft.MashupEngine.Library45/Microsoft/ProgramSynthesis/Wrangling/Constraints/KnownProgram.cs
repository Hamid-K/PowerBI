using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000233 RID: 563
	public class KnownProgram<TInput, TOutput> : Constraint<TInput, TOutput>
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x000248AE File Offset: 0x00022AAE
		public KnownProgram(Program<TInput, TOutput> program)
		{
			this.Program = program;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x000248BD File Offset: 0x00022ABD
		public Program<TInput, TOutput> Program { get; }

		// Token: 0x06000C0F RID: 3087 RVA: 0x000248C8 File Offset: 0x00022AC8
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			KnownProgram<TInput, TOutput> knownProgram = other as KnownProgram<TInput, TOutput>;
			return !(knownProgram == null) && object.Equals(this.Program, knownProgram.Program);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return false;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000248F8 File Offset: 0x00022AF8
		public override int GetHashCode()
		{
			int num = 529510939;
			Program<TInput, TOutput> program = this.Program;
			return num ^ ((program != null) ? program.GetHashCode() : 0);
		}
	}
}
