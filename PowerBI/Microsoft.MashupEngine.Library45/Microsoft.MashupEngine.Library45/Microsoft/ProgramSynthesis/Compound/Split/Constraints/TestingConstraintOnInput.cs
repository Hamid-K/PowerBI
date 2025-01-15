using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x02000A01 RID: 2561
	internal abstract class TestingConstraintOnInput : ConstraintOnInput<StringRegion, ITable<StringRegion>>
	{
		// Token: 0x06003DD9 RID: 15833 RVA: 0x000C0D62 File Offset: 0x000BEF62
		protected TestingConstraintOnInput(StringRegion input, bool isSoft)
			: base(input, isSoft)
		{
		}
	}
}
