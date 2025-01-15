using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001904 RID: 6404
	internal class PowerAutomateFunc : FormulaFunc
	{
		// Token: 0x0600D0E6 RID: 53478 RVA: 0x002B47D9 File Offset: 0x002B29D9
		public PowerAutomateFunc(string name, IEnumerable<FormulaExpression> arguments)
			: base(name, arguments.ToArray<FormulaExpression>())
		{
		}

		// Token: 0x0600D0E7 RID: 53479 RVA: 0x002B47F8 File Offset: 0x002B29F8
		public PowerAutomateFunc(string name, params FormulaExpression[] arguments)
			: base(name, arguments)
		{
		}

		// Token: 0x0600D0E8 RID: 53480 RVA: 0x002C8DD0 File Offset: 0x002C6FD0
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerAutomateFunc(base.Name, base.Children.Accept(visitor));
		}
	}
}
