using System;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.TableToJson.Constraint
{
	// Token: 0x02001A7D RID: 6781
	public class TableToJsonExample : Example<Table, JToken>
	{
		// Token: 0x0600DF2C RID: 57132 RVA: 0x002F5CCD File Offset: 0x002F3ECD
		public TableToJsonExample(Table input, JToken output)
			: base(input, output, false)
		{
		}

		// Token: 0x0600DF2D RID: 57133 RVA: 0x002F5CD8 File Offset: 0x002F3ED8
		public override bool Equals(Constraint<Table, JToken> other)
		{
			return other != null && (this == other || base.Equals((TableToJsonExample)other));
		}

		// Token: 0x0600DF2E RID: 57134 RVA: 0x002F5CF4 File Offset: 0x002F3EF4
		public override bool ConflictsWith(Constraint<Table, JToken> other)
		{
			TableToJsonExample tableToJsonExample = other as TableToJsonExample;
			return tableToJsonExample == null || !tableToJsonExample.Input.Equals(base.Input);
		}

		// Token: 0x0600DF2F RID: 57135 RVA: 0x002F5CAF File Offset: 0x002F3EAF
		public override bool Valid(Program<Table, JToken> program)
		{
			return program.Run(base.Input) != null;
		}

		// Token: 0x0600DF30 RID: 57136 RVA: 0x002F5CC0 File Offset: 0x002F3EC0
		public override int GetHashCode()
		{
			return base.Input.GetHashCode();
		}
	}
}
