using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F5 RID: 2549
	public class IgnoreSelectData : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D8B RID: 15755 RVA: 0x000C06C5 File Offset: 0x000BE8C5
		public void SetOptions(Options options)
		{
			options.IgnoreSelectData = true;
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x000C06CE File Offset: 0x000BE8CE
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is IgnoreSelectData;
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return false;
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000C06D9 File Offset: 0x000BE8D9
		public override int GetHashCode()
		{
			return 15787;
		}
	}
}
