using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F1 RID: 2545
	public class FilterEmptyColumns : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D72 RID: 15730 RVA: 0x000C05AD File Offset: 0x000BE7AD
		public void SetOptions(Options options)
		{
			options.FilterEmptyColummns = true;
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> _)
		{
			return false;
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000C05B6 File Offset: 0x000BE7B6
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is FilterEmptyColumns;
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x0001B1AD File Offset: 0x000193AD
		public override int GetHashCode()
		{
			return 17;
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> _)
		{
			return true;
		}
	}
}
