using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009EC RID: 2540
	public abstract class DelimiterConstraint : Constraint<StringRegion, ITable<StringRegion>>
	{
		// Token: 0x06003D4F RID: 15695 RVA: 0x000C01C4 File Offset: 0x000BE3C4
		internal static void SetSplitFileOptions(Options options)
		{
			options.IgnoreFilterHeader = true;
			options.IgnoreSelectData = true;
			options.IgnoreSplitSequence = true;
		}
	}
}
