using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009FE RID: 2558
	public class SimpleDelimiterOrFixedWidth : DelimiterConstraint, IOptionConstraint<Options>
	{
		// Token: 0x06003DC4 RID: 15812 RVA: 0x000C0B24 File Offset: 0x000BED24
		public void SetOptions(Options options)
		{
			DelimiterConstraint.SetSplitFileOptions(options);
			options.TextConstraints.Add(new SimpleDelimitersOrFixedWidth());
			options.TextConstraints.Add(new IncludeDelimitersInOutput(false));
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000C0B4D File Offset: 0x000BED4D
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is SimpleDelimiterOrFixedWidth;
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x000C0B58 File Offset: 0x000BED58
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			if (!(other is SimpleDelimiter) && !(other is FixedWidth))
			{
				DelimiterStrings delimiterStrings = other as DelimiterStrings;
				return delimiterStrings != null && delimiterStrings.Delimiters.Count > 1;
			}
			return true;
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x000C0B91 File Offset: 0x000BED91
		public override int GetHashCode()
		{
			return 24151;
		}
	}
}
