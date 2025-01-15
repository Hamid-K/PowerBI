using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x0200190D RID: 6413
	internal class PowerAutomateVariable : FormulaVariable
	{
		// Token: 0x0600D106 RID: 53510 RVA: 0x002BF983 File Offset: 0x002BDB83
		public PowerAutomateVariable(string name)
			: base(name)
		{
		}

		// Token: 0x0600D107 RID: 53511 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D108 RID: 53512 RVA: 0x002C8FE4 File Offset: 0x002C71E4
		public override FormulaExpression CloneWith(string name)
		{
			return new PowerAutomateVariable(name);
		}
	}
}
