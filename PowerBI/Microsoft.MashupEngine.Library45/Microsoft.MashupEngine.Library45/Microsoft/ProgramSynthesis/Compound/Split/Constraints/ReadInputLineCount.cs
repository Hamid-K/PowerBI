using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009FB RID: 2555
	internal class ReadInputLineCount : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003DB4 RID: 15796 RVA: 0x000C09D9 File Offset: 0x000BEBD9
		public ReadInputLineCount(int limit)
		{
			this.Limit = limit;
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003DB5 RID: 15797 RVA: 0x000C09E8 File Offset: 0x000BEBE8
		public int Limit { get; }

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000C09F0 File Offset: 0x000BEBF0
		public void SetOptions(Options options)
		{
			options.ReadInputLineCount = this.Limit;
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000C09FE File Offset: 0x000BEBFE
		private bool Equals(ReadInputLineCount other)
		{
			return other != null && this.Limit == other.Limit;
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000C0A19 File Offset: 0x000BEC19
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj.GetType() == base.GetType() && this.Equals((ReadInputLineCount)obj)));
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x000C0A47 File Offset: 0x000BEC47
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return this.Equals(other as ReadInputLineCount);
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000C0A58 File Offset: 0x000BEC58
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			ReadInputLineCount readInputLineCount = other as ReadInputLineCount;
			return readInputLineCount != null && !object.Equals(this, readInputLineCount);
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x000C0A7C File Offset: 0x000BEC7C
		public override int GetHashCode()
		{
			return 2851 ^ this.Limit.GetHashCode();
		}
	}
}
