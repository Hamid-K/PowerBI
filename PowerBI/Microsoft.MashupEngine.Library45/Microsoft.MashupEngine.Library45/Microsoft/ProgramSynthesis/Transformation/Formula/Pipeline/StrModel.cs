using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019AD RID: 6573
	public class StrModel : LiteralModel<string>
	{
		// Token: 0x0600D6BD RID: 54973 RVA: 0x002DA4C0 File Offset: 0x002D86C0
		public override string ToOperatorString()
		{
			return base.Value.ToCSharpPseudoLiteral();
		}
	}
}
