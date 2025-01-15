using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009FF RID: 2559
	public class SkipFooter : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003DCA RID: 15818 RVA: 0x000C0B98 File Offset: 0x000BED98
		public SkipFooter(int skipFooterLinesCount)
		{
			this.SkipFooterLinesCount = skipFooterLinesCount;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x000C0BA7 File Offset: 0x000BEDA7
		public int SkipFooterLinesCount { get; }

		// Token: 0x06003DCC RID: 15820 RVA: 0x000C0BAF File Offset: 0x000BEDAF
		public void SetOptions(Options options)
		{
			options.SkipFooterLinesCount = this.SkipFooterLinesCount;
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000C0BC0 File Offset: 0x000BEDC0
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			Program program2 = program as Program;
			return program2 != null && program2.Properties.SkipFooterLinesCount == this.SkipFooterLinesCount;
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000C0BEC File Offset: 0x000BEDEC
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			SkipFooter skipFooter = other as SkipFooter;
			return skipFooter != null && skipFooter.SkipFooterLinesCount != this.SkipFooterLinesCount;
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000C0C18 File Offset: 0x000BEE18
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			if (other == null)
			{
				return false;
			}
			SkipFooter skipFooter = other as SkipFooter;
			return skipFooter != null && this.SkipFooterLinesCount == skipFooter.SkipFooterLinesCount;
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000C0C44 File Offset: 0x000BEE44
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			SkipFooter skipFooter = other as SkipFooter;
			return skipFooter != null && this.SkipFooterLinesCount == skipFooter.SkipFooterLinesCount;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000C0C70 File Offset: 0x000BEE70
		public override int GetHashCode()
		{
			return this.SkipFooterLinesCount.GetHashCode();
		}
	}
}
