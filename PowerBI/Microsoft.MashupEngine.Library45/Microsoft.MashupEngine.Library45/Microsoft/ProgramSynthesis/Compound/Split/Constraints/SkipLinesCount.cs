using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x02000A00 RID: 2560
	public class SkipLinesCount : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003DD2 RID: 15826 RVA: 0x000C0C8B File Offset: 0x000BEE8B
		public SkipLinesCount(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("Should be non-negative", "count");
			}
			this.Count = count;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000C0CAE File Offset: 0x000BEEAE
		public void SetOptions(Options options)
		{
			options.SkipLinesCount = new int?(this.Count);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000C0CC4 File Offset: 0x000BEEC4
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			SkipLinesCount skipLinesCount = other as SkipLinesCount;
			return skipLinesCount != null && this.Count == skipLinesCount.Count;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000C0CF8 File Offset: 0x000BEEF8
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			SkipLinesCount skipLinesCount = other as SkipLinesCount;
			return skipLinesCount != null && this.Equals(skipLinesCount);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000C0D1D File Offset: 0x000BEF1D
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is SkipLinesCount;
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000C0D28 File Offset: 0x000BEF28
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			Program program2 = program as Program;
			return program2 != null && program2.Properties.SkipLinesCount == this.Count;
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000C0D54 File Offset: 0x000BEF54
		public override int GetHashCode()
		{
			return 919 ^ this.Count;
		}

		// Token: 0x04001CCC RID: 7372
		public readonly int Count;
	}
}
