using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F4 RID: 2548
	public class IgnoreQuote : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D85 RID: 15749 RVA: 0x000C06B1 File Offset: 0x000BE8B1
		public void SetOptions(Options options)
		{
			options.IgnoreQuote = true;
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x000C06BA File Offset: 0x000BE8BA
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is IgnoreQuote;
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return false;
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x000C00A9 File Offset: 0x000BE2A9
		public override int GetHashCode()
		{
			return 859;
		}
	}
}
