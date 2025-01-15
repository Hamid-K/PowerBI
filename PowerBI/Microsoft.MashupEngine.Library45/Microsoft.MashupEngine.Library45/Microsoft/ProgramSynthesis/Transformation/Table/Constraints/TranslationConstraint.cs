using System;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Constraints
{
	// Token: 0x02001B60 RID: 7008
	public abstract class TranslationConstraint : Constraint<ITable<object>, ITable<object>>
	{
		// Token: 0x0600E616 RID: 58902 RVA: 0x0030BC85 File Offset: 0x00309E85
		public override bool ConflictsWith(Constraint<ITable<object>, ITable<object>> other)
		{
			return this != other && ((other != null) ? other.GetType() : null) == base.GetType();
		}

		// Token: 0x0600E617 RID: 58903 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<ITable<object>, ITable<object>> program)
		{
			return true;
		}
	}
}
