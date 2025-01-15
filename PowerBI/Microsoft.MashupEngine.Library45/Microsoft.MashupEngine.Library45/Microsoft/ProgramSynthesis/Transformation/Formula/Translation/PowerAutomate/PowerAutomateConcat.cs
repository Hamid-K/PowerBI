using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x0200190C RID: 6412
	internal class PowerAutomateConcat : PowerAutomateFunc
	{
		// Token: 0x0600D104 RID: 53508 RVA: 0x002C8FBE File Offset: 0x002C71BE
		public PowerAutomateConcat(IEnumerable<FormulaExpression> arguments)
			: base("Concat", arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600D105 RID: 53509 RVA: 0x002C8FD1 File Offset: 0x002C71D1
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerAutomateConcat(base.Children.Accept(visitor));
		}
	}
}
