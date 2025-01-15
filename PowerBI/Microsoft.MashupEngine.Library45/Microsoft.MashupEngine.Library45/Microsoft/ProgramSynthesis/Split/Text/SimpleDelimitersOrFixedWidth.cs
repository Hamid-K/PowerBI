using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012FA RID: 4858
	public class SimpleDelimitersOrFixedWidth : Constraint<StringRegion, SplitCell[]>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06009268 RID: 37480 RVA: 0x001EC8B8 File Offset: 0x001EAAB8
		public void SetOptions(Witnesses.Options options)
		{
			options.LearnSimpleDelimitersOrFixedWidth = true;
		}

		// Token: 0x06009269 RID: 37481 RVA: 0x001EC8C4 File Offset: 0x001EAAC4
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			SplitProgram splitProgram = program as SplitProgram;
			ProgramProperties programProperties = ((splitProgram != null) ? splitProgram.Properties : null);
			return programProperties != null && (programProperties.Delimiter != null || programProperties.FieldPositions != null);
		}

		// Token: 0x0600926A RID: 37482 RVA: 0x001EC8FC File Offset: 0x001EAAFC
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			return other is SimpleDelimiter || other is NthExampleConstraint || other is FixedWidthConstraint;
		}

		// Token: 0x0600926B RID: 37483 RVA: 0x0016C7B5 File Offset: 0x0016A9B5
		public bool Equals(SimpleDelimitersOrFixedWidth other)
		{
			return other != null;
		}

		// Token: 0x0600926C RID: 37484 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600926D RID: 37485 RVA: 0x001EC919 File Offset: 0x001EAB19
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SimpleDelimitersOrFixedWidth)obj)));
		}

		// Token: 0x0600926E RID: 37486 RVA: 0x001EC947 File Offset: 0x001EAB47
		public override int GetHashCode()
		{
			return 3466643;
		}
	}
}
