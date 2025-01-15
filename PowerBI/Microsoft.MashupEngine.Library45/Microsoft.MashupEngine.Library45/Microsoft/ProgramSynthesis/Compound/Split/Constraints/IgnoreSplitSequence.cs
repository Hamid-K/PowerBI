using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F9 RID: 2553
	public class IgnoreSplitSequence : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003DA5 RID: 15781 RVA: 0x000C08F5 File Offset: 0x000BEAF5
		public void SetOptions(Options options)
		{
			options.IgnoreSplitSequence = true;
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x000C08FE File Offset: 0x000BEAFE
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is IgnoreSplitSequence;
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return false;
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000C0909 File Offset: 0x000BEB09
		public override int GetHashCode()
		{
			return 18013;
		}
	}
}
