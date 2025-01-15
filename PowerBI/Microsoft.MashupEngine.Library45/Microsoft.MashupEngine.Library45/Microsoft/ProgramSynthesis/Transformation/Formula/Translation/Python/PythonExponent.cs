using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001866 RID: 6246
	internal class PythonExponent : FormulaExponent
	{
		// Token: 0x0600CBFC RID: 52220 RVA: 0x002B8CAB File Offset: 0x002B6EAB
		public PythonExponent(FormulaExpression left, FormulaExpression right)
			: base(left, right, 2)
		{
		}

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x0600CBFD RID: 52221 RVA: 0x002B8CB6 File Offset: 0x002B6EB6
		public override string Symbol
		{
			get
			{
				return "**";
			}
		}

		// Token: 0x0600CBFE RID: 52222 RVA: 0x002B8CBD File Offset: 0x002B6EBD
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonExponent(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
