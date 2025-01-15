using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019AE RID: 6574
	public class NumberModel : LiteralModel<decimal>
	{
		// Token: 0x0600D6BF RID: 54975 RVA: 0x002DA4D5 File Offset: 0x002D86D5
		public override string ToOperatorString()
		{
			return base.Value.ToCSharpPseudoLiteral();
		}
	}
}
