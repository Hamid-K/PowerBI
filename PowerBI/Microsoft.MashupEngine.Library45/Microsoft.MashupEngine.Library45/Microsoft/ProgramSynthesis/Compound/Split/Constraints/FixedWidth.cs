using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F2 RID: 2546
	public class FixedWidth : DelimiterConstraint, IOptionConstraint<Options>
	{
		// Token: 0x06003D78 RID: 15736 RVA: 0x000C05C1 File Offset: 0x000BE7C1
		public FixedWidth(string schema = null)
		{
			this.Schema = schema;
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x000C05D0 File Offset: 0x000BE7D0
		public string Schema { get; }

		// Token: 0x06003D7A RID: 15738 RVA: 0x000C05D8 File Offset: 0x000BE7D8
		public void SetOptions(Options options)
		{
			DelimiterConstraint.SetSplitFileOptions(options);
			options.TextConstraints.Add(new FixedWidthConstraint());
			options.FixedWidthSchema = this.Schema;
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000C05FC File Offset: 0x000BE7FC
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			FixedWidth fixedWidth = other as FixedWidth;
			return fixedWidth != null && this.Schema == fixedWidth.Schema;
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000C0626 File Offset: 0x000BE826
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is SimpleDelimiter || other is SimpleDelimiterOrFixedWidth || other is FixedWidth;
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x000C0644 File Offset: 0x000BE844
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			Program program2 = program as Program;
			return program2 != null && (program2.Properties.ProgramType == ProgramType.FixedWidth || program2.Properties.ProgramType == ProgramType.SimpleOneColumn);
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000C067B File Offset: 0x000BE87B
		public override int GetHashCode()
		{
			if (this.Schema != null)
			{
				return this.Schema.GetHashCode();
			}
			return 19469;
		}
	}
}
