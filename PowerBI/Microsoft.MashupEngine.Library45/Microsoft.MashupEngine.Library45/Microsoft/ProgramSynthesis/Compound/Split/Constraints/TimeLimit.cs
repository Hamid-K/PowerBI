using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x02000A02 RID: 2562
	public class TimeLimit : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003DDA RID: 15834 RVA: 0x000C0D6C File Offset: 0x000BEF6C
		public TimeLimit(TimeSpan limit)
		{
			this.Limit = limit;
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000C0D7B File Offset: 0x000BEF7B
		public TimeSpan Limit { get; }

		// Token: 0x06003DDC RID: 15836 RVA: 0x000C0D83 File Offset: 0x000BEF83
		public void SetOptions(Options options)
		{
			options.TimeLimit = this.Limit;
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000C0D91 File Offset: 0x000BEF91
		private bool Equals(TimeLimit other)
		{
			return other != null && object.Equals(this.Limit, other.Limit);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000C0DB9 File Offset: 0x000BEFB9
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((TimeLimit)obj)));
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000C0DE7 File Offset: 0x000BEFE7
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return this.Equals(other as TimeLimit);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000C0DF8 File Offset: 0x000BEFF8
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			TimeLimit timeLimit = other as TimeLimit;
			return !(timeLimit == null) && !object.Equals(this, timeLimit);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x000C0E24 File Offset: 0x000BF024
		public override int GetHashCode()
		{
			return 4157 ^ this.Limit.GetHashCode();
		}
	}
}
