using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F6 RID: 2550
	public class IgnoreSkip : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D91 RID: 15761 RVA: 0x000C06E0 File Offset: 0x000BE8E0
		public void SetOptions(Options options)
		{
			options.IgnoreSkip = true;
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000C06E9 File Offset: 0x000BE8E9
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is IgnoreSkip;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return false;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x000C06F4 File Offset: 0x000BE8F4
		public override int GetHashCode()
		{
			return 12841;
		}
	}
}
