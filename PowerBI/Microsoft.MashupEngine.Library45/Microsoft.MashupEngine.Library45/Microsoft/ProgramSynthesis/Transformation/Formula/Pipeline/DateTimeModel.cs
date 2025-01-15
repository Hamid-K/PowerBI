using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019AF RID: 6575
	public class DateTimeModel : LiteralModel<DateTime>
	{
		// Token: 0x0600D6C1 RID: 54977 RVA: 0x002DA4EF File Offset: 0x002D86EF
		public override string ToOperatorString()
		{
			return base.Value.ToCSharpPseudoLiteral();
		}
	}
}
