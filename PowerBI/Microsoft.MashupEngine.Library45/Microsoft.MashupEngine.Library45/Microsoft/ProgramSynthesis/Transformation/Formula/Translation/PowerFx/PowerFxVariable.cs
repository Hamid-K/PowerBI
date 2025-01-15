using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018DE RID: 6366
	internal class PowerFxVariable : FormulaVariable
	{
		// Token: 0x0600CF8F RID: 53135 RVA: 0x002BF983 File Offset: 0x002BDB83
		public PowerFxVariable(string name)
			: base(name)
		{
		}

		// Token: 0x0600CF90 RID: 53136 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CF91 RID: 53137 RVA: 0x002C430A File Offset: 0x002C250A
		public override FormulaExpression CloneWith(string name)
		{
			return new PowerFxVariable(name);
		}
	}
}
