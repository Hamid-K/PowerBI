using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012F9 RID: 4857
	public class SimpleDelimiter : Constraint<StringRegion, SplitCell[]>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06009260 RID: 37472 RVA: 0x001EC849 File Offset: 0x001EAA49
		public void SetOptions(Witnesses.Options options)
		{
			options.LearnSimpleSingleDelimiterPrograms = true;
		}

		// Token: 0x06009261 RID: 37473 RVA: 0x001EC852 File Offset: 0x001EAA52
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			SplitProgram splitProgram = program as SplitProgram;
			return ((splitProgram != null) ? splitProgram.Properties.Delimiter : null) != null;
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x001EC86E File Offset: 0x001EAA6E
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			return other is FixedWidthConstraint || other is SimpleDelimitersOrFixedWidth;
		}

		// Token: 0x06009263 RID: 37475 RVA: 0x0016C7B5 File Offset: 0x0016A9B5
		public bool Equals(SimpleDelimiter other)
		{
			return other != null;
		}

		// Token: 0x06009264 RID: 37476 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06009265 RID: 37477 RVA: 0x001EC883 File Offset: 0x001EAA83
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SimpleDelimiter)obj)));
		}

		// Token: 0x06009266 RID: 37478 RVA: 0x001EC8B1 File Offset: 0x001EAAB1
		public override int GetHashCode()
		{
			return 1372051;
		}
	}
}
