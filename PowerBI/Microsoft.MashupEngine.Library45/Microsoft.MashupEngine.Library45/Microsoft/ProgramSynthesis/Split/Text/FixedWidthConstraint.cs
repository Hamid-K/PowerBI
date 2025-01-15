using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012F1 RID: 4849
	public class FixedWidthConstraint : Constraint<StringRegion, SplitCell[]>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06009222 RID: 37410 RVA: 0x001EC110 File Offset: 0x001EA310
		public void SetOptions(Witnesses.Options options)
		{
			options.LearnFixedWidth = true;
		}

		// Token: 0x06009223 RID: 37411 RVA: 0x001EC119 File Offset: 0x001EA319
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			SplitProgram splitProgram = program as SplitProgram;
			return ((splitProgram != null) ? splitProgram.Properties.FieldPositions : null) != null;
		}

		// Token: 0x06009224 RID: 37412 RVA: 0x001EC135 File Offset: 0x001EA335
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			return other is DelimiterStringsConstraint || other is SimpleDelimiter || other is NthExampleConstraint || other is SimpleDelimitersOrFixedWidth;
		}

		// Token: 0x06009225 RID: 37413 RVA: 0x0016C7B5 File Offset: 0x0016A9B5
		public bool Equals(FixedWidthConstraint other)
		{
			return other != null;
		}

		// Token: 0x06009226 RID: 37414 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06009227 RID: 37415 RVA: 0x001EC15A File Offset: 0x001EA35A
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((FixedWidthConstraint)obj)));
		}

		// Token: 0x06009228 RID: 37416 RVA: 0x001EC188 File Offset: 0x001EA388
		public override int GetHashCode()
		{
			return 9040861;
		}
	}
}
