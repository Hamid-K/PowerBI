using System;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.TableToJson.Constraint
{
	// Token: 0x02001A7C RID: 6780
	public class AutoTransform : ConstraintOnInput<Table, JToken>
	{
		// Token: 0x0600DF27 RID: 57127 RVA: 0x002F5C7E File Offset: 0x002F3E7E
		public AutoTransform(Table table)
			: base(table, false)
		{
		}

		// Token: 0x0600DF28 RID: 57128 RVA: 0x002F5C88 File Offset: 0x002F3E88
		public override bool Equals(Constraint<Table, JToken> other)
		{
			return other != null && (this == other || base.Equals((AutoTransform)other));
		}

		// Token: 0x0600DF29 RID: 57129 RVA: 0x002F5CA1 File Offset: 0x002F3EA1
		public override bool ConflictsWith(Constraint<Table, JToken> other)
		{
			return !(other is AutoTransform);
		}

		// Token: 0x0600DF2A RID: 57130 RVA: 0x002F5CAF File Offset: 0x002F3EAF
		public override bool Valid(Program<Table, JToken> program)
		{
			return program.Run(base.Input) != null;
		}

		// Token: 0x0600DF2B RID: 57131 RVA: 0x002F5CC0 File Offset: 0x002F3EC0
		public override int GetHashCode()
		{
			return base.Input.GetHashCode();
		}
	}
}
