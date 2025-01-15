using System;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001856 RID: 6230
	internal class PythonStringLiteral : FormulaStringLiteral
	{
		// Token: 0x0600CBC9 RID: 52169 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public PythonStringLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600CBCA RID: 52170 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CBCB RID: 52171 RVA: 0x002B88E1 File Offset: 0x002B6AE1
		protected override string ToCodeString()
		{
			return base.Value.ToPythonLiteral();
		}
	}
}
