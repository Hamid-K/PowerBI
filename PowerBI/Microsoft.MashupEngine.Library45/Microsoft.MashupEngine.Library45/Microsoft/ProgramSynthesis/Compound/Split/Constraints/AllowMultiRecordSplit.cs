using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009EA RID: 2538
	public class AllowMultiRecordSplit : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D43 RID: 15683 RVA: 0x000C0095 File Offset: 0x000BE295
		public void SetOptions(Options options)
		{
			options.AllowMultiRecord = true;
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000C009E File Offset: 0x000BE29E
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is AllowMultiRecordSplit;
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return false;
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x000C00A9 File Offset: 0x000BE2A9
		public override int GetHashCode()
		{
			return 859;
		}
	}
}
