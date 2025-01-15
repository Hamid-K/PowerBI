using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F3 RID: 2547
	public class IgnoreFilterHeader : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D7F RID: 15743 RVA: 0x000C0696 File Offset: 0x000BE896
		public void SetOptions(Options options)
		{
			options.IgnoreFilterHeader = true;
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000C069F File Offset: 0x000BE89F
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is IgnoreFilterHeader;
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return false;
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x000C06AA File Offset: 0x000BE8AA
		public override int GetHashCode()
		{
			return 13499;
		}
	}
}
