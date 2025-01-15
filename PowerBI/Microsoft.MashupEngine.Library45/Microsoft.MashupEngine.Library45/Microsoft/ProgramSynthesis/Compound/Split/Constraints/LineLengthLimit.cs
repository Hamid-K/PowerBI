using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009FA RID: 2554
	public class LineLengthLimit : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003DAB RID: 15787 RVA: 0x000C0910 File Offset: 0x000BEB10
		public LineLengthLimit(int limit)
		{
			this.Limit = limit;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x000C091F File Offset: 0x000BEB1F
		public int Limit { get; }

		// Token: 0x06003DAD RID: 15789 RVA: 0x000C0927 File Offset: 0x000BEB27
		public void SetOptions(Options options)
		{
			options.LineLengthLimit = this.Limit;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000C0935 File Offset: 0x000BEB35
		private bool Equals(LineLengthLimit other)
		{
			return other != null && this.Limit == other.Limit;
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000C0950 File Offset: 0x000BEB50
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((LineLengthLimit)obj)));
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000C097E File Offset: 0x000BEB7E
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return this.Equals(other as LineLengthLimit);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000C098C File Offset: 0x000BEB8C
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			LineLengthLimit lineLengthLimit = other as LineLengthLimit;
			return !(lineLengthLimit == null) && !object.Equals(this, lineLengthLimit);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000C09B8 File Offset: 0x000BEBB8
		public override int GetHashCode()
		{
			return 2851 ^ this.Limit.GetHashCode();
		}
	}
}
