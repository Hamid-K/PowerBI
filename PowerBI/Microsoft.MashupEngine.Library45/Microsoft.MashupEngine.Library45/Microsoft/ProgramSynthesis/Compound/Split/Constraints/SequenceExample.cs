using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009FC RID: 2556
	public class SequenceExample : Prefix<IEnumerable<StringRegion>, IEnumerable<StringRegion>>
	{
		// Token: 0x06003DBD RID: 15805 RVA: 0x000C0A9D File Offset: 0x000BEC9D
		public SequenceExample(IEnumerable<StringRegion> inputLines, IEnumerable<IEnumerable<StringRegion>> outputPrefix)
			: base(inputLines, outputPrefix, false)
		{
		}
	}
}
