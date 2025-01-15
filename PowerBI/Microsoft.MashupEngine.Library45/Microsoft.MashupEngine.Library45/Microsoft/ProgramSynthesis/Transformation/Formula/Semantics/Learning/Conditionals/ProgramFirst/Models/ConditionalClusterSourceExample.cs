using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.ProgramFirst.Models
{
	// Token: 0x02001724 RID: 5924
	public class ConditionalClusterSourceExample
	{
		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x0600C52D RID: 50477 RVA: 0x002A7023 File Offset: 0x002A5223
		// (set) Token: 0x0600C52E RID: 50478 RVA: 0x002A702B File Offset: 0x002A522B
		public int Position { get; set; }

		// Token: 0x0600C52F RID: 50479 RVA: 0x002A7034 File Offset: 0x002A5234
		public override string ToString()
		{
			return string.Format("{0,2}: {1}", this.Position, this.Example.Output.ToCSharpLiteral());
		}

		// Token: 0x04004D35 RID: 19765
		public Example<IRow, object> Example;
	}
}
