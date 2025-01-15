using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x02001318 RID: 4888
	public abstract class TranslationConstraint : Constraint<StringRegion, SplitCell[]>
	{
		// Token: 0x06009300 RID: 37632 RVA: 0x001EEA85 File Offset: 0x001ECC85
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			return this != other && ((other != null) ? other.GetType() : null) == base.GetType();
		}
	}
}
