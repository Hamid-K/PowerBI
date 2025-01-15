using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009FD RID: 2557
	public class SimpleDelimiter : DelimiterConstraint, IOptionConstraint<Options>
	{
		// Token: 0x06003DBE RID: 15806 RVA: 0x000C0AA8 File Offset: 0x000BECA8
		public void SetOptions(Options options)
		{
			DelimiterConstraint.SetSplitFileOptions(options);
			options.TextConstraints.Add(new SimpleDelimiter());
			options.TextConstraints.Add(new IncludeDelimitersInOutput(false));
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000C0AD1 File Offset: 0x000BECD1
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is SimpleDelimiter;
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000C0ADC File Offset: 0x000BECDC
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			if (!(other is FixedWidth) && !(other is SimpleDelimiterOrFixedWidth))
			{
				DelimiterStrings delimiterStrings = other as DelimiterStrings;
				return delimiterStrings != null && delimiterStrings.Delimiters.Count > 1;
			}
			return true;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000C0B15 File Offset: 0x000BED15
		public override int GetHashCode()
		{
			return 59833;
		}
	}
}
